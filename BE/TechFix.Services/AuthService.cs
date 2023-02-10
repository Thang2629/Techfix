using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Google.Authenticator;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using Microsoft.Extensions.Localization;
using TechFix.Common.AppSetting;
using TechFix.Common.Constants;
using TechFix.Common.Constants.Packages;
using TechFix.Common.Constants.User;
using TechFix.Services.EmailServices;
using VlinkSequence = TechFix.Services.Common.VlinkSequence;


namespace TechFix.Services
{

    public interface IAuthService
    {
        User GetUser(Guid? id);
        Task<TokenTransport> Authenticate(string userName, string password, bool checkAdmin, string googleCode, string smsCode, string emailCode, string ipAddress = null);
        UserToken GetValidateToken(Guid? userId);
        Task<string> ForgotPassword(string userName, string toString);
        void ResetPassword(Guid token, ResetPasswordTransport transport);
        void SetAuthenticatedUser(Guid? userId);
        Task<RegisterTransport> AddUser(RegisterTransport model, string userRole, bool isSeedData = false);
        UserToken GenerateValidateToken(Guid? userId, string ipAddress = null);
        void LogOut(Guid? userId = null);
        string ValidateUser(User user, bool isCreate, string password = null);
        public string GetCleanIp(HttpRequest request);
        bool IsValidEmail(string transportEmail);
        void CreatePasswordHash(string modelNewPassword, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string modelPassword, string userPasswordHash, string userPasswordSalt);
        UserToken CreateUserToken(User user, string tokenType, int expiredHour = 48);
    }

    public class AuthService : IAuthService
    {
        private DataContext _context;
        private AppSettings _appSettings;
        private IMapper _mapper;
        private VlinkSequence _vlinkSequence;
        private CommonService _commonService;
        private readonly IDistributedCache _distributedCache;
        public readonly IWebHostEnvironment _env;
        private readonly IStringLocalizer _localizer;
        private readonly IEmailService _emailService;

        public AuthService(
            DataContext db,
            IOptions<AppSettings> appSettings,
            IMapper mapper,
            VlinkSequence vlinkSequence,
            CommonService commonService,
            IDistributedCache distributedCache,
            IWebHostEnvironment env,
            IStringLocalizer localizer,
            IEmailService emailService)
        {
            _context = db;
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _vlinkSequence = vlinkSequence;
            _commonService = commonService;
            _localizer = localizer;
            _emailService = emailService;
            this._distributedCache = distributedCache;
            _env = env;
        }

        public async Task<RegisterTransport> AddUser(RegisterTransport model, string userRole, bool isSeedData = false)
        {
            var user = _mapper.Map<User>(model);
            user.Id = Guid.NewGuid();
            user.Role = userRole;
            user.Status = UserStatus.WaitingConfirm;
      
            Create(user, model.Password);

            var userToken = CreateUserToken(user, UserTokenType.ConfirmRegister);
            var userLogin = _context.Users.Find(_context.AuthenticatedUserId);
            if (userLogin != null && userLogin.Role != UserRole.Admin && model.ReferralId == null)
            {
                _context.Users.Update(userLogin);
                user.ReferralUserId = userLogin.Id;
                user.ReferralId = userLogin.VLinkId;
                _context.Users.Add(user);
            }
            else
            {

                var referralUser = _context.Users.FirstOrDefault(u => u.VLinkId == model.ReferralId);
                if (referralUser == null)
                {
                    referralUser = _context.Users.First(u => u.VLinkId == _appSettings.DefaultReferralUser);
                }

                _context.Users.Update(referralUser);

                user.ReferralUserId = referralUser.Id;
                user.ReferralId = referralUser.VLinkId;

                _context.Users.Add(user);
            }

            if (isSeedData)
            {
                user.Status = UserStatus.Active;
            }
            else
            {
                var linkConfirm = $@"{_appSettings.CurrentUrl}/user/confirm-register?token={userToken.Token}";
            }

            _context.SaveChanges();

            var userResult = _context.Users.Find(user.Id);
            var result = _mapper.Map<RegisterTransport>(userResult);
            return result;
        }

