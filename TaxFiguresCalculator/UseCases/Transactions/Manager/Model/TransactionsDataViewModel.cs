using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxFiguresCalculator.MVC.ViewModels
{
    public class TransactionsDataViewModel
    {
        public int Customerid { get; set; }
        public string CustomerName { get; set; }
        public int TotalAcctouns { get; set; }
        public int TotalTransactions { get; set; }
        public List<TransactionViewModel> transactionViewModels { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}
