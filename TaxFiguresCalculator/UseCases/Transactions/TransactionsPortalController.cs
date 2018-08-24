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
    public class TransactionsPortalController : Controller
    {
        private readonly ICustomerService _CustomerService;

        /// <summary>
        /// Handles Customer Related Actions
        /// </summary>
        /// <param name="CustomerService"></param>
        public TransactionsPortalController(ICustomerService CustomerService)
        {
            _CustomerService = CustomerService;

        }

        /// <summary>
        /// Retreives Customer Index Model
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var CustomerModel = await _CustomerService.GetAllCustomers();

            CustomerIndexViewModel CustomerList = new CustomerIndexViewModel();
            CustomerList.Customers = CustomerModel;

            return View(CustomerList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
