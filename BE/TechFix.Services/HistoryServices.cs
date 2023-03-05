using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using TechFix.EntityModels;
using TechFix.Common.AppSetting;

namespace TechFix.Services
{
    public interface IHistoryServices
    {
        Task<Guid> WriteProductHistory(Product product, string actionName, string code, int quantity);
        Task<Guid> WriteMoneyInHistory(Guid billId, Customer customer, decimal amount, decimal inDebtAmount, Guid? cashierId, DateTime paymentDate, PaymentMethod paymentMethod);
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

            return id;
        }

        public async Task<Guid> WriteMoneyInHistory(Guid billId, Customer customer, decimal amount, decimal inDebtAmount, Guid? cashierId, DateTime paymentDate, PaymentMethod paymentMethod)
        {
            var id = Guid.NewGuid();
            var history = new MoneyInHistory
            {
                Id = id,
                Amount = amount,
                CashierId = cashierId,
                PaymentDate = paymentDate,
                BillId = billId,
                PaymentMethodId = paymentMethod.Id,
                CustomerId = customer?.Id
            };
            _context.MoneyInHistories.Add(history);

            if (customer != null)
            {
                customer.AmountOwed += inDebtAmount;
            }

            return id;
        }
    }
}
