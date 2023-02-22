using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bogus.DataSets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Options;
using TechFix.Common.AppSetting;
using TechFix.Common.Helper;
using TechFix.Common.Paging;
using TechFix.EntityModels;
using TechFix.Services;
using TechFix.Services.Common;
using TechFix.TransportModels;
using TechFix.TransportModels.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplatesController : CustomController
    {
        private IWebHostEnvironment _env;
        private IHelperService _helperService;
        public TemplatesController(IMapper mapper, IOptions<AppSettings> appSettings, DataContext context, IWebHostEnvironment env, CommonService commonService, IHelperService helperService) : base(mapper, appSettings, context, env, commonService)
        {
            _env = env;
            _helperService = helperService;
        }

        // GET: api/<TemplatesController>
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAllTemplates(PagingParams param)
        {
            var queryable = _context.Templates
                .Where(m => !m.IsDeleted);
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            var projectTo = queryable.ProjectTo<CategoryDto>(_mapper.ConfigurationProvider);
            var result = PagedList<CategoryDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
            return Ok(result);
        }

        // GET api/<TemplatesController>
        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> Download(string code)
        {
            var item = _context.Templates.Where(x => x.Code.Equals(code.Trim()) && !x.IsDeleted).FirstOrDefault();
            if (item == null)
            {
                return BadRequest(new NotFoundResult());
            }
            var path = item.Path;
            if (!string.IsNullOrEmpty(path) && !item.Path.StartsWith("/")) path = "/" + path; 
            var downloadUrl = Request.Scheme + "://" + Request.Host.Value + "/upload" + path + $"/{item.Name}";
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetAsync(downloadUrl);
                if (response.IsSuccessStatusCode)
                {
                    HttpContent content = response.Content;
                    var contentStream = await content.ReadAsStreamAsync(); // get the actual content stream
                    return File(contentStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", item.Name);
                }
                return BadRequest(new FileNotFoundException());
            }
            catch(Exception ex)
            {
                return BadRequest($"Failed to get download file {ex.Message}");
            }
        }

        // POST api/<TemplatesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] TemplateTransport transport)
        {
            if(transport != null && transport.File != null)
            {
                string uploads = !string.IsNullOrEmpty(transport.Path) ? Path.Combine(_env.WebRootPath, "upload", transport.Path) : Path.Combine(_env.WebRootPath, "upload");
                var file = transport.File;
                if(await _helperService.Upload(file, uploads))
                {
                    _context.Templates.Add(new Template()
                    {
                        Code = transport.Code,
                        Name = file.FileName,
                        Description = transport.Description,
                        Path = transport.Path,
                        CreatedDate = DateTime.UtcNow,
                        CreatedUser = _context.UserInfo.CurrentUserId,
                        IsDeleted = false,
                    });

                    await _context.SaveChangesAsync();
                }
                return Ok();
            }
            return BadRequest();
        }

        //PUT api/<TemplatesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromForm] TemplateTransport transport)
        {
            var model = await _context.Templates.FindAsync(id);
            if (model != null)
            {
                string uploads = !string.IsNullOrEmpty(transport.Path) ? Path.Combine(_env.WebRootPath, "upload", transport.Path) : Path.Combine(_env.WebRootPath, "upload");
                var file = transport.File;
                if (await _helperService.Upload(file, uploads, model.Name))
                {
                    model.Name = file.FileName;
                    model.Code = transport.Code;
                    model.Description = transport.Description;
                    model.Path = transport.Path;
                    model.ModifiedDate = DateTime.UtcNow;
                    model.ModifiedUser = _context.UserInfo.CurrentUserId;

                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
            return BadRequest();
        }

        // DELETE api/<TemplatesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var item = await _context.Templates.FindAsync(id);
            if (item != null)
            {
                item.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
