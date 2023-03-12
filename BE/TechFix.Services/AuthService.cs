using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TechFix.EntityModels;
using TechFix.TransportModels.Auth;
using TechFix.Services.Common;
using TechFix.TransportModels;
using TechFix.Common;
using TechFix.Common.AppSetting;
using TechFix.Common.Constants;
using TechFix.Common.Constants.User;
using TechFix.Services.EmailServices;


namespace TechFix.Services
{

    public interface IAuthService
    {
        User FindUser(Guid? id);
        Task<TokenTransport> GetLoginTokenAsync(string userName, string password);
        UserToken GetValidToken(Guid? userId);
        Task ResetPasswordAsync(ResetPasswordTransport transport);
        void SetUserInfo(User user, string ipAddress);
        Task<RegisterTransport> InsertUserAsync(RegisterTransport model);
        UserToken GenerateValidateToken(Guid? userId);
        void LogOut(Guid? userId = null);
        string ValidateUser(User user, bool isCreate, string password = null);
        void HashPassword(string modelNewPassword, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string modelPassword, string userPasswordHash, string userPasswordSalt);
    }

    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private CommonService _commonService;
        private readonly IDistributedCache _distributedCache;
        public readonly IWebHostEnvironment _env;
        private readonly IEmailService _emailService;

        public AuthService(
            DataContext db,
            IOptions<AppSettings> appSettings,
            IMapper mapper,
            CommonService commonService,
            IDistributedCache distributedCache,
            IWebHostEnvironment env,
            IEmailService emailService)
        {
            _context = db;
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _commonService = commonService;
            _emailService = emailService;
            _distributedCache = distributedCache;
            _env = env;
        }

        public async Task<RegisterTransport> InsertUserAsync(RegisterTransport model)
        {
            model.Role = model.Role?.ToUpper();
            model.StaffCode = model.StaffCode?.Trim();
            model.Email = model.Email?.ToUpper();

            var user = _mapper.Map<User>(model);

            var error = ValidateUser(user, true, model.Password);
            if (error != null)
                throw new Exception(error);

            HashPassword(model.Password, out var passwordHash, out var passwordSalt);

            user.PasswordHash = Convert.ToBase64String(passwordHash);
            user.PasswordSalt = Convert.ToBase64String(passwordSalt);

            _context.Users.Add(user);

            await _context.SaveChangesAsync();
            return model;

        }

        public async Task<TokenTransport> GetLoginTokenAsync(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                throw new Exception("Tên đăng nhập hoặc mật khẩu không đúng.");
            var user = await _context.Users.SingleOrDefaultAsync(x => x.StaffCode == userName || x.Email == userName);
   
            if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new Exception("Tên đăng nhập hoặc mật khẩu không đúng.");
            
            if (user.Status != UserStatus.Active)
                throw new Exception("Tài khoản của bạn đã bị khóa");

            if (!UserRole.AllRoles.Contains(user.Role))
            {
                throw new Exception("Tài khoản của bạn không có quyền truy cập vào hệ thống");
            }

            var validateToken = GenerateValidateToken(user.Id);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, user.Id.ToString()),
                    new(ClaimTypes.Role, user.Role),
                    new("ValidateToken", validateToken.Token.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            
            return new TokenTransport
            {
                Id = user.Id,
                Username = user.StaffCode,
                FullName = user.FullName,
                Role = user.Role,
                Token = tokenString,
                Email = user.Email
            };
        }

        public User FindUser(Guid? id)
        {
            return _context.Users.Find(id);
        }

        public UserToken GenerateValidateToken(Guid? userId)
        {
            var validToken = GetValidToken(userId);
            if (validToken != null)
                return validToken;

            validToken = new UserToken
            {
                Id = Guid.NewGuid(),
                Token = Guid.NewGuid(),
                Type = UserTokenType.Validate,
                UserId = userId,
                Status = UserToken.UserTokenStatus.Active,
                IpAddress = _context.UserInfo.IpAddress,
                ExpiredDate = DateTime.Now.AddDays(30),
            };
            _context.UserTokens.Add(validToken);
            _context.SaveChanges();
            return validToken;
        }



