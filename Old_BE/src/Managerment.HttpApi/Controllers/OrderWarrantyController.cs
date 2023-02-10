using Managerment.CustomerTypes;
using Managerment.OrderWarranties;
using Managerment.ProcessWarranties;
using Managerment.Shares;
using Managerment.WarrantyProcesss;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managerment.Controllers
{
    [Route("api/order-warranty")]
    public class OrderWarrantyController : ManagermentController
    {
        private readonly IOrderWarrantyAppService _orderWarrantyAppService;
        public OrderWarrantyController(IOrderWarrantyAppService orderWarrantyAppService)
        {
            _orderWarrantyAppService = orderWarrantyAppService;
        }
        /// <summary>
        /// tạo đơn bảo hành
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task CreateAsync([FromBody]CreateOrderWarrantyDto input)
        {
            await _orderWarrantyAppService.CreateAsync(input);
        }
        /// <summary>
        /// danh sách đơn bảo hành
        /// </summary>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="e_customer_type"></param>
        /// <returns></returns>
        [HttpGet("order-warranties")]
        public async Task<ReponseDataDto<ResultLstOrderWarrantyDto>> GetListAsync([Required]int page, [Required]int perPage, string filter,
                                                                DateTime? fromDate, DateTime? toDate,
                                                                ECustomerTypeEnum? e_customer_type)
        {
            return await _orderWarrantyAppService.GetListAsync(page, perPage, filter, fromDate, toDate, e_customer_type);
        }

        /// <summary>
        /// danh sách quy trình bảo hành
        /// </summary>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="filter"></param>
        /// <param name="user_id"></param>
        /// <param name="eProductWarrantyType"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="eWarrantyProcess"></param>
        /// <returns></returns>
        [HttpGet("process-warranties")]
        public async Task<ReponseDataDto<ResultProductWarrantyDto>> GetListProcessWarranty(int page, int perPage, string filter = null,
                                                                Guid? user_id = null, EProductWarrantyType? eProductWarrantyType = null,
                                                                DateTime? fromDate = null, DateTime? toDate = null,
                                                                EWarrantyProcess? eWarrantyProcess = null)
        {
            return await _orderWarrantyAppService.GetListProcessWarranty(page, perPage, filter, user_id, eProductWarrantyType, fromDate, toDate, eWarrantyProcess);
        }
    }
}
