using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxFiguresCalculator.Core.Entities;
using TaxFiguresCalculator.Core.Helpers;
using TaxFiguresCalculator.Core.Interfaces;
using TaxFiguresCalculator.Core.Repositories;
using TaxFiguresCalculator.Core.Service;
using TaxFiguresCalculator.Core.Services.TransactionManageAggregate;
using TaxFiguresCalculator.Core.Validations;

namespace TaxFiguresCalculator.Core.Services
{
    public class FileUploaderService : IFileUploadService
    {
        private readonly IAsyncRepository<Transaction> _transactionRepository;
        private readonly IAsyncRepository<Transaction> _asyncTransactionRepository;
        //private readonly IAsyncRepository<Account> _asyncAccountsRepository;
        private readonly IAccountRepository _asyncAccountsRepository;



        public FileUploaderService(IAsyncRepository<Transaction> transactionRepository,
            IAsyncRepository<Transaction> asyncTransactionRepository,
            IAccountRepository asyncAccountsRepository)
        {
            _transactionRepository = transactionRepository;
            _asyncTransactionRepository = asyncTransactionRepository;
            _asyncAccountsRepository = asyncAccountsRepository;
        }



        /// <summary>
        ///Retrieves the URL of the submitted temp File and creates IDisposable ExcelPackage in Memory.
        ///Loops Excel Row and Validates row data.
        ///Validated row data through the 
        ///
        /// </summary>
        /// <param name="urlPath"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<FileUploadSummary>> ValidateAndSaveTransactions(string urlPath, int clientId)
        {
            FileInfo file = new FileInfo(urlPath);
            ValidateCurrencyCodes _validateCurrencyCodes;
            ValidateTransactionAccount _validateTransactionAccount;
            ValueObjectsHelper valueObjectsHelper = new ValueObjectsHelper();
            List<FileUploadSummary> fileUploadSummaries = new List<FileUploadSummary>();

            var accounts = await _asyncAccountsRepository.GetAccountsByCustomer(clientId);

            try
            {
                List<Transaction> transactionsList = new List<Transaction>();

                //Creates IDisposable Excel Package
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    FileUploadSummary fileUploadSummary = new FileUploadSummary();
                    StringBuilder sb = new StringBuilder();
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;

                    //Calls Header Validator Method For Exchel Header Validation
                    if (!AreExcelColumnsHeadersValid(worksheet.Cells[1, 1].Value ?? "",
                        worksheet.Cells[1, 2].Value ?? "",
                        worksheet.Cells[1, 3].Value.ToString() ?? "",
                        worksheet.Cells[1, 4].Value.ToString() ?? ""))
                    {
                        fileUploadSummaries.Add(GetExceptionSummaryMessage("1", "NO VALID HEADERS"));
                        return fileUploadSummaries;
                    }

                    for (int row = 2; row <= rowCount; row++)
                    {
                        //Validates if Rows are Not Null or Contain Empty Values
                        Transaction transactions = new Transaction();
                        if (worksheet.Cells[row, 1].Value == null || String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()))
                        {
                            fileUploadSummaries.Add(GetExceptionSummaryMessage(row.ToString(), "No Value for Account Number"));
                            continue;
                        }

                        if ((worksheet.Cells[row, 2].Value == null || String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString())))
                        {
                            fileUploadSummaries.Add(GetExceptionSummaryMessage(row.ToString(), "No Value for Transaction Description"));
                            continue;
                        }

                        if (worksheet.Cells[row, 3].Value == null || String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()))
                        {
                            fileUploadSummaries.Add(GetExceptionSummaryMessage(row.ToString(), "No Value on Currency Code Row"));
                            continue;
                        }

                        if (worksheet.Cells[row, 4].Value != null && !String.IsNullOrEmpty(worksheet.Cells[row, 4].Value.ToString()))
                        {
                            ///Validates If Row Decimal Value is Valid Decimal
                            if (!valueObjectsHelper.TryGetValidDecimal(worksheet.Cells[row, 4].Value.ToString()))
                            {
                                fileUploadSummaries.Add(GetExceptionSummaryMessage(row.ToString(), "Not a Valid Decimal Amount for Transactions"));
                                continue;
                            }
                        }
                        else
                        {
                            fileUploadSummaries.Add(GetExceptionSummaryMessage(row.ToString(), "No Value for Transaction Amount"));
                            continue;
                        }

                        transactions.AccountId = worksheet.Cells[row, 1].Value.ToString();
                        transactions.Description = worksheet.Cells[row, 2].Value.ToString();
                        transactions.CurrencyCode = worksheet.Cells[row, 3].Value.ToString();
                        transactions.Amount = Convert.ToDecimal(worksheet.Cells[row, 4].Value.ToString());

                        //Currency and Account Validation
                        _validateCurrencyCodes = new ValidateCurrencyCodes(transactions);
                        _validateTransactionAccount = new ValidateTransactionAccount(transactions, accounts);

                        //validate Currency Through Currency Validator Class
                        if (!_validateCurrencyCodes.IsValid)
                        {
                            fileUploadSummary.RowNumber = row.ToString();
                            fileUploadSummary.Message = _validateCurrencyCodes.Message;
                            sb.Append("Line: " + row.ToString() + _validateCurrencyCodes.Message);
                            fileUploadSummaries.Add(GetExceptionSummaryMessage(row.ToString(), _validateCurrencyCodes.Message));

                            continue;
                        }

                        //Validate Transaction Account through Account Validator Class
                        if (!_validateTransactionAccount.IsValid)
                        {
                            fileUploadSummaries.Add(GetExceptionSummaryMessage(row.ToString(), _validateTransactionAccount.Message));
                            continue;
                        }
                        transactionsList.Add(transactions);
                    }
                }

                //Calls EFCore.Bulks Extensions handling bulk Updates.
                _asyncTransactionRepository.UpdateBulkAsync(transactionsList);

                //deletes file
                file.Delete();
            }
            catch (Exception ex)
            {


            }
            return fileUploadSummaries;
        }


        /// <summary>
        /// Checks if File Contains Valid Header.
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Descreption"></param>
        /// <param name="CurrencyCode"></param>
        /// <param name="Amount"></param>
        /// <returns></returns>
        public bool AreExcelColumnsHeadersValid(object Account, object Descreption, object CurrencyCode, object Amount)
        {
            if (String.IsNullOrEmpty(Account.ToString()) || Account.ToString() != "Account")
            {
                return false;
            }
            if (String.IsNullOrEmpty(Descreption.ToString()) || Descreption.ToString() != "Description")
            {
                return false;
            }
            if (String.IsNullOrEmpty(CurrencyCode.ToString()) || CurrencyCode.ToString() != "Currency Code")
            {
                return false;
            }
            if (String.IsNullOrEmpty(Amount.ToString()) || Amount.ToString() != "Amount")
            {
                return false;
            }
            return true;
        }
        public FileUploadSummary GetExceptionSummaryMessage(string row, string message)
        {
            return new FileUploadSummary { RowNumber = row, Message = message };
        }
    }
}
