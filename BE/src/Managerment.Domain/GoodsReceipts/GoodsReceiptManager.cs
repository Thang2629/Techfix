using Managerment.Payments;
using System;
using Volo.Abp.Domain.Services;

namespace Managerment.GoodsReceipts
{
    public class GoodsReceiptManager : DomainService
    {
        public GoodsReceipt CreateAsync(
               string receipt_code,
               DateTime receipt_date,
               decimal total_price,
               decimal discount,
               decimal total_money,
               decimal paid,
               decimal debt,
               string notes,
               EPayment payment_method,
               bool can_return,
               Guid id_store,
               Guid id_user,
               Guid id_supplier
         )
        {
            return new GoodsReceipt(
                GuidGenerator.Create(),
                receipt_code,
                receipt_date,
                total_price,
                discount,
                total_money,
                paid,
                debt,
                notes,
                payment_method,
                can_return,
                id_store,
                id_user,
                id_supplier
            );
        }
    }
}


