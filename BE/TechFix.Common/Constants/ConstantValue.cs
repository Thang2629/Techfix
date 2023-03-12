using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.Common.Constants
{
    public static class ConstantValue
    {
        public const string FILE_TYPE_EXCEL = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public const string FILE_EXT_EXCEL = ".xlsx";
        public const string FILE_INVENTORY_EXCEL = "inventory-";
        public const string FILE_INPUTPRODUCT_EXCEL = "inputproduct-";
        public const string FILE_CUSTOMER_EXCEL = "customer-";
        public const string FILE_PRODUCT_EXCEL = "product-";
        public const string FILE_SUPPLIER_EXCEL = "supplier-";

        public const string PROCESS_CHECKING = "Đang kiểm tra";
        public const string PROCESS_INPROGRESS = "Đang sửa";
        public const string PROCESS_DONE = "Đã sửa xong";
        public const string PROCESS_PRICE_REPORT = "Báo giá";
        public const string PROCESS_RETURN_CUSTOMER = "Trả khách";

        public const string CUSTOMER_WHOLESALE = "Khách sỉ";
        public const string CUSTOMER_FLT = "Khách lẻ";
    }
}