        public void LogOut(Guid? userId = null)
        {
            var currentUserId = userId ?? _context.UserInfo.CurrentUserId;
            if (currentUserId == null)
                return;
            if(!_env.IsDevelopment())
            {
                RemoveCachedUser(currentUserId);
            }
            var validateToken = GetValidToken(currentUserId);
            if (validateToken != null)
            {
                validateToken.Status = "INACTIVE";
                _context.UserTokens.Update(validateToken);
                _context.SaveChanges();
            }
        }

        private void RemoveCachedUser(Guid? currentUserId)
        {
            var cachedUser = _distributedCache.Get(currentUserId.ToString());
            if (cachedUser != null)
            {
                _distributedCache.Remove(currentUserId.ToString());
            }
        }

        public UserToken GetValidToken(Guid? userId)
        {
            var validateToken = _context.UserTokens.FirstOrDefault(t => t.UserId == userId
                                                                       && t.Type.ToUpper() == UserTokenType.Validate
                                                                       && t.Status.ToUpper() == UserToken.UserTokenStatus.Active);
            return validateToken;
        }
        public void SetUserInfo(User user, string ipAddress)
        {
            _context.UserInfo.IpAddress = ipAddress;
            _context.UserInfo.CurrentUserId = user.Id;
            _context.UserInfo.Username = user.StaffCode;
            _context.UserInfo.StoreId = user.StoreId;
        }

        public bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("PasswordCantBeEmpty", "password");

            var keys = Convert.FromBase64String(storedSalt);
            using (var hmac = new System.Security.Cryptography.HMACSHA512(keys))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                var pwString = Convert.ToBase64String(computedHash);

                return (string.Equals(storedHash, pwString));
            }
        }

        public void HashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Mật khẩu không thể để trống");
            }

            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }


        public async Task ResetPasswordAsync(ResetPasswordTransport transport)
        {
            if (transport.Password != transport.PasswordAgain)
            {
                throw new Exception("PasswordAndPasswordAgainAreNotSame");
            }

            var user = await _context.Users.FindAsync(_context.UserInfo.CurrentUserId);
            HashPassword(transport.Password, out var passwordHash, out var passwordSalt);
            user.PasswordHash = Convert.ToBase64String(passwordHash);
            user.PasswordSalt = Convert.ToBase64String(passwordSalt);
            _context.Users.Update(user);

            await _context.SaveChangesAsync();
        }

        public string ValidateUser(User user, bool isCreate, string password = null)
        {

            if (user.StaffCode.Length > 50)
            {
                return "Mã nhân viên phải ít ơn 50 ký tự";
            }

            if (!StringUtil.IsValidEmail(user.Email))
            {
                return "Email không hợp lệ";
            }
            
            if (isCreate)
            {
                if (_context.Users.Any(u => u.StaffCode == user.StaffCode || u.Email == user.StaffCode))
                {
                    return "Mã nhân viên đã tồn tại trong hệ thống";
                }

                if (password != null && password.Length < 6)
                {
                    return "Mật khẩu phải nhiều hơn 6 ký tự";
                }

                if (_context.Users.Any(u => u.Email == user.Email || u.StaffCode == user.Email))
                {
                    return "Email đã tồn tại trong hệ thống";
                }
            }
            else
            {
                if (_context.Users.Any(u => (u.StaffCode == user.StaffCode || u.Email == user.StaffCode) && u.Id != user.Id))
                {
                    return "Mã nhân viên đã tồn tại trong hệ thống";
                }

                if (_context.Users.Any(u => (u.Email == user.Email || u.StaffCode == user.Email) && u.Id != user.Id))
                {
                    return "Email đã tồn tại trong hệ thống";
                }
            }

            return null;
        }
        

    }


}
