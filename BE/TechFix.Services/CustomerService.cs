using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechFix.Common;
using TechFix.Common.AppSetting;
using TechFix.Common.Constants;
using TechFix.Common.Helper;
using TechFix.Common.Paging;
using TechFix.EntityModels;
using TechFix.Services.Common;
using TechFix.Services.EmailServices;
using TechFix.TransportModels;

namespace TechFix.Services
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomerByFilter(PagingParams param);
        MemoryStream GenerateExcel(List<Customer> data);
        Task<bool> ImportExcel(IFormFile formFile, CancellationToken cToken);
    }
    public class CustomerService : ICustomerService
    {
        private DataContext _context;
        private IHelperService _helperService;
        public readonly IWebHostEnvironment _env;

        public CustomerService(IWebHostEnvironment env, DataContext context, IHelperService helperService)
        {
            _env = env;
            _context = context;
            _helperService = helperService;
        }

        public List<Customer> GetAllCustomerByFilter(PagingParams param)
        {
            var queryable = _context.Customers
                .Where(m => !m.IsDeleted);
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            return queryable.ToList();
        }
        public MemoryStream GenerateExcel(List<Customer> data)
        {
            var stream = new MemoryStream();
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("Customers");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(Color.Blue);
                const int startRow = 2;
                var row = startRow;

                //Create Headers and format them
                worksheet.Cells["A1"].Value = "Nhom_Khach_Hang";
                worksheet.Cells["B1"].Value = "Ma_Khach_Hang";
                worksheet.Cells["C1"].Value = "Ten_Khach_Hang";
                worksheet.Cells["D1"].Value = "So_Dien_Thoai";
                worksheet.Cells["E1"].Value = "Email";
                worksheet.Cells["F1"].Value = "Dia_Chi";
                worksheet.Cells["G1"].Value = "Ghi_Chu";
                worksheet.Cells["H1"].Value = "Ngay_Sinh";
                worksheet.Cells["I1"].Value = "Gioi_Tinh";

                row = 2;
                foreach (var customer in data)
                {
                    worksheet.Cells[row, 1].Value = customer.Team.Trim().Equals(ConstantValue.CUSTOMER_WHOLESALE) ? 1 : 0;
                    worksheet.Cells[row, 2].Value = !string.IsNullOrEmpty(customer.Code) ? customer.Code : string.Empty;
                    worksheet.Cells[row, 3].Value = !string.IsNullOrEmpty(customer.FullName) ? customer.FullName : string.Empty;
                    worksheet.Cells[row, 4].Value = !string.IsNullOrEmpty(customer.PhoneNumber) ? customer.PhoneNumber : string.Empty;
                    worksheet.Cells[row, 5].Value = !string.IsNullOrEmpty(customer.Email) ? customer.Email : string.Empty;
                    worksheet.Cells[row, 6].Value = !string.IsNullOrEmpty(customer.Address) ? customer.Address : string.Empty;
                    worksheet.Cells[row, 7].Value = !string.IsNullOrEmpty(customer.Note) ? customer.Note : string.Empty;
                    
                    row++;
                }

                // set some core property values
                xlPackage.Workbook.Properties.Title = "Customer List";
                xlPackage.Workbook.Properties.Author = "";
                xlPackage.Workbook.Properties.Subject = "Customer List";
                // save the new spreadsheet
                xlPackage.Save();
            }
            stream.Position = 0;
            return stream;
        }
        public async Task<bool> ImportExcel(IFormFile formFile, CancellationToken cToken)
        {
            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            var list = new List<Customer>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        //build up data
                        var customerTeam = _helperService.GetExcelValueByColumnName(worksheet, "Nhom_Khach_Hang", row);
                        var customerCode = _helperService.GetExcelValueByColumnName(worksheet, "Ma_Khach_Hang", row);
                        var customerName = _helperService.GetExcelValueByColumnName(worksheet, "Ten_Khach_Hang", row);
                        var customerPhone = _helperService.GetExcelValueByColumnName(worksheet, "So_Dien_Thoai", row);
                        var customerEmail = _helperService.GetExcelValueByColumnName(worksheet, "Email", row);
                        var customerAddress = _helperService.GetExcelValueByColumnName(worksheet, "Dia_Chi", row);
                        var customerNote = _helperService.GetExcelValueByColumnName(worksheet, "Ghi_Chu", row);
                        var customerDOB = _helperService.GetExcelValueByColumnName(worksheet, "Ngay_Sinh", row);
                        var customerGender = _helperService.GetExcelValueByColumnName(worksheet, "Gioi_Tinh", row);

                        //model
                        var existItem = _context.Customers.FirstOrDefault(x => x.Code.Equals(customerCode));
                        DateTime dob;
                        if (existItem != null)
                        {
                            existItem.Team = customerTeam == "1" ? ConstantValue.CUSTOMER_WHOLESALE : ConstantValue.CUSTOMER_FLT;
                            existItem.FullName = customerName;
                            existItem.Code = customerCode;
                            existItem.PhoneNumber = customerPhone;
                            existItem.Email = customerEmail;
                            existItem.Address = customerAddress;
                            existItem.Note = customerNote;
                            existItem.Birthday = DateTime.TryParse(customerDOB, out dob) ? dob : null;
                            existItem.Sex = customerGender;

                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var item = new Customer
                            {
                                FullName = customerName,
                                Code = customerCode,
                                Team = customerTeam == "1" ? ConstantValue.CUSTOMER_WHOLESALE : ConstantValue.CUSTOMER_FLT,
                                Address = customerAddress,
                                Birthday = DateTime.TryParse(customerDOB, out dob) ? dob : null,
                                Sex = customerGender,
                                Email = customerEmail,
                                Note = customerNote,
                                PhoneNumber = customerPhone,
                            };
                            list.Add(item);
                        }
                    }
                }
                if (list.Count == 0)
                {
                    _context.Customers.AddRange(list);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
        }
    }
}
