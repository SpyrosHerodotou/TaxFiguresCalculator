using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaxFiguresCalculator.Core.Entities;
using TaxFiguresCalculator.Core.Interfaces;
using TaxFiguresCalculator.Core.Repositories;
using TaxFiguresCalculator.Core.Services;
using TaxFiguresCalculator.MVC.Interfaces;

namespace TaxFiguresCalculator.MVC.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IAsyncRepository<Customer> _customerrepository;

        public CustomerService(
            IAsyncRepository<Customer> customerrepository)
        {
            _customerrepository = customerrepository;
        }
        public async Task<IEnumerable<SelectListItem>> GetAllCustomers()
        {
            var customers = await _customerrepository.ListAllAsync();
            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "Select Customer", Selected = true }
            };
            foreach (Customer customer in customers)
            {
                items.Add(new SelectListItem() { Value = customer.Id.ToString(), Text = customer.Name });
            }
            return items;
        }
    }
}
