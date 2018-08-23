using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxFiguresCalculator.Core.Repositories
{
   public interface IValidationRepository
    {
        bool IsValid { get; } // True when valid
        string Validate(); // Throws an exception when not valid
        string Message { get; } // The message when object is not valid
    }
}
