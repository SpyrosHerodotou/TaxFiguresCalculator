using System;
using System.Collections.Generic;
using System.Text;
using TaxFiguresCalculator.Core.Entities;

namespace TransactionEntity.cs.builder
{
    public class clientBuilder
    {
        private Customer _clients;
        public long TestclientIdUnit => 3;
        public string TestclientNameUnit => "SpyrosTest";
        public string TestclientSurnameUnit => "HerodotouTest";
        public string TestclientTelephoneUnit => "0034987323";
        public string TestclientEmailUnit => "spyrosherod@gmail.com";

        public clientBuilder()
        {
            _clients = WithDefaultValues();
        }

        public Customer Build()
        {
            return _clients;
        }

        public Customer WithDefaultValues()
        {

            _clients = new Customer();
            _clients.Id = TestclientIdUnit;
            _clients.Name = TestclientNameUnit;
            _clients.Surname = TestclientSurnameUnit;
            _clients.EmailAddress = TestclientEmailUnit;
            _clients.Telephone = TestclientTelephoneUnit;
            _clients.Accounts = new List<Account>() { new AccountBuilder().WithDefaultValues() };
            return _clients;
        }

    }
}
