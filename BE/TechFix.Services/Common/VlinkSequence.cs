using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TechFix.Common.AppSetting;
using TechFix.EntityModels;
using TechFix.Services.ScheduleServices;

namespace TechFix.Services.Common
{
	public class VlinkSequence
	{
		private DataContext _context;
		private IMapper _mapper;
		private AppSettings _appSettings;
		private readonly ILogger<AutomationServices> _logger;

		public VlinkSequence(DataContext context, IMapper mapper, IOptions<AppSettings> appSettings, ILogger<AutomationServices> logger)
		{
			_context = context;
			_mapper = mapper;
			_appSettings = appSettings.Value;
			_logger = logger;
		}

    //    private int GetSequence(string sequenceName, int defaultNumber)
    //    {
    //        var result = _context.GetNextSequenceValue(sequenceName);

    //        if (result == null || result == 0)
    //        {
    //            result = defaultNumber;
				//var sequence = new EntityModels.VlinkSequence
    //            {
    //                Value = defaultNumber,
    //                SequenceName = sequenceName
    //            };
    //            _context.VlinkSequence.Add(sequence);
    //            _context.SaveChanges();
    //        }

    //        return (int) result;
    //    }

  //      public string GetNextVlinkId()
		//{
		//	var codeDate = GetCodeDate();
		//	var nextVlinkId = GetSequence(SequenceName.VlinkId, 10000);
		//	return $"V{codeDate}{nextVlinkId}";
		//}

		//public int GetNextCartId()
		//{
		//	return GetSequence(SequenceName.CartId, 54321);
		//}

		//public string GetNextSavingsAccountNumber()
		//{
		//	var number = GetSequence(SequenceName.SavingsAccountNumber, 1000000);
		//	var codeDate = GetCodeDate();
		//	return $"SV_00{codeDate}{number}";
		//}

		//public string GetNextTransactionId()
		//{
		//	var number = GetSequence($@"{SequenceName.TransactionId}", 500000);
		//	var codeDate = GetCodeDate();
		//	return $"{codeDate}{number}";
		//}

  //      public string GetCreditLendingCode()
  //      {
  //          var number = GetSequence($@"{SequenceName.CreditLending}", 500);
  //          var codeDate = GetCodeDate();
  //          return $"CL_{codeDate}{number}";
  //      }

  //      public string GetEventTicketCode()
  //      {
  //          var number = GetSequence($@"{SequenceName.EventTicket}", 500001);
  //          return $"{number}";
  //      }

        private string GetCodeDate()
		{
			return DateTime.Now.ToString("yyMM");
		}

		public class SequenceName
		{
			public const string VlinkId = "VLINK_ID";
			public const string CartId = "CART_ID";
			public const string SavingsAccountNumber = "SAVINGS_ACCOUNT_NUMBER";
			public const string TransactionId = "TRANSACTION_ID";
			public const string CreditLending = "CREDIT_LENDING_ID";
			public const string EventTicket = "EVENT_TICKET";
		}
	}
	
}
