using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TechFix.EntityModels;
using TechFix.TransportModels;
using TechFix.Common;
using TechFix.Common.AppSetting;

namespace TechFix.Services
{
	public interface IMemberServices
	{
    }

	public class MemberServices : IMemberServices
	{
		private DataContext _context;
		private IMapper _mapper;
		private AppSettings _appSettings;
		private IAuthService _authService;

        public MemberServices(DataContext context, IMapper mapper, IOptions<AppSettings> appSettings, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _authService = authService;
        }

        public void ChangePassword(ChangePasswordTransport model, Guid? userId)
		{
			var user = _context.Users.Find(userId);
			if (user == null)
				throw new Exception("UserNotFound");
			if (string.IsNullOrWhiteSpace(model.NewPassword))
				throw new Exception("Missing password");
			if (model.NewPassword.Length < 6)
				throw new Exception($"YourPasswordMustBeMinimum6Characters");

			if (!_authService.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
				throw new Exception("PasswordIsIncorrect");

			byte[] passwordHash, passwordSalt;
			_authService.HashPassword(model.NewPassword, out passwordHash, out passwordSalt);

			user.PasswordHash = Convert.ToBase64String(passwordHash);
			user.PasswordSalt = Convert.ToBase64String(passwordSalt);

			var validateToken = _authService.GenerateValidateToken(user.Id);
			validateToken.Token = Guid.NewGuid();

			_context.UserTokens.Update(validateToken);
			_context.Users.Update(user);
			_context.SaveChanges();
		}

		public async Task<ProfileTransport> GetMyProfile()
		{
			var user = await _context.FindAsync<User>(_context.UserInfo.CurrentUserId);
			var transport = _mapper.Map<ProfileTransport>(user);
			
			return transport;
		}

		public async Task<UpdateProfileTransport> UpdateMyProfile(UpdateProfileTransport transport)
        {
            var user = await _context.FindAsync<User>(_context.UserInfo.CurrentUserId);

            if (!string.IsNullOrWhiteSpace(transport.PhoneNumber))
            {
                var phoneNumber = Regex.Replace(transport.PhoneNumber, @"[^\d]", "");
                if (string.IsNullOrWhiteSpace(phoneNumber))
                    throw new Exception($"PhoneNumber must be numeric only");
            }

            if (!StringUtil.IsValidEmail(transport.Email))
            {
                throw new Exception("InvalidEmailFormat");
            }

            if (transport.Email != user.Email || transport.Username != user.StaffCode)
            {

                if (transport.Username != user.StaffCode)
                {
                    var existUsername =
                        await _context.Users.AnyAsync(u => u.Id != user.Id && u.StaffCode == transport.Username);
                    if (existUsername)
                        throw new Exception();

                    if (user.StaffCode.Length < 4)
                        throw new Exception();
                }

              
            }

            // if (user.KYC == KYCStatus.Approved)
            // 	throw new Exception("YourProfileWasVerifiedAndDisabledForUpdating");

            // if (user.KYC == KYCStatus.Review)
            // 	throw new Exception("CannotUpdateInformationBeforeTheSystemIsReviewingKYC");

            transport.Id = user.Id;
            _mapper.Map(transport, user);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            var userResult = await _context.Users.FindAsync(user.Id);
            var result = _mapper.Map<UpdateProfileTransport>(userResult);
            return result;
        }



		public async Task AddAffiliateMember(RegisterTransport transport)
		{
			await _authService.InsertUserAsync(transport);
		}
    }
}