        public async Task<TokenTransport> Authenticate(string userName, string password, bool checkAdmin, string googleCode, string smsCode, string emailCode, string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                throw new Exception("UsernameOrPasswordIsIncorrect");
            var user = _context.Users.SingleOrDefault(x => x.Username == userName);
            if(user == null)
                user = _context.Users.SingleOrDefault(x => x.Email == userName);

            // check if username exists
            if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new Exception("UsernameOrPasswordIsIncorrect");
            if (user.Status == UserStatus.WaitingConfirm)
                return new TokenTransport() {Status = "WAITING" };
            if (user.Status == UserStatus.Lock)
                return new TokenTransport() {Status = UserStatus.Lock };;
            if (user.Status != UserStatus.Active)
                throw new Exception("YourAccountIsNotActive");
            if (checkAdmin && user.Role != "ADMIN" && user.Role != "SUPPORTER" && user.Role != "ACCOUNTANT")
                throw new Exception("YourAccountDoesNotHavePermissionToAccess");
            if (!checkAdmin && user.Role == "ACCOUNTANT")
                throw new Exception("YourAccountDoesNotHavePermissionToAccess");

            var authenticationTypes = new List<string>();
            if (authenticationTypes.Any())
                return new TokenTransport {AuthenticationTypes = authenticationTypes};
            var validateToken = GenerateValidateToken(user.Id, ipAddress);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("ValidateToken", validateToken.Token.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            // authentication successful

            var needPayInterestCredit = false;
            var isOverdueDebtWarning = false;
            
            return new TokenTransport
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                Token = tokenString,
                Email = user.Email,
                Type = user.Type,
                Avatar = user.Avatar,
            };

        }


        public User GetUser(Guid? id)
        {
            return _context.Users.Find(id);
        }

        public UserToken GenerateValidateToken(Guid? userId, string ipAddress = null)
        {
            var validateToken = GetValidateToken(userId);
            if (validateToken != null)
                return validateToken;

            validateToken = new UserToken
            {
                Id = Guid.NewGuid(),
                Token = Guid.NewGuid(),
                Type = UserTokenType.Validate,
                UserId = userId,
                Status = UserToken.UserTokenStatus.Active,
                IpAddress = ipAddress,
                ExpiredDate = DateTime.Now.AddDays(30),
            };
            _context.UserToken.Add(validateToken);
            _context.SaveChanges();
            return validateToken;
        }



