using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxFiguresCalculator.MVC.ViewModels;

namespace TaxFiguresCalculator.MVC.Interfaces
{
   public interface IFileUploadViewService
    {
        Task<IActionResult> UploadFiles(DataFileViewModel TransactionsFileDescription);
    }
}
