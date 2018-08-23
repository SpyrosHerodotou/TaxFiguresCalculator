using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaxFiguresCalculator.MVC.ViewModels
{
    public class CustomerIndexViewModel
    {
        [Required]
        public IEnumerable<SelectListItem> Customers { get; set; }
        public string CustomerId { get; set; }
    }
}