        public void LogOut(Guid? userId = null)
        {
            var currentUserId = userId ?? _context.AuthenticatedUserId;
            if (currentUserId == null)
                return;
            if(!_env.IsDevelopment())
            {
                RemoveCachedUser(currentUserId);
            }
            var validateToken = GetValidateToken(currentUserId);
            if (validateToken != null)
            {
                validateToken.Status = "INACTIVE";
                _context.UserToken.Update(validateToken);
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

        public UserToken GetValidateToken(Guid? userId)
        {
            var validateToken = _context.UserToken.FirstOrDefault(t => t.UserId == userId
                                                                       && t.Type.ToUpper() == UserTokenType.Validate
                                                                       && t.Status.ToUpper() == UserToken.UserTokenStatus.Active);
            return validateToken;
        }
        public void SetAuthenticatedUser(Guid? userId)
        {
            _context.AuthenticatedUserId = userId;
        }

        public async Task<string> ForgotPassword(string userName, string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new Exception("PleaseEnterUserName");
            var user = _context.Users.FirstOrDefault(u => (u.Username == userName || u.Email == userName));
            if (user == null)
                throw new Exception("UserIsNotFound");
            if (user.Status != UserStatus.Active)
                throw new Exception("TheUserIsNotConfirmed");
            if (user.Email == null)
                throw new Exception("TheUserDoesNotHaveEmailToRecoverPassword");

            var userToken = CreateUserToken(user, UserTokenType.ForgotPassword);
            _context.SaveChanges();

            var subject = "Please reset your password";
            var body = GetBodyResetMail(userToken.Token, user.Username);
            var email = new EmailRequest(user.Email, null, null, subject, body, true);

            await _emailService.SendAsync(email);

            return StringUtil.HideEmail(user.Email);
        }

        public UserToken CreateUserToken(User user, string tokenType, int expiredHour = 48)
        {
            var oldTokens = _context.UserToken.Where(t => t.UserId == user.Id && t.Status == UserToken.UserTokenStatus.Active && t.ExpiredDate > DateTime.Now && t.Type == tokenType).ToList();
            foreach (var oldUserToken in oldTokens)
            {
                oldUserToken.Status = UserToken.UserTokenStatus.Inactive;
                _context.Update(oldUserToken);
            }

            var userToken = new UserToken()
            {
                //IpAddress = ipAddress,
                Token = Guid.NewGuid(),
                ExpiredDate = DateTime.Now.AddHours(expiredHour),
                Type = tokenType,
                Status = "ACTIVE",
                UserId = user.Id
            };
            _context.Add(userToken);
            return userToken;
        }

        string GetBodyResetMail(Guid token, string userName)
        {
            var changePasswordLink = $@"{_appSettings.CurrentUrl}/user/reset-password?token={token}";
            var resetPasswordLink = $@"{_appSettings.CurrentUrl}/home?forgot=true";
            return $@"<table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""border-collapse:collapse"">
                        <tbody>
                           <tr>
                              <td valign=""bottom"" style=""border-collapse:collapse;padding:20px 16px 12px"">
                              <div style=""text-align:center"">
                                 <a href=""{_appSettings.CurrentUrl}/"" style=""color:#439fe0;font-weight:bold;text-decoration:none;word-break:break-word"" target=""_blank"">
                                 <img src=""{_appSettings.CurrentUrl}/assets/img/logo.png"" style=""outline:none;text-decoration:none;border:none;width:120px;height:36px"" class=""CToWUd""></a>
                              </div>
                              </td>
                           </tr>
                        </tbody>
                        </table>

                        <table cellpadding=""32"" cellspacing=""0"" border=""0"" align=""center"" style=""border-collapse:collapse;background:white;border-radius:0.5rem;margin-bottom:1rem"">
                        <tbody>
                           <tr><td width=""546"" valign=""top"" style=""border-collapse:collapse"">
                              <div style=""max-width:600px;margin:0 auto"">
                              <div style=""background:white;border-radius:0.5rem;margin-bottom:1rem"">
                              <p style=""font-size:17px;line-height:24px;margin:0 0 16px"">Dear {userName},</p>
                              <p style=""font-size:17px;line-height:24px;margin:0 0 16px"">You told us you <span class=""il"">forgot</span> your password. If you really did, click here to choose a new one:</p>
                              <div style=""text-align:center;margin:2rem 0 1rem"">
                              <table cellpadding=""0"" cellspacing=""0"" style=""border-collapse:collapse;background:#2b60a0;border-bottom:2px solid #2b60a0;border-radius:4px;padding:14px 32px;display:inline-block"">
                        <tbody>
                           <tr>
                              <td style=""border-collapse:collapse""><a href=""{changePasswordLink}"" style=""color:white;font-weight:normal;text-decoration:none;word-break:break-word;display:inline-block;letter-spacing:1px;font-size:20px;line-height:26px"" align=""center"" target=""_blank"" data-saferedirecturl=""{changePasswordLink}"">Choose a new password</a></td>
                           </tr>
                        </tbody>
                        </table></div>	<p style=""font-size:0.9rem;line-height:20px;margin:0 auto 1rem;color:#aaa;text-align:center;max-width:100%;word-break:break-word"">Need the raw link? <a href=""{changePasswordLink}"" style=""color:#439fe0;font-weight:bold;text-decoration:none;word-break:break-word"" target=""_blank"" data-saferedirecturl=""{changePasswordLink}"">{changePasswordLink}</a></p>
                        <p style=""font-size:17px;line-height:24px;margin:0 0 16px"">If you didn't mean to reset your password, then you can just ignore this email; your password will not change.</p>
                        <p>If you don’t use this link within 3 hours, it will expire. To get a new password reset link, visit <a href=""{resetPasswordLink}"">{resetPasswordLink}</a></p>
                        </div>
                        </div>
                        </td>
                        </tr></tbody></table>";
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

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("PasswordCantBeEmpty", "password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("PasswordCantBeEmpty", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


        public void ResetPassword(Guid token, ResetPasswordTransport transport)
        {
            if (transport.Password != transport.PasswordAgain)
            {
                throw new Exception("PasswordAndPasswordAgainAreNotSame");
            }
            var userToken = GetUserToken(token, out var user, UserTokenType.ForgotPassword);

            CreatePasswordHash(transport.Password, out var passwordHash, out var passwordSalt);
            user.PasswordHash = Convert.ToBase64String(passwordHash);
            user.PasswordSalt = Convert.ToBase64String(passwordSalt);
            _context.Users.Update(user);

            userToken.Status = UserToken.UserTokenStatus.Inactive;
            _context.UserToken.Update(userToken);
            _context.SaveChanges();
        }

        private UserToken GetUserToken(Guid token, out User user, string tokenType)
        {
            var userToken = _context.UserToken.FirstOrDefault(t => t.Token == token);
            if (userToken == null)
            {
                throw new Exception("TheLinkIsNotValid");
            }

            if (userToken.Status != UserToken.UserTokenStatus.Active)
            {
                throw new Exception("ThisLinkIsAlreadyInUse");
            }

            if (userToken.ExpiredDate < DateTime.Now
                && userToken.Type == tokenType)
            {
                throw new Exception("TheLinkHasExpired");
            }

            user = _context.Users.Find(userToken.UserId);
            if (user == null)
                throw new Exception("TheLinkHasExpired");
            if (user.Status != UserStatus.WaitingConfirm && tokenType == UserTokenType.ConfirmRegister)
                throw new Exception("ThisUserHasBeenConfirmed");

            return userToken;
        }

        private User Create(User user, string password)
        {
            TrimUserModel(user);
            // validation
            var error = ValidateUser(user, true, password);
            if (error != null)
                throw new Exception(error);

            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.PasswordHash = Convert.ToBase64String(passwordHash);
            user.PasswordSalt = Convert.ToBase64String(passwordSalt);
            user.VLinkId = _vlinkSequence.GetNextVlinkId();
            return user;
        }

        private void TrimUserModel(User user)
        {
            user.Username = user.Username?.Trim();
            user.Email = user.Email?.Trim();
        }

        public string ValidateUser(User user, bool isCreate, string password = null)
        {

            if (user.Username.Length > NumberConfig.UsernameMaxLength)
                throw new Exception(_localizer["usernameMustBeLessThanXCharacters", NumberConfig.UsernameMaxLength + 1]);

            if (!IsValidEmail(user.Email))
            {
                return "InvalidEmailFormat";
            }

            user.Type = user.Type?.ToUpper();
            if (!UserType.AllType.Contains(user.Type))
            {
                return "UserTypeIsNotValid";
            }

            if (!string.IsNullOrWhiteSpace(user.ReferralId))
            {
                var isExistReferral = _context.Users.Any(u => u.VLinkId == user.ReferralId);
                if (!isExistReferral)
                {
                    user.ReferralId = null;
                    //throw new ArgumentException($"Referral not found.");
                }
            }

            //var rootEmail = StringUtil.GetRootEmail(user.Email);
            var rootEmail = user.Email;
            if (isCreate)
            {
                if (_context.Users.Any(x => x.Username == user.Username))
                {
                    return _localizer["UsernameXIsAlreadyTaken", user.Username].Value;
                }

                if (user.Username.Length < 4)
                    return _localizer["YourUsernameMustBeMinimumXCharacters", 4].Value;
                if (password != null && password.Length < 6)
                    return _localizer["UserPasswordMustBeMinimumXCharacters", 6].Value;

                if (_context.Users.Any(x => x.Email == rootEmail))
                    return _localizer["EmailXIsAlreadyTaken", rootEmail].Value;
            }
            else
            {
                if (_context.Users.Any(x => x.Username == user.Username && x.Id != user.Id))
                    return _localizer["UsernameXIsAlreadyTaken", user.Username].Value;
                if (_context.Users.Any(x => x.Email == rootEmail && x.Id != user.Id))
                    return _localizer["EmailXIsAlreadyTaken", rootEmail].Value;
            }

            return null;
        }

        public bool IsValidEmail(string emailAddress)
        {
            try
            {
                var m = new MailAddress(emailAddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }


        public string GetCleanIp(HttpRequest request)
        {
            var ipAddress = request.HttpContext.Connection.RemoteIpAddress?.ToString().Trim();
            if (request.Headers.TryGetValue("X-Forwarded-For", out var forwardedIps))
            {
                ipAddress = forwardedIps.First();
                if (ipAddress.Contains(","))
                {
                    ipAddress = ipAddress.Split(',').First().Trim();
                }
            }

            return ipAddress;
        }



    }


}
