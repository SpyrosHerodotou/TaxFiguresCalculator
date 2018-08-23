using System.Collections.Generic;
using System.Threading.Tasks;
using TaxFiguresCalculator.Core.Services.TransactionManageAggregate;

namespace TaxFiguresCalculator.Core.Interfaces
{
   public interface IFileUploadService
    {
        Task<IEnumerable<FileUploadSummary>> UploadFiles(string urlPath,int customerId);

    }
}
