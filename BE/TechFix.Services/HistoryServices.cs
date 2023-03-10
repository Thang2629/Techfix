using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using TechFix.EntityModels;
using TechFix.Common.AppSetting;
using Microsoft.EntityFrameworkCore;
using Customer = TechFix.EntityModels.Customer;

namespace TechFix.Services
{
    public interface IHistoryServices
    {
        Task<Guid> WriteProductHistory(Product product, string actionName, string code, int quantity);
        Task<Guid> WriteMoneyInHistory(Bill bill, Customer customer, decimal amount, Guid? cashierId, DateTime paymentDate, PaymentMethod paymentMethod);
        Task<Guid> WriteMoneyOutHistory(Guid supplierId, decimal amount, Guid? cashierId, DateTime paymentDate, PaymentMethod paymentMethod);
        Task DeleteMoneyInHistory(Guid? id);
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

        public async Task<Guid> WriteMoneyInHistory(Bill bill, Customer customer, decimal amount, Guid? cashierId, DateTime paymentDate, PaymentMethod paymentMethod)
        {
            var id = Guid.NewGuid();
            var history = new MoneyInHistory
            {
                Id = id,
                Amount = amount,
                CashierId = cashierId,
                PaymentDate = paymentDate,
                BillId = bill.Id,
                PaymentMethodId = paymentMethod.Id,
                CustomerId = customer.Id,
                PaymentMethod = paymentMethod
            };
            _context.MoneyInHistories.Add(history);
            await _context.SaveChangesAsync();

            await CalculateMoneyInDebt(id, bill, customer);

            return id;
        }

        public async Task DeleteMoneyInHistory(Guid? id)
        {
            var history = await _context.MoneyInHistories
                .Include(h => h.Bill)
                .Include(h => h.Customer)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (history != null)
            {
                //todo tính lại tiền cho member
                history.IsDeleted = true;
            }

            await _context.SaveChangesAsync();
        }

        private async Task CalculateMoneyInDebt(Guid? id, Bill bill, Customer customer)
        {
            bill.AmountPaid = await _context.MoneyInHistories
                .Where(h => h.BillId == id && !h.IsDeleted)
                .SumAsync(h => h.Amount);
            bill.AmountOwed = bill.TotalAmount - bill.AmountPaid;
            await _context.SaveChangesAsync();

            customer.AmountOwed = await _context.Bills
                .Where(b => b.CustomerId == customer.Id && !b.IsDeleted)
                .SumAsync(h => h.AmountOwed);
            await _context.SaveChangesAsync();
        }

        public async Task<Guid> WriteMoneyOutHistory(Guid supplierId, decimal amount, Guid? cashierId, DateTime paymentDate, PaymentMethod paymentMethod)
        {

            var id = Guid.NewGuid();
            var history = new MoneyOutHistory
            {
                Id = id,
                Amount = amount,
                CashierId = cashierId,
                PaymentDate = paymentDate,
                PaymentMethodId = paymentMethod.Id,
                SupplierId = supplierId,
                PaymentMethod = paymentMethod
            };
            _context.MoneyOutHistories.Add(history);
            await _context.SaveChangesAsync();

            return id;
        }

        public async Task DeleteMoneyOutHistory(Guid? id)
        {
            var history = await _context.MoneyOutHistories
                .Include(m => m.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (history != null)
            {
                history.IsDeleted = true;
                //todo tính lại tiền cho Supplier
            }


            await _context.SaveChangesAsync();
        }

        private async Task CalculateMoneyOutDebt(InputProduct inputProduct, Supplier supplier)
        {
            inputProduct.AmountPaid = await _context.MoneyOutHistories
                .Where(h => h.InputProductId == inputProduct.Id && !h.IsDeleted)
                .SumAsync(h => h.Amount);
            inputProduct.AmountOwed = inputProduct.TotalAmount - inputProduct.AmountPaid;
            await _context.SaveChangesAsync();

            supplier.AmountOwed = await _context.InputProducts
                .Where(b => b.SupplierId == supplier.Id && !b.IsDeleted)
                .SumAsync(h => h.AmountOwed);
            await _context.SaveChangesAsync();
        }
    }
}
