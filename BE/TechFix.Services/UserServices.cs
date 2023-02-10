using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TechFix.EntityModels;
using TechFix.TransportModels;
using Microsoft.Extensions.Localization;
using TechFix.Common.AppSetting;
using TechFix.Common.Constants.User;
using TechFix.Services.EmailServices;
using TechFix.Services.Common;
using VlinkSequence = TechFix.Services.Common.VlinkSequence;

namespace TechFix.Services
{
	public interface IUserServices
	{
		void ChangePassword(ChangePasswordTransport model, Guid? userId);

		Task<ProfileTransport> GetMyProfile();
		Task<UpdateProfileTransport> UpdateMyProfile(UpdateProfileTransport transport);
		Task AddAffiliateMember(AddAffiliateMemberTransport transport);
		List<AffiliateMemberTransport> GetAffiliateMember(string q);
    }

	public class UserServices : IUserServices
	{
		private DataContext _context;
		private IMapper _mapper;
		private AppSettings _appSettings;
		private IAuthService _authService;
		private readonly IStringLocalizer _localizer;
        private readonly IEmailService _emailService;
        private readonly CommonService _commonService;
        private readonly VlinkSequence _vlinkSequence;

        public UserServices(DataContext context, IMapper mapper, IOptions<AppSettings> appSettings, IAuthService authService, IStringLocalizer localizer, IEmailService emailService, VlinkSequence vlinkSequence, CommonService commonService)
		{
			_context = context;
			_mapper = mapper;
			_appSettings = appSettings.Value;
			_authService = authService;
			_localizer = localizer;
            _emailService = emailService;
            _vlinkSequence = vlinkSequence;
            _commonService = commonService;
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
			_authService.CreatePasswordHash(model.NewPassword, out passwordHash, out passwordSalt);

			user.PasswordHash = Convert.ToBase64String(passwordHash);
			user.PasswordSalt = Convert.ToBase64String(passwordSalt);

			var validateToken = _authService.GenerateValidateToken(user.Id);
			validateToken.Token = Guid.NewGuid();

			_context.UserToken.Update(validateToken);
			_context.Users.Update(user);
			_context.SaveChanges();
		}

		public async Task<ProfileTransport> GetMyProfile()
		{
			var user = await _context.FindAsync<User>(_context.AuthenticatedUserId);
			var transport = _mapper.Map<ProfileTransport>(user);
			
			return transport;
		}

		public async Task<UpdateProfileTransport> UpdateMyProfile(UpdateProfileTransport transport)
        {
            var user = await _context.FindAsync<User>(_context.AuthenticatedUserId);

            if (!string.IsNullOrWhiteSpace(transport.PhoneNumber))
            {
                var phoneNumber = Regex.Replace(transport.PhoneNumber, @"[^\d]", "");
                if (string.IsNullOrWhiteSpace(phoneNumber))
                    throw new Exception($"PhoneNumber must be numeric only");
                if (transport.PhoneNumber != user.PhoneNumber)
                {
                    var isExistPhone = _context.Users.Any(u => u.Id != user.Id
                                                               && u.PhoneNumber == transport.PhoneNumber
                                                               && u.DialCode == transport.DialCode);
                    if (isExistPhone)
                    {
                        throw new Exception("PhoneNumberIsAlreadyTaken");
                    }
                }
            }

            if (!_authService.IsValidEmail(transport.Email))
            {
                throw new Exception("InvalidEmailFormat");
            }

            if (transport.Email != user.Email || transport.Username != user.Username)
            {

                if (transport.Username != user.Username)
                {
                    var existUsername =
                        await _context.Users.AnyAsync(u => u.Id != user.Id && u.Username == transport.Username);
                    if (existUsername)
                        throw new Exception(_localizer["UsernameXIsAlreadyTaken", transport.Username].Value);

                    if (user.Username.Length < 4)
                        throw new Exception(_localizer["YourUsernameMustBeMinimumXCharacters", 4].Value);
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

        public List<AffiliateMemberTransport> GetAffiliateMember(string q)
        {
            var affiliate = _context.Users.Where(u => u.ReferralUserId == _context.AuthenticatedUserId);
            if (!string.IsNullOrWhiteSpace(q))
            {
                affiliate = affiliate.Where(a => EF.Functions.Like(a.SearchData, $@"%{q}%"));
            }

            return null;
        }


		public async Task AddAffiliateMember(AddAffiliateMemberTransport transport)
		{
			var user = await _context.Users.FindAsync(_context.AuthenticatedUserId);
			var model = new RegisterTransport()
			{
				Username = transport.Username,
				Email = transport.Email,
				Address = transport.Address,
				Birthday = transport.Birthday,
				Country = transport.Country,
				FirstName = transport.FirstName,
				LastName = transport.LastName,
				Gender = transport.Gender,
				Password = transport.Password,
				//PhoneNumber = transport.PhoneNumber,
				ReferralId = user.VLinkId,
				Type = string.IsNullOrWhiteSpace(transport.Type) ? UserType.Affiliate : transport.Type
			};
			await _authService.AddUser(model, UserRole.Member);

		}

    }
}
