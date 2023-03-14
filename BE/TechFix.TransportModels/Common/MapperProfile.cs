using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechFix.EntityModels;
using TechFix.TransportModels.Dtos;

namespace TechFix.TransportModels.Common
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region Products
            CreateMap<ProductTransport, Product>();
            CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacturer.Name))
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.ProductUnitName, opt => opt.MapFrom(src => src.ProductUnit.Name))
            .ForMember(dest => dest.ProductConditionName, opt => opt.MapFrom(src => src.ProductCondition.Name));
            #endregion

            #region Suppliers
            CreateMap<Supplier, SupplierDto>();
            CreateMap<SupplierTransport, Supplier>();
            #endregion

            #region Product Histories
            #endregion
        }
    }
}
