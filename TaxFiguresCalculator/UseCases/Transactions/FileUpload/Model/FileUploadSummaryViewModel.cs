using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaxFiguresCalculator.MVC.ViewModels
{
    public class FileUploadSummaryViewModel
    {
        [StringLength(20, MinimumLength = 20)]
        [Display(Name = "Row Number:")]
        public string RowNumber { get; set; }
        [Display(Name = "Validation Message:")]
        public String Message { get; set; }
        public int CustomerId { get; set; }

    }
}
