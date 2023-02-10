using Managerment.CustomCodes;
using Managerment.Customers;
using Managerment.CustomerTypes;
using Managerment.DetailProductRepairs;
using Managerment.Enums;
using Managerment.ProcessWarranties;
using Managerment.ProductRepairs;
using Managerment.ProductWarranties;
using Managerment.Shares;
using Managerment.Stores;
using Managerment.Users;
using Managerment.WarrantyProcesss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Managerment.OrderWarranties
{
    [RemoteService(IsEnabled = false)]
    public class OrderWarrantyAppService : ManagermentAppService, IOrderWarrantyAppService
    {
        private readonly IOrderWarrantyRepository _orderWarrantyRepository;
        private readonly OrderWarrantyManager _orderWarrantyManager;
        private readonly CustomCodeAppService _customCodeAppService;
        private readonly IProductWarrantyRepository _productWarrantyRepository;
        private readonly IDetailProductRepairAppService _detailProductRepairAppService;
        private readonly ProductWarrantyManager _productWarrantyManager;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IProcessWarrantyRepository _processWarrantyRepository;
        private readonly ProcessWarrantyManager _processWarrantyManager;

        public OrderWarrantyAppService(
            IOrderWarrantyRepository orderWarrantyRepository,
            OrderWarrantyManager orderWarrantyManager,
            CustomCodeAppService customCodeAppService,
            IProductWarrantyRepository productWarrantyRepository,
            IDetailProductRepairAppService detailProductRepairAppService,
            ProductWarrantyManager productWarrantyManager,
            ICustomerRepository customerRepository,
            IUserRepository userRepository,
            IStoreRepository storeRepository,
            IProcessWarrantyRepository processWarrantyRepository,
            ProcessWarrantyManager processWarrantyManager)
        {
            _orderWarrantyRepository = orderWarrantyRepository;
            _orderWarrantyManager = orderWarrantyManager;
            _customCodeAppService = customCodeAppService;
            _productWarrantyRepository = productWarrantyRepository;
            _detailProductRepairAppService = detailProductRepairAppService;
            _productWarrantyManager = productWarrantyManager;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _storeRepository = storeRepository;
            _processWarrantyRepository = processWarrantyRepository;
            _processWarrantyManager = processWarrantyManager;
        }

        private async Task<ReponseCreateOrderWarrantyDto> CreateOrderWarranty(CreateOrderWarrantyDefaultDto createOrderWarrantyDefaultDto)
        {
            var order_warranty = _orderWarrantyManager.CreateAsync(createOrderWarrantyDefaultDto.OW_code, createOrderWarrantyDefaultDto.Text,
                                                                    createOrderWarrantyDefaultDto.ID_cus, createOrderWarrantyDefaultDto.ID_user);
            await _orderWarrantyRepository.InsertAsync(order_warranty);

            return ObjectMapper.Map<OrderWarranty, ReponseCreateOrderWarrantyDto>(order_warranty);
        }

        public async Task CreateAsync(CreateOrderWarrantyDto input)
        {
            try
            {
                //order_repair code
                var productCode = await _customCodeAppService.GenerateCode("BH");
                //tạo order warranty 
                CreateOrderWarrantyDefaultDto createOrderWarrantyDefaultDto = new CreateOrderWarrantyDefaultDto()
                {
                    OW_code = productCode,
                    ID_cus = input.ID_cus,
                    ID_user = input.ID_user,
                    Text = input.Text,
                };

                var order_warranty = await CreateOrderWarranty(createOrderWarrantyDefaultDto);

                await _customCodeAppService.UpdateAsync("BH");

                ///tạo process_warranty
                var process_warranty = _processWarrantyManager.CreateAsync("", DateTime.Now, 0 ,EWarrantyProcess.Under_warranty, "", order_warranty.Id);

                await _processWarrantyRepository.InsertAsync(process_warranty);

                ////từ id order_warranty tạo những sản phẩm warranty
                foreach (var productDto in input.Product_warranties)
                {
                    var detail_warranty_product_map = ObjectMapper.Map<DetailProductRepairCreateOrderDetailDto, CreateDetailProductRepairDto>(productDto.Detail_product_warranty);

                    var detail_product = await _detailProductRepairAppService.CreateAsync(detail_warranty_product_map);

                    // tạo product prepair
                    var product_warranty = _productWarrantyManager.CreateAsync(productDto.PW_code, productDto.PW_name, productDto.PW_date_finish, productDto.PW_status
                                            , productDto.Product_warranty_type, productDto.Warranty_times, productDto.Total_count, detail_product.Id,
                                            order_warranty.Id);

                    await _productWarrantyRepository.InsertAsync(product_warranty);
                }
            }
            catch(Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
          
        }

        /// <summary>
        /// lấy danh sách đơn bảo hành
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ReponseDataDto<ResultLstOrderWarrantyDto>> GetListAsync(int page, int perPage, string filter = null,
                                                                DateTime? fromDate = null, DateTime? toDate = null,
                                                                ECustomerTypeEnum? e_customer_type = null)
        {
            var order_warranty_join = await _orderWarrantyRepository.GetQueryableAsync();
            var customer_join = await _customerRepository.GetQueryableAsync();
            var user_join = await _userRepository.GetQueryableAsync();
            var store_join = await _storeRepository.GetQueryableAsync();
            var product_warranty_join = await _productWarrantyRepository.GetQueryableAsync();

            var query = from order_warranty in order_warranty_join
                        join customer in customer_join on order_warranty.ID_cus equals customer.Id
                        join user in user_join on order_warranty.ID_user equals user.Id
                        join store in store_join on user.Id_store equals store.Id
                        join product_warranty in product_warranty_join on order_warranty.Id equals product_warranty.ID_order_warranty
                        where (filter == null || customer.C_name.Contains(filter) && customer.C_code.Contains(filter) && customer.C_phone.Contains(filter))
                        && fromDate == null && toDate == null || fromDate.GetValueOrDefault() <= order_warranty.CreationTime && order_warranty.CreationTime <= toDate.GetValueOrDefault()
                        && e_customer_type == null || customer.Customer_type == e_customer_type
                        select new ListOrderWarrantyDto
                        {
                            Customer_name = customer.C_name,
                            Customer_type = customer.Customer_type,
                            Customer_type_name = EnumAppService.GetNameEnum<ECustomerTypeEnum>(customer.Customer_type),
                            Phone = customer.C_phone,
                            Received_time = order_warranty.CreationTime,
                            Store_name = store.Store_name,
                            OW_code = order_warranty.OW_code,
                            User_name = user.U_name,
                            Total_count = product_warranty.Total_count
                        };
            int pageCout = !query.Any() ? 0 : query.Count() / perPage + 1;

            var query_lst = query.OrderByDescending(x => x.Received_time).Skip((page - 1) * perPage).Take(perPage).ToList();

            ResultLstOrderWarrantyDto result = new()
            {
                Data = query_lst,
            };

            return new ReponseDataDto<ResultLstOrderWarrantyDto>()
            {
                Count = query.Count(),
                Page = page,
                Per_page = perPage,
                Total_pages = pageCout,
                Data = result
            };
        }

        public async Task<ReponseDataDto<ResultProductWarrantyDto>> GetListProcessWarranty(int page, int perPage, string filter = null,
                                                                Guid? user_id = null, EProductWarrantyType? eProductWarrantyType = null,
                                                                DateTime? fromDate = null, DateTime? toDate = null,
                                                                EWarrantyProcess? eWarrantyProcess = null)
        {
            var order_warranty_join = await _orderWarrantyRepository.GetQueryableAsync();
            var customer_join = await _customerRepository.GetQueryableAsync();
            var user_join = await _userRepository.GetQueryableAsync();
            var process_warranty_join = await _processWarrantyRepository.GetQueryableAsync();
            var product_warranty_join = await _productWarrantyRepository.GetQueryableAsync();
            var query = from order_warranty in order_warranty_join
                        join customer in customer_join on order_warranty.ID_cus equals customer.Id
                        join user in user_join on order_warranty.ID_user equals user.Id
                        join product_warranty in product_warranty_join on order_warranty.Id equals product_warranty.ID_order_warranty
                        join process_warranty in process_warranty_join on order_warranty.Id equals process_warranty.Order_warranty_id
                        where (filter == null || customer.C_name.Contains(filter) && customer.C_code.Contains(filter) && customer.C_phone.Contains(filter))
                        && fromDate == null && toDate == null || fromDate.GetValueOrDefault() <= order_warranty.CreationTime && order_warranty.CreationTime <= toDate.GetValueOrDefault()
                        && eProductWarrantyType == null || product_warranty.Product_warranty_type == eProductWarrantyType
                        && eWarrantyProcess == null || process_warranty.Warranty_process == eWarrantyProcess
                        select new ProcessWarrantyDto
                        {
                            Customer_name = customer.C_name,
                            Order_warranty_code = order_warranty.OW_code,
                            Product_warranty_type = product_warranty.Product_warranty_type,
                            Date_received = order_warranty.CreationTime,
                            PW_code = product_warranty.PW_code,
                            PW_name = product_warranty.PW_name,
                            PW_status = product_warranty.PW_status,
                            Warranty_date = process_warranty.Warranty_date,
                            Warranty_process = process_warranty.Warranty_process,
                            Warranty_status = process_warranty.Warranty_status,
                            Warranty_times = product_warranty.Warranty_times,
                            Warranty_price = process_warranty.Warranty_price,
                            User_warranty = process_warranty.User_warranty
                        };
            int pageCout = !query.Any() ? 0 : query.Count() / perPage + 1;

            int warranty_count = 0;

            int warranty_done = 0;

            int return_customer = 0;

            decimal total_warranty_cost = 0;

            foreach (var warranty in query.ToList())
            {
                warranty_count += warranty.Warranty_times;

                if (warranty.Warranty_process == EWarrantyProcess.Warranty_done)
                {
                    warranty_done += warranty.Warranty_times;
                }
                if (warranty.Warranty_process == EWarrantyProcess.Return_customer)
                {
                    return_customer += warranty.Warranty_times;
                }
                total_warranty_cost += warranty.Warranty_price;
            }

            ResultProductWarrantyDto resultProductWarrantyDto = new()
            {
                Warranty_count = warranty_count,
                Warranty_done = warranty_done,
                Warranty_return_customer = return_customer,
                Total_warranty_cost = total_warranty_cost,
                Warranty_lst = query.ToList()
            };
            return new ReponseDataDto<ResultProductWarrantyDto>()
            {
                Count = query.Count(),
                Page = page,
                Per_page = perPage,
                Total_pages = pageCout,
                Data = resultProductWarrantyDto
            };
        }

        public Task UpdateAsync(Guid id, CreateOrderWarrantyDto input)
        {
            throw new NotImplementedException();
        }
    }
}
