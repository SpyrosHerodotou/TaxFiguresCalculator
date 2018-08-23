using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaxFiguresCalculator.Models;
using TaxFiguresCalculator.MVC.Interfaces;
using TaxFiguresCalculator.MVC.ViewModels;

namespace TaxFiguresCalculator.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;

        }
        public async Task<IActionResult> Index()
        {
            var customerModel = await _customerService.GetAllCustomers();
            CustomerIndexViewModel customerList = new CustomerIndexViewModel();
            customerList.Customers = customerModel;
          
            return View(customerList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
