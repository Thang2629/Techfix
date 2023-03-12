using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bogus.DataSets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NuGet.Configuration;
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
    public class ProductHistoriesController : CustomController
    {
        private IHistoryServices _historyService;
        public ProductHistoriesController(IMapper mapper,
            IOptions<AppSettings> appSettings,
            DataContext context,
            IWebHostEnvironment env,
            CommonService commonService,
            IHistoryServices historyServices) : base(mapper, appSettings, context, env, commonService)
        {
            _historyService = historyServices;
        }

        //GET: api/<ProductHistoriesController>
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAllProductHistories(PagingParams param)
        {
            var queryable = _context.ProductHistories
                .Where(m => !m.IsDeleted);
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            var mapConfig = new MapperConfiguration(
                cfg => cfg.CreateMap<ProductHistory, ProductHistoryDto>()
                    .ForMember(dest => dest.Action, opt => opt.MapFrom(src => src.ActionName))
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.FullName : null))
                    .ForMember(dest => dest.ProductCondition, opt => opt.MapFrom(src => src.ProductCondition != null ? src.ProductCondition.Name : null))
            );
            var projectTo = queryable.ProjectTo<ProductHistoryDto>(mapConfig);
            var result = PagedList<ProductHistoryDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
            return Ok(result);
        }

        [HttpGet]
        [Route("get/{code}")]
        public IActionResult GetProductHistoryByCode(string code)
        {
            try
            {
                if (!string.IsNullOrEmpty(code))
                {
                    var items = _historyService.GetProductHistoryByCode(code);
                    List<ProductHistoryDto> list = new List<ProductHistoryDto>();
                    if (items != null && items.Count > 0)
                    {
                        foreach(var item in items)
                        {
                            var listItem = new ProductHistoryDto
                            {
                                Id = item.Id,
                                Action = item.ActionName,
                                Code = item.Code,
                                DateTime = item.DateTime,
                                OriginalPrice = item.OriginalPrice,
                                ProductCondition = item.ProductCondition?.Name,
                                Quantity = item.Quantity,
                                StoreName = item.Store?.Name,
                                UserName = item.User?.FullName,
                                Warranty = item.Warranty,
                            };

                            list.Add(listItem);
                        }
                    }
                    return Ok(list);
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
