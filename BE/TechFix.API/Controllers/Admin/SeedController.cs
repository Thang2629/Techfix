using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TechFix.Common.AppSetting;
using TechFix.EntityModels;
using TechFix.Services;
using TechFix.Services.Common;
using VlinkSequence = TechFix.Services.Common.VlinkSequence;

namespace TechFix.API.Controllers.Admin
{
    [Route("admin/seed")]
    [Authorize(Roles = "ADMIN")]

    [ApiController]
    public class SeedController : CustomController
    {
        private readonly IAuthService _authService;
        private static Random random = new Random();
        private readonly VlinkSequence _vlinkSequence;

        public SeedController(IMapper mapper, IOptions<AppSettings> appSettings, DataContext context, IWebHostEnvironment env, CommonService commonService, IAuthService authService, VlinkSequence vlinkSequence)
            : base(mapper, appSettings, context, env, commonService)
        {
            _authService = authService;
            _vlinkSequence = vlinkSequence;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [Route("seed-user")]
        public async Task<IActionResult> SeedUser()
        {
            try
            {
                var referralUser = await _context.Users.FirstOrDefaultAsync(u => u.StaffCode == "vlinkgroup");
                if (referralUser == null)
                    return BadRequest("referralUser is not exist");

                //var userIds = await SeedUser(referralUser);

                return Ok("Seed OK.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        //private async Task<List<Guid?>> SeedUser(User referralUser)
        //{
        //    var result = new List<RegisterTransport>();
        //    for (var i = 1; i <= 10; i++)
        //    {
        //        var userName = $"vlinkgroupfun{i.ToString().PadLeft(2, '0')}";
        //        var model = new RegisterTransport
        //        {
        //            Username = userName,
        //            Email = $"{userName}@gmail.com",
        //            Password = RandomString(10),
        //            ReferralId = referralUser.VLinkId,
        //            Type = UserType.Affiliate
        //        };

        //        var addedUser = await _authService.AddUser(model, UserRole.Member, true);
        //        if (addedUser == null)
        //        {
        //            throw new Exception($"User {i} is invalid");
        //        }

        //        result.Add(addedUser);
        //        Console.WriteLine($"{i} is created!");
        //        await System.IO.File.AppendAllTextAsync("./wwwroot/SeedUser.txt", $"{model.Username}\t{model.Password}" + Environment.NewLine);
        //    }


        //    var userIds = result.Select(u => u.Id).ToList();
        //    return userIds;
        //}

     


        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
