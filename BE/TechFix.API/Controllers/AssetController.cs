using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TechFix.Common.AppSetting;
using TechFix.EntityModels;

namespace TechFix.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = UserRole.MANAGER)]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly DataContext _context;

        public AssetController(IOptions<AppSettings> appSettings, DataContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadAsync()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var folderName = _appSettings.ImagePath;
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var extension = Path.GetExtension(fileName);
                    var internalFileName = Guid.NewGuid() + extension;
                    var fullPath = Path.Combine(pathToSave, internalFileName);
                    //var relativePath = Path.Combine(folderName, internalFileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { Photo = internalFileName });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


    }
}