using System;
using System.Collections.Generic;
using System.Text;
using TaxFiguresCalculator.Core.Entities;

namespace TaxFiguresCalculator.Core.Validations
{
    public class ValidateTransactionAccount : ValidationBase<Transaction>
    {
        public readonly List<Account> _accounts;
        public ValidateTransactionAccount(Transaction context,List<Account>Accounts)
       : base(context)
        {
            _accounts = Accounts;
        }
        public override bool Requirement
        {
            get { return _accounts.Find(x => x.Id == Context.AccountId) != null; }
        }

        public override string Message
        {
            get
            {
                return string.Format("The Account with ID: '{0}' with Description: '{1}' does not exist for current Customer.",
                    Context.AccountId, Context.Description);
            }
        }
    }
}
