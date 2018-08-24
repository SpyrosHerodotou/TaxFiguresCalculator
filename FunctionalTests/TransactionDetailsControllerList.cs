using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TaxFiguresCalculator;
using TaxFiguresCalculator.MVC.ViewModels;
using Xunit;

namespace FunctionalTests
{
    public class TransactionDetailsControllerList : IClassFixture<CustomApplicationWebFactory<Startup>>
    {
        public TransactionDetailsControllerList(CustomApplicationWebFactory<Startup> factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task ReturnsFirst10TransactionsItems()
        {
            var response = await Client.GetAsync("/api/TransactionDetails/Index");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<TransactionsDataViewModel>(stringResponse);

            Assert.Equal(10, model.transactionViewModels.Count());
        }

        [Fact]
        public async Task ReturnsLast2CatalogItemsGivenPageIndex1()
        {
            var response = await Client.GetAsync("/api/TransactionDetails/Index?page=1");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<TransactionsDataViewModel>(stringResponse);

            Assert.Equal(2, model.transactionViewModels.Count());
        }
    }
}

