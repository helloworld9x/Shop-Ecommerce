using System;
using Nop.Web.Controllers;
using NUnit.Framework;

namespace Nop.Web.MVC.Tests.Public.Infrastructure
{
    [TestFixture]
    public class RoutesTests : RoutesTestsBase
    {
        [Test]
        public void Default_route()
        {
            "~/".ShouldMapTo<HomeController>(c => c.Index());
        }

        [Test]
        public void Catalog_routes()
        {
            //"~/p/some-se-name/".ShouldMapTo<CatalogController>(c => c.Product("some-se-name"));
            "~/recentlyviewedproducts/".ShouldMapTo<ProductController>(c => c.RecentlyViewedProducts());
            "~/newproducts/".ShouldMapTo<ProductController>(c => c.NewProducts());
            "~/newproducts/rss/".ShouldMapTo<ProductController>(c => c.NewProductsRss());
            "~/compareproducts/add/2".ShouldMapTo<ProductController>(c => c.AddProductToCompareList(2));
            "~/compareproducts/".ShouldMapTo<ProductController>(c => c.CompareProducts());
            "~/compareproducts/remove/3".ShouldMapTo<ProductController>(c => c.RemoveProductFromCompareList(3));
            "~/clearcomparelist/".ShouldMapTo<ProductController>(c => c.ClearCompareList());
            "~/productemailafriend/4".ShouldMapTo<ProductController>(c => c.ProductEmailAFriend(4));

            //"~/c/5/".ShouldMapTo<CatalogController>(c => c.Category(5, null));
            //"~/c/5/se-name/".ShouldMapTo<CatalogController>(c => c.Category(5, null));
            "~/manufacturer/all/".ShouldMapTo<CatalogController>(c => c.ManufacturerAll());
            //"~/m/6/".ShouldMapTo<CatalogController>(c => c.Manufacturer(6, null));
            //"~/m/6/se-name/".ShouldMapTo<CatalogController>(c => c.Manufacturer(6, null));

            "~/productreviews/7/".ShouldMapTo<ProductController>(c => c.ProductReviews(7));
            "~/backinstocksubscribe/8/".ShouldMapTo<BackInStockSubscriptionController>(c => c.SubscribePopup(8));

            "~/producttag/9/".ShouldMapTo<CatalogController>(c => c.ProductsByTag(9, null));
            "~/producttag/9/se-name/".ShouldMapTo<CatalogController>(c => c.ProductsByTag(9, null));
            "~/producttag/all/".ShouldMapTo<CatalogController>(c => c.ProductTagsAll());

            "~/search/".ShouldMapTo<CatalogController>(c => c.Search(null,null));
        }

        [Test]
        public void Customer_routes()
        {
            //"~/login/".ShouldMapTo<CustomerController>(c => c.Login(null, null, false));
            "~/login/checkoutasguest/".ShouldMapTo<CustomerController>(c => c.Login(true));
            //"~/register/".ShouldMapTo<CustomerController>(c => c.Register(null, false));
            "~/logout/".ShouldMapTo<CustomerController>(c => c.Logout());
            "~/registerresult/2".ShouldMapTo<CustomerController>(c => c.RegisterResult(2));
            "~/passwordrecovery/".ShouldMapTo<CustomerController>(c => c.PasswordRecovery());
            "~/passwordrecovery/confirm".ShouldMapTo<CustomerController>(c => c.PasswordRecoveryConfirm(null, null));

            "~/customer/info/".ShouldMapTo<CustomerController>(c => c.Info());
            "~/customer/addresses/".ShouldMapTo<CustomerController>(c => c.Addresses());
            "~/order/history/".ShouldMapTo<OrderController>(c => c.CustomerOrders());
            "~/returnrequest/history/".ShouldMapTo<ReturnRequestController>(c => c.CustomerReturnRequests());
            "~/backinstocksubscriptions/manage/".ShouldMapTo<BackInStockSubscriptionController>(c => c.CustomerSubscriptions(null));
            "~/backinstocksubscriptions/manage/3".ShouldMapTo<BackInStockSubscriptionController>(c => c.CustomerSubscriptions(3));

            "~/customer/changepassword/".ShouldMapTo<CustomerController>(c => c.ChangePassword());
            "~/customer/avatar/".ShouldMapTo<CustomerController>(c => c.Avatar());
            //"~/customer/activation?token=cc74c80f-1edd-43f7-85df-a3cccc1b47b9&email=test@test.com".ShouldMapTo<CustomerController>(c => c.AccountActivation("cc74c80f-1edd-43f7-85df-a3cccc1b47b9", "test@test.com"));
            "~/customer/addressdelete/6".ShouldMapTo<CustomerController>(c => c.AddressDelete(6));
            "~/customer/addressedit/7".ShouldMapTo<CustomerController>(c => c.AddressEdit(7));
            "~/customer/addressadd".ShouldMapTo<CustomerController>(c => c.AddressAdd());
        }

