using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxFiguresCalculator.Core.Helpers
{
    public sealed class ValueObjectsHelper
    {

        public bool TryGetCurrencySymbol(string ISOCurrencySymbol, out string symbol)
        {
            symbol = CultureInfo
                .GetCultures(CultureTypes.AllCultures)
                .Where(c => !c.IsNeutralCulture)
                .Select(culture =>
                {
                    try
                    {
                        return new RegionInfo(culture.LCID);
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(ri => ri != null && ri.ISOCurrencySymbol == ISOCurrencySymbol)
                .Select(ri => ri.CurrencySymbol)
                .FirstOrDefault();
            return symbol != null;
        }
        public bool TryGetValidDecimal(string amountText )
        {
            var style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
            var cultureinfo = CultureInfo.InvariantCulture;
            return Decimal.TryParse(amountText,style,cultureinfo, out decimal amount);
        }
    }

}
