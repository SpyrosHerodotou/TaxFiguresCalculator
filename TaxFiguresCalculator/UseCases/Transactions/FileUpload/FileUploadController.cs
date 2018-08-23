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
        private readonly ICustomerService _customerService;
        private IHostingEnvironment _hostingEnvironment;
        private readonly IFileUploadService _fileUploadService;
        private readonly IRepository<Customer> _customerrepository;

        private static int _customerId;
        public IActionResult Index(IFormCollection formcollection, int customerId)
        {
            if (formcollection["CustomerIdForUpload"].ToString() != "")
            {
                customerId = Convert.ToInt32(formcollection["CustomerIdForUpload"]);
            }
            _customerId = customerId;
            var customers = _customerrepository.ListAll();
            if (!customers.Any(x => x.Id == customerId))
            {
                return RedirectToAction("Index", "Customer");
            }
            CustomerIndexViewModel customerIndexViewModel = new CustomerIndexViewModel()
            {
                CustomerId = formcollection["CustomerIdForUpload"]
        };
            return View("Index", customerIndexViewModel);
        }
        public FileUploadController(IFileUploadService fileUploadService,
            IRepository<Customer> customerrepository)
        {
            _fileUploadService = fileUploadService;
            _customerrepository = customerrepository;
        }
        [HttpPost]
        [DisableFormValueModelBindingAttribute]
        public async Task<IActionResult> UploadStreamingFile()
        {
            int CustomerId = _customerId;
            if (!ModelState.IsValid)
            {
                RedirectToAction("Index", new { customerId = CustomerId });
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

                var viewModel = await _fileUploadService.UploadFiles(filePath, CustomerId);
                fileUploadSummaries = viewModel.Select(i => new FileUploadSummaryViewModel()
                {
                    RowNumber = i.RowNumber,
                    Message = i.Message,
                    CustomerId = _customerId

                }).ToList();
                if (fileUploadSummaries.Count() == 0)
                {
                    List<FileUploadSummaryViewModel> fileUploadSummariesList = new List<FileUploadSummaryViewModel>();
                    fileUploadSummariesList.Add(new FileUploadSummaryViewModel()
                    {
                        RowNumber = "",
                        Message = "All Transactions Have Been Successfully Imported!",
                        CustomerId = _customerId
                    });
                    fileUploadSummaries = fileUploadSummariesList;
                }
            }catch(Exception ex)
            {
                return RedirectToAction("Index", "FileUpload", new { customerId = CustomerId });
            }
            return View("UploadResolve",fileUploadSummaries);
        }
    }
}