using System;
using System.Collections.Generic;
using System.Text;
using TaxFiguresCalculator.Core.Entities;
using TaxFiguresCalculator.Core.Helpers;

namespace TaxFiguresCalculator.Core.Service
{
    public class ValidateCurrencyCodes : ValidationBase<Transaction>
    {
        private readonly ValueObjectsHelper _valueObjectsHelper;
        public ValidateCurrencyCodes(Transaction context)
        : base(context)
        {
            _valueObjectsHelper = new ValueObjectsHelper();
        }

        public override bool Requirement
        {
            get { return _valueObjectsHelper.TryGetCurrencySymbol(Context.CurrencyCode, out string symbol); }
        }

        public override string Message
        {
            get
            {
                return string.Format("The Curency Code '{0}' for Account Number: '{2}' with Description '{1}' <b><i>is not a valid Currency Code</b></i>.",
                    Context.CurrencyCode, Context.Description, Context.AccountId);
            }
        }
    }
}
