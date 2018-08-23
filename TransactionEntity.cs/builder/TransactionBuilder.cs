
using System;
using System.Collections.Generic;
using System.Text;
using TaxFiguresCalculator.Core.Entities;

namespace TransactionEntity.cs.builder
{
    public class TransactionBuilder
    {
        private Transaction _transaction;
        public long TestTransactionIdUnit;
        public string TestAccountIdUnit => "A01231";
        public decimal TestAmountUnit => 45;
        public string TestDescriptionUnit => "Testing Description";
        public string TestCurrencyCodeUnit => "GBP";

        public TransactionBuilder()
        {
            _transaction = WithDefaultValues();
        }

        public Transaction Build()
        {
            return _transaction;
        }

        public Transaction WithDefaultValues()
        {
            _transaction = new Transaction();
            _transaction.AccountId = TestAccountIdUnit;
            _transaction.Amount = TestAmountUnit;
            _transaction.CurrencyCode = TestCurrencyCodeUnit;
            _transaction.Description = TestDescriptionUnit;
            _transaction.Id = TestTransactionIdUnit;
            return _transaction;
        }

    }
}