        [Test]
        public void Profile_routes()
        {
            "~/profile/1".ShouldMapTo<ProfileController>(c => c.Index(1, null));
            "~/profile/1/page/2".ShouldMapTo<ProfileController>(c => c.Index(1, 2));
        }

        [Test]
        public void Cart_routes()
        {
            "~/cart/".ShouldMapTo<ShoppingCartController>(c => c.Cart());
            "~/wishlist".ShouldMapTo<ShoppingCartController>(c => c.Wishlist(null));
            "~/wishlist/aa74c80f-1edd-43f7-85df-a3cccc1b47b9".ShouldMapTo<ShoppingCartController>(c => c.Wishlist(new Guid("aa74c80f-1edd-43f7-85df-a3cccc1b47b9")));
            "~/emailwishlist".ShouldMapTo<ShoppingCartController>(c => c.EmailWishlist());
        }

        [Test]
        public void Checkout_routes()
        {
            "~/checkout".ShouldMapTo<CheckoutController>(c => c.Index());
            "~/onepagecheckout".ShouldMapTo<CheckoutController>(c => c.OnePageCheckout());
            "~/checkout/shippingaddress".ShouldMapTo<CheckoutController>(c => c.ShippingAddress());
            "~/checkout/billingaddress".ShouldMapTo<CheckoutController>(c => c.BillingAddress(null));
            "~/checkout/shippingmethod".ShouldMapTo<CheckoutController>(c => c.ShippingMethod());
            "~/checkout/paymentmethod".ShouldMapTo<CheckoutController>(c => c.PaymentMethod());
            "~/checkout/paymentinfo".ShouldMapTo<CheckoutController>(c => c.PaymentInfo());
            "~/checkout/confirm".ShouldMapTo<CheckoutController>(c => c.Confirm());
            //"~/checkout/completed".ShouldMapTo<CheckoutController>(c => c.Completed(null));
            "~/checkout/completed/1".ShouldMapTo<CheckoutController>(c => c.Completed(1));
        }

        [Test]
        public void Order_routes()
        {
            "~/orderdetails/1".ShouldMapTo<OrderController>(c => c.Details(1));
            "~/reorder/3".ShouldMapTo<OrderController>(c => c.ReOrder(3));
            "~/orderdetails/pdf/4".ShouldMapTo<OrderController>(c => c.GetPdfInvoice(4));
            "~/orderdetails/print/5".ShouldMapTo<OrderController>(c => c.PrintOrderDetails(5));
        }

        [Test]
        public void ReturnRequest_routes()
        {
            "~/returnrequest/2".ShouldMapTo<ReturnRequestController>(c => c.ReturnRequest(2));
        }

        [Test]
        public void Common_routes()
        {
            "~/contactvendor/1".ShouldMapTo<CommonController>(c => c.ContactVendor(1));
            "~/contactus".ShouldMapTo<CommonController>(c => c.ContactUs());
            "~/sitemap".ShouldMapTo<CommonController>(c => c.Sitemap());
            "~/sitemap.xml".ShouldMapTo<CommonController>(c => c.SitemapXml());
        }

        [Test]
        public void Newsletter_routes()
        {
            //TODO cannot validate true parameter
            //"~/newsletter/subscriptionactivation/bb74c80f-1edd-43f7-85df-a3cccc1b47b9/true".ShouldMapTo<NewsletterController>(c => c.SubscriptionActivation(new Guid("bb74c80f-1edd-43f7-85df-a3cccc1b47b9"), true));
        }

        [Test]
        public void News_routes()
        {
            "~/news".ShouldMapTo<NewsController>(c => c.List(null));
            "~/news/rss/1".ShouldMapTo<NewsController>(c => c.ListRss(1));
            //"~/news/2/".ShouldMapTo<NewsController>(c => c.NewsItem(2));
            //"~/news/2/se-name".ShouldMapTo<NewsController>(c => c.NewsItem(2));
        }

        //[Test]
        //public void Topic_routes()
        //{
        //    "~/t/somename".ShouldMapTo<TopicController>(c => c.TopicDetails("somename"));
        //    "~/t-popup/somename".ShouldMapTo<TopicController>(c => c.TopicDetailsPopup("somename"));
        //}
    }
}
