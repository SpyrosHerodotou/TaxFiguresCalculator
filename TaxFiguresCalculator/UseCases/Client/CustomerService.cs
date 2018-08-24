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
        private readonly IAsyncRepository<Customer> _CustomerRepository;


        /// <summary>
        /// Handles Creation of Customer Related Models
        /// </summary>
        /// <param name="CustomerRepository"></param>
        public CustomerService(
            IAsyncRepository<Customer> CustomerRepository)
        {
            _CustomerRepository = CustomerRepository;
        }

        /// <summary>
        /// Retrieves all Customer Related Entities through Customer Repository Interface.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SelectListItem>> GetAllCustomers()
        {
            var Customers = await _CustomerRepository.ListAllAsync();

            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "Select Customer", Selected = true }
            };

            foreach (Customer Customer in Customers)
            {
                items.Add(new SelectListItem() { Value = Customer.Id.ToString(), Text = Customer.Name });
            }
            return items;
        }
    }
}
