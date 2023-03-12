using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFix.Common.AppSetting;
using TechFix.EntityModels;
using TechFix.EntityModels.Abstracts;
using TechFix.EntityModels.Enums;
using TechFix.Services.Common;
using TechFix.TransportModels;

namespace TechFix.Services
{
    public interface IProductAssociatedService
    {
        Task<List<ComboboxData>> GetComboboxDataAsync(ProductAssociatedType type);
    }
    public class ProductAssociatedService : IProductAssociatedService
    {
        protected readonly DataContext _context;
        protected readonly IMapper _mapper;
        protected readonly AppSettings _appSettings;

        public ProductAssociatedService(
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            DataContext context)
        {
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _context = context;
        }

        public async Task<List<ComboboxData>> GetComboboxDataAsync(ProductAssociatedType type)
        {
            var query = GetProductAssociatedQueryable(type);
            var result = await query.ProjectTo<ComboboxData>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return result;
        }

        private IQueryable<ProductAssociated> GetProductAssociatedQueryable(ProductAssociatedType type)
        {
            switch (type)
            {
                case ProductAssociatedType.Manufacturer:
                    return _context.Manufacturers;
                case ProductAssociatedType.ProductCondition:
                    return _context.ProductConditions;
                case ProductAssociatedType.ProductUnit:
                    return _context.ProductUnits;
                case ProductAssociatedType.Store:
                    return _context.Stores;
                case ProductAssociatedType.Supplier:
                    return _context.Suppliers;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
