using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaxFiguresCalculator.Core.Entities;
using TaxFiguresCalculator.Core.Interfaces;
using TaxFiguresCalculator.Core.Repositories;
using TaxFiguresCalculator.Core.Services;
using TaxFiguresCalculator.MVC.Filters;
using TaxFiguresCalculator.MVC.Helpers;
using TaxFiguresCalculator.MVC.Interfaces;
using TaxFiguresCalculator.MVC.ViewModels;

namespace TaxFiguresCalculator.MVC.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly ICustomerService _CustomerService;
        private IHostingEnvironment _hostingEnvironment;
        private readonly IFileUploadService _fileUploadService;
        private readonly IRepository<Customer> _Customerrepository;

        private static int _CustomerId;
        public IActionResult Index(IFormCollection formcollection, int CustomerId)
        {
            if (formcollection["CustomerIdForUpload"].ToString() != "")
            {
                CustomerId = Convert.ToInt32(formcollection["CustomerIdForUpload"]);
            }
            _CustomerId = CustomerId;
            var Customers = _Customerrepository.ListAll();
            if (!Customers.Any(x => x.Id == CustomerId))
            {
                return RedirectToAction("Index", "TransactionPortal");
            }
            CustomerIndexViewModel CustomerIndexViewModel = new CustomerIndexViewModel()
            {
                CustomerId = formcollection["CustomerIdForUpload"]
            };
            return View("Index", CustomerIndexViewModel);
        }
        public FileUploadController(IFileUploadService fileUploadService,
            IRepository<Customer> Customerrepository)
        {
            _fileUploadService = fileUploadService;
            _Customerrepository = Customerrepository;
        }


        /// <summary>
        /// Upload Streaming Controller Action. Disabling form binding and reading stream.
        /// Dependency Injection for FileUploadService for Constructing/Validating/Submitting valid Transaction
        /// Data on DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DisableFormValueModelBindingAttribute]
        public async Task<IActionResult> UploadStreamingFile()
        {
            int CustomerId = _CustomerId;
            if (!ModelState.IsValid)
            {
                RedirectToAction("Index", new { CustomerId = CustomerId });
            }
            IEnumerable<FileUploadSummaryViewModel> fileUploadSummaries;

            // full path to file in temp location
            var filePath = Path.GetTempFileName();
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await FileUploadHelper.StreamFile(Request, stream);
                }

                var viewModel = await _fileUploadService.ValidateAndSaveTransactions(filePath, CustomerId);
                fileUploadSummaries = viewModel.Select(i => new FileUploadSummaryViewModel()
                {
                    RowNumber = i.RowNumber,
                    Message = i.Message,
                    CustomerId = _CustomerId

                }).ToList();
                if (fileUploadSummaries.Count() == 0)
                {
                    List<FileUploadSummaryViewModel> fileUploadSummariesList = new List<FileUploadSummaryViewModel>();
                    fileUploadSummariesList.Add(new FileUploadSummaryViewModel()
                    {
                        RowNumber = "",
                        Message = "All Transactions Have Been Successfully Imported!",
                        CustomerId = _CustomerId
                    });
                    fileUploadSummaries = fileUploadSummariesList;
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "FileUpload", new { CustomerId = CustomerId });
            }

            return View("UploadResolve", fileUploadSummaries);
        }
    }
}