using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Entities
{
    class TransactionEntity
    {
        public class Total
        {
            private int _transactionId = 123;
            private string _accountId = "31wdada";
            private string description = "This is transaction Description"
            private decimal _amount = 1.23m;
            private int _testQuantity = 2;

            [Fact]
            public void AddsTransactionInTransactionTable()
            {
                var transaction = new transactions
                basket.AddItem(_testCatalogItemId, _testUnitPrice, _testQuantity);

                var firstItem = basket.Items.Single();
                Assert.Equal(_testCatalogItemId, firstItem.CatalogItemId);
                Assert.Equal(_testUnitPrice, firstItem.UnitPrice);
                Assert.Equal(_testQuantity, firstItem.Quantity);
            }

            [Fact]
            public void IncrementsQuantityOfItemIfPresent()
            {
                var basket = new Basket();
                basket.AddItem(_testCatalogItemId, _testUnitPrice, _testQuantity);
                basket.AddItem(_testCatalogItemId, _testUnitPrice, _testQuantity);

                var firstItem = basket.Items.Single();
                Assert.Equal(_testQuantity * 2, firstItem.Quantity);
            }

            [Fact]
            public void KeepsOriginalUnitPriceIfMoreItemsAdded()
            {
                var basket = new Basket();
                basket.AddItem(_testCatalogItemId, _testUnitPrice, _testQuantity);
                basket.AddItem(_testCatalogItemId, _testUnitPrice * 2, _testQuantity);

                var firstItem = basket.Items.Single();
                Assert.Equal(_testUnitPrice, firstItem.UnitPrice);
            }

            [Fact]
            public void DefaultsToQuantityOfOne()
            {
                var basket = new Basket();
                basket.AddItem(_testCatalogItemId, _testUnitPrice);

                var firstItem = basket.Items.Single();
                Assert.Equal(1, firstItem.Quantity);
            }
        }
}
