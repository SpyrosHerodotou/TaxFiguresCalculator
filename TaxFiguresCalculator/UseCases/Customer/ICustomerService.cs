using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxFiguresCalculator.MVC.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<SelectListItem>> GetAllCustomers();
    }
}
