using System.Collections.Generic;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.News;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Stores;
using Nop.Core.Domain.Vendors;

namespace Nop.Services.Messages
{
    public interface IMessageTokenProvider
    {
        void AddStoreTokens(IList<Token> tokens, Store store, EmailAccount emailAccount);

        void AddOrderTokens(IList<Token> tokens, Order order, int languageId, int vendorId = 0);

        void AddOrderRefundedTokens(IList<Token> tokens, Order order, decimal refundedAmount);

        void AddShipmentTokens(IList<Token> tokens, Shipment shipment, int languageId);

        void AddRecurringPaymentTokens(IList<Token> tokens, RecurringPayment recurringPayment);

        void AddReturnRequestTokens(IList<Token> tokens, ReturnRequest returnRequest, OrderItem orderItem);

        void AddCustomerTokens(IList<Token> tokens, Customer customer);

        void AddVendorTokens(IList<Token> tokens, Vendor vendor);

        void AddNewsLetterSubscriptionTokens(IList<Token> tokens, NewsLetterSubscription subscription);

        void AddProductReviewTokens(IList<Token> tokens, ProductReview productReview);

        void AddNewsCommentTokens(IList<Token> tokens, NewsComment newsComment);

        void AddProductTokens(IList<Token> tokens, Product product, int languageId);

        void AddAttributeCombinationTokens(IList<Token> tokens, ProductAttributeCombination combination, int languageId);

        void AddBackInStockTokens(IList<Token> tokens, BackInStockSubscription subscription);

        string[] GetListOfCampaignAllowedTokens();

        string[] GetListOfAllowedTokens();
    }
}
