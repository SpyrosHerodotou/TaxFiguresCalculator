using System;
using System.Collections.Generic;
using System.Text;
using TaxFiguresCalculator.Core.Entities;
using TaxFiguresCalculator.MVC.ViewModels;

namespace TransactionEntity.cs.builder
{
    public class CustomerBuilder
    {
        private Customer _customers;
        public long TestCustomerIdUnit => 3;
        public string TestCustomerNameUnit => "SpyrosTest";
        public string TestCustomerSurnameUnit => "HerodotouTest";
        public string TestCustomerTelephoneUnit => "0034987323";
        public string TestCustomerEmailUnit => "spyrosherod@gmail.com";

        public CustomerBuilder()
        {
            _customers = WithDefaultValues();
        }

        public Customer Build()
        {
            return _customers;
        }

        public Customer WithDefaultValues()
        {

            _customers = new Customer();
            _customers.Id = TestCustomerIdUnit;
            _customers.Name = TestCustomerNameUnit;
            _customers.Surname = TestCustomerSurnameUnit;
            _customers.EmailAddress = TestCustomerEmailUnit;
            _customers.Telephone = TestCustomerTelephoneUnit;
            _customers.Accounts = new List<Account>() { new AccountBuilder().WithDefaultValues() };
            return _customers;
        }

    }
}
