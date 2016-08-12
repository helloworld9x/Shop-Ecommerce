using System;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Orders;
using Nop.Tests;
using NUnit.Framework;

namespace Nop.Data.Tests.Orders
{
    [TestFixture]
    public class GiftCardUsageHistoryPersistenceTests : PersistenceTest
    {
        [Test]
        public void Can_save_and_load_giftCardUsageHistory()
        {
            var gcuh = new GiftCardUsageHistory
            {
                UsedValue = 1.1M,
                CreatedOnUtc = new DateTime(2010, 01, 01),
                UsedWithOrder = GetTestOrder()
            };

            var fromDb = SaveAndLoadEntity(gcuh);
            fromDb.ShouldNotBeNull();
            fromDb.UsedValue.ShouldEqual(1.1M);
            fromDb.CreatedOnUtc.ShouldEqual(new DateTime(2010, 01, 01));

            fromDb.UsedWithOrder.ShouldNotBeNull();
        }


        protected Customer GetTestCustomer()
        {
            return new Customer
            {
                CustomerGuid = Guid.NewGuid(),
                AdminComment = "some comment here",
                Active = true,
                Deleted = false,
                CreatedOnUtc = new DateTime(2010, 01, 01),
                LastActivityDateUtc = new DateTime(2010, 01, 02)
            };
        }

        protected Order GetTestOrder()
        {
            return new Order
            {
                OrderGuid = Guid.NewGuid(),
                Customer = GetTestCustomer(),
                BillingAddress = new Address
                {
                    Country = new Country
                    {
                        Name = "United States",
                        TwoLetterIsoCode = "US",
                        ThreeLetterIsoCode = "USA",
                    },
                    CreatedOnUtc = new DateTime(2010, 01, 01),
                },
                Deleted = true,
                CreatedOnUtc = new DateTime(2010, 01, 01)
            };
        }
    }
}