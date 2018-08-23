using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaxFiguresCalculator.MVC.ViewModels
{
    public class TransactionViewModel
    {
        public int CustomerId { get; set; }

        [StringLength(20, MinimumLength = 20)]
        [Display(Name = "Transaction ID:")]
        public String TransactionId { get; set; }
        [Display(Name = "Account Name:")]
        public String accountName { get; set; }
        [Display(Name = "Description:")]
        public String Description { get; set; }

        [StringLength(3, MinimumLength = 3)]
        [Display(Name = "Currency:")]
        public String Currency { get; set; }
        [Display(Name = "Amount:")]
        public String Amount { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}
