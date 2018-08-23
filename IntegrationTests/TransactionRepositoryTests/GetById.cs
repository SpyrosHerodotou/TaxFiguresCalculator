using Microsoft.EntityFrameworkCore;
using System.Linq;
using TaxFiguresCalculator.Core.Repositories;
using TaxFiguresCalculator.Infrastracture.DataAccess;
using TaxFiguresCalculator.Infrastructure;
using TaxFiguresCalculator.Infrastructure.DataAccess;
using TransactionEntity.cs.builder;
using Xunit;
using Xunit.Abstractions;


namespace IntegrationTests.TransactionRepositoryTests
{

namespace Microsoft.eShopWeb.IntegrationTests.Repositories.OrderRepositoryTests
    {
        public class GetById
        {
            private readonly Tax_Figures_CalculatorContext _Figures_CalculatorContext;
            private readonly TaxFiguresCalculator.Core.Repositories.ITransactionRepository _transactionRepository;
            private CustomerBuilder customerBuilder { get; } = new CustomerBuilder();
            private readonly ITestOutputHelper _output;
            public GetById(ITestOutputHelper output)
            {
                _output = output;
                var dbOptions = new DbContextOptionsBuilder<Tax_Figures_CalculatorContext>()
                    .UseSqlServer("Server=WCYSH185195-U9B; Database=Tax_Figures_Calculator;User Id = sa; Password = admin")
                    .Options;
                _Figures_CalculatorContext = new Tax_Figures_CalculatorContext(dbOptions);
                _transactionRepository = new TaxFiguresCalculator.Infrastructure.DataAccess.ITransactionRepository(_Figures_CalculatorContext);
            }

            [Fact]
            public void GetExistingTransaction()
            {
                var existingTransaction = customerBuilder.WithDefaultValues().Accounts.FirstOrDefault().Transactions.FirstOrDefault();
                _Figures_CalculatorContext.Transaction.Add(existingTransaction);
                _Figures_CalculatorContext.SaveChanges();
                long transactionId = existingTransaction.Id;
                string accountId = existingTransaction.AccountId;
                _output.WriteLine($"OrderId: {existingTransaction}");

                var customerFromRepo = _transactionRepository.GetByIdComposite(transactionId, accountId);
                Assert.Equal(existingTransaction.Id, customerFromRepo.Id);
            }
        }
    }
}
