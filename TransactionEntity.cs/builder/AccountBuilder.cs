using System;
using System.Collections.Generic;
using System.Text;
using TaxFiguresCalculator.Core.Entities;

namespace TransactionEntity.cs.builder
{
   public class AccountBuilder
    {
        private Account _accounts;
        public string TestAccountIdUnit => "A01231";
        public string TestAccountTypeUnit => "Test Account";
        public string TestAccountDescriptionUnit => "TestingAccounts";

        public AccountBuilder()
        {
            _accounts = WithDefaultValues();
        }

        public Account Build()
        {
            return _accounts;
        }

        public Account WithDefaultValues()
        {
            _accounts = new Account();
            _accounts.Id = TestAccountIdUnit;
            _accounts.AccountType = TestAccountTypeUnit;
            _accounts.AccountDescription = TestAccountDescriptionUnit;
            _accounts.Transactions = new List<Transaction> { new TransactionBuilder().WithDefaultValues() };
            return _accounts;
        }
    }
}
