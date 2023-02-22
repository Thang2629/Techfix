using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using TechFix.EntityModels;
using TechFix.Common.AppSetting;
using Microsoft.CodeAnalysis;

namespace TechFix.Services
{
    public interface IHistoryServices
    {
        Task<Guid> WriteProductHistory(Product product, string actionName, string code, int quantity);
    }

    public class HistoryServices : IHistoryServices
	{
		private DataContext _context;
		private IMapper _mapper;
		private AppSettings _appSettings;
		private IAuthService _authService;

        public HistoryServices(DataContext context, IMapper mapper, IOptions<AppSettings> appSettings, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _authService = authService;
        }


        public async Task<Guid> WriteProductHistory(Product product, string actionName, string code, int quantity)
        {
            var id = Guid.NewGuid();
            var productHistory = new ProductHistory
            {
                Id = id,
                StoreId = _context.UserInfo.StoreId,
                ProductId = product.Id,
                ProductConditionId = product.ProductConditionId,
                UserId = _context.UserInfo.CurrentUserId,
                DateTime = DateTime.Now,
                ActionName = actionName,
                Code = code,
                Quantity = quantity,
                Warranty = product.Warranty,
                OriginalPrice = product.OriginalPrice
            };
            _context.ProductHistories.Add(productHistory);
            await _context.SaveChangesAsync();

            return id;
        }
    }
}
