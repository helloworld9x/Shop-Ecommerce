using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Routing;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Plugins;
using Nop.Plugin.Payments.GQPay.Controllers;
using Nop.Plugin.Payments.GQPay.Data;
using Nop.Plugin.Payments.GQPay.Services;
using Nop.Services.Localization;
using Nop.Services.Payments;
using Nop.Services.Security;
using Nop.Web.Framework;

namespace Nop.Plugin.Payments.GQPay
{
    public class GoQuoPaymentGateWaysProcess : BasePlugin, IPaymentMethod
    {
        private readonly GoQuoPayProcessorObjectContext _context;
        private readonly ProcessorKeyValueObjectContext _keyValueObjectContext;
        private readonly IStoreContext _storeContext;
        private readonly IGoQuoPaymentProcessorService _goQuoPaymentProcessor;
        private readonly IProcessorKeyValueService _processorKeyValueService;
        private readonly IEncryptionService _encryptionService;

        public GoQuoPaymentGateWaysProcess(GoQuoPayProcessorObjectContext context, ProcessorKeyValueObjectContext keyValueObjectContext, IStoreContext storeContext, IGoQuoPaymentProcessorService goQuoPaymentProcessor, IProcessorKeyValueService processorKeyValueService, IEncryptionService encryptionService)
        {
            _context = context;
            _keyValueObjectContext = keyValueObjectContext;
            _storeContext = storeContext;
            _goQuoPaymentProcessor = goQuoPaymentProcessor;
            _processorKeyValueService = processorKeyValueService;
            _encryptionService = encryptionService;
        }

        public override void Install()
        {
            //_keyValueObjectContext.Install();
            this.AddOrUpdatePluginLocaleResource("Plugin.Payments.GQPay.Fields.Name", "Process Name");
            this.AddOrUpdatePluginLocaleResource("Plugin.Payments.GQPay.Fields.Currency", "Process Currency");
            this.AddOrUpdatePluginLocaleResource("Plugin.Payments.GQPay.Fields.Active", "Process Active");
            this.AddOrUpdatePluginLocaleResource("Plugin.Payments.GQPay.Fields.KeyValue", "Process KeyValue");
            this.AddOrUpdatePluginLocaleResource("Plugin.Payments.GQPay.Fields.View", "KeyValue View");
            this.AddOrUpdatePluginLocaleResource("Plugin.Payments.GQPay.CreateProcess", "CreateProcess");
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Payments.GQPay.CreateProcess", "CreateProcess");
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Payments.GQPay.Configuration.Processor.KeyValue", "Key Value");
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Payments.GQPay.Configuration.Processor.Select", "Processor Select");
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Payments.GQPay.key", "key");
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Payments.GQPay.Value", "Value");
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Payments.GQPay.Nofields", "No fields config in GQ Payment");
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Payments.GQPay.Noconfig", "No config in GQ Payment");
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Payments.GQPay.RequestFail", "Request Fail");
            _context.Install();
            base.Install();
        }

        public override void Uninstall()
        {
            _keyValueObjectContext.Uninstall();
            _context.Uninstall();

            base.Uninstall();
        }

        public ProcessPaymentResult ProcessPayment(ProcessPaymentRequest processPaymentRequest)
        {
            var result = new ProcessPaymentResult();
            result.NewPaymentStatus = PaymentStatus.Pending;
            result.AllowStoringCreditCardNumber = true;

            return result;
        }

        // ReSharper disable InconsistentNaming

        public void PostProcessPayment(PostProcessPaymentRequest postProcessPaymentRequest)
        {
            var post = new RemotePost
            {
                FormName = "GQGateway",
                Method = "POST",
                AcceptCharset = "UTF-8"
            };

            var siteUrl = _storeContext.CurrentStore.Url.TrimEnd('/');

            var failUrl = $"{siteUrl}/{"Plugins/GQPay/PaymentFailuResult"}";

            if (postProcessPaymentRequest != null && postProcessPaymentRequest.Order != null)
            {
                var order = postProcessPaymentRequest.Order;
                var goquoProcess = _goQuoPaymentProcessor.GetByCurrency(order.CustomerCurrencyCode);

                if (goquoProcess != null)
                {
                    var keyvalueproce = _processorKeyValueService.GetAllProcessKeyValue(goquoProcess.Id);
                    if (keyvalueproce != null)
                    {
                        var keyvalues = keyvalueproce.ToDictionary(x => x.ProcessKey, x => x.ProcessValue);

                        string gqLogin, gqPass, url;
                        keyvalues.TryGetValue("GQLogin", out gqLogin);
                        keyvalues.TryGetValue("GQPassword", out gqPass);
                        keyvalues.TryGetValue("Url", out url);
                        var randomNumber = "GQ" + order.Id /*rnd.Next(0, 999999999).ToString().PadLeft(6, '0')*/;
                        var key = order.CustomerCurrencyCode + Convert.ToInt64(Math.Floor(order.OrderTotal)) +
                                  randomNumber + gqLogin + gqPass;

                        var billingAddress = order.BillingAddress;
                        string firstName = billingAddress.FirstName,
                            lastName = billingAddress.LastName,
                            address1 = billingAddress.Address1,
                            address2 = billingAddress.Address2,
                            city = billingAddress.City,
                            zipPostalCode = billingAddress.ZipPostalCode,
                            state = billingAddress.StateProvince != null
                                ? billingAddress.StateProvince.Name
                                : string.Empty,
                            countryName = billingAddress.Country != null ? billingAddress.Country.Name : string.Empty,
                            countryTwoLetterIsoCode = billingAddress.Country == null
                                ? string.Empty
                                : billingAddress.Country.TwoLetterIsoCode,
                            email = billingAddress.Email,
                            mobliePhone = billingAddress.PhoneNumber;

                        var CardType = _encryptionService.DecryptText(order.CardType);
                        var CardName = _encryptionService.DecryptText(order.CardName);
                        var CardNumber = _encryptionService.DecryptText(order.CardNumber);
                        var CardExpirationMonth = _encryptionService.DecryptText(order.CardExpirationMonth);
                        var CardExpirationYear = _encryptionService.DecryptText(order.CardExpirationYear);
                        var CardCvv2 = _encryptionService.DecryptText(order.CardCvv2);

                        var form = new Dictionary<string, object>();
                        failUrl = string.Format("{0}/{1}", siteUrl, "Plugins/GQPay/ResponsesResult");
                        form.Add("OrderID", randomNumber);
                        form.Add("Language", string.Empty);
                        form.Add("Name", firstName + " " + lastName);
                        form.Add("FirstName", firstName);
                        form.Add("LastName", lastName);
                        form.Add("Address1", address1);
                        form.Add("Address2", address2);
                        form.Add("City", city);
                        form.Add("PostCode", zipPostalCode);
                        form.Add("State", state);
                        form.Add("Country", countryName);
                        form.Add("CountryCode", countryTwoLetterIsoCode);
                        form.Add("Email", email);
                        form.Add("MobilePhone", mobliePhone);
                        form.Add("MerRespURL", failUrl);
                        form.Add("CCType", CardType);
                        form.Add("NameOntheCard", CardName);
                        form.Add("CreditCardNo", CardNumber);
                        form.Add("CreditCardExpiryDate",
                            CardExpirationMonth.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + "/" +
                            CardExpirationYear);
                        form.Add("CreditCardCVV", CardCvv2);
                        form.Add("PurAmount", order.OrderTotal.ToString("F"));
                        form.Add("GQKey", _goQuoPaymentProcessor.GetSha512Hash(key));
                        form.Add("CaptureCard", "0");
                        form.Add("ItemDesc", order.OrderGuid.ToString());
                        form.Add("Currency", order.CustomerCurrencyCode);

                        foreach (var processorKeyValue in keyvalues)
                        {
                            form.Add(processorKeyValue.Key, processorKeyValue.Value);
                        }
                        foreach (var param in form)
                        {
                            var paramValue = param.Value != null ? param.Value.ToString() : String.Empty;
                            post.Add(param.Key, paramValue);
                        }
                        post.AcceptCharset = "UTF-8";
                        post.Url = url;
                        post.Post();
                    }
                    else
                    {
                        post.Add("error", "No fields config in GQ Payment");
                        post.Add("orderId", postProcessPaymentRequest.Order.Id.ToString());
                        post.Url = failUrl;
                        post.Post();
                    }
                }
                else
                {
                    post.Add("error", "No config in GQ Payment");
                    post.Add("orderId", postProcessPaymentRequest.Order.Id.ToString());
                    post.Url = failUrl;
                    post.Post();
                }
            }
            else
            {
                post.Add("error", "Request Fail");
                post.Url = failUrl;
                post.Post();
            }
        }

        public bool HidePaymentMethod(IList<ShoppingCartItem> cart)
        {
            return false;
        }

        public decimal GetAdditionalHandlingFee(IList<ShoppingCartItem> cart)
        {
            return decimal.Zero;
        }

        public CapturePaymentResult Capture(CapturePaymentRequest capturePaymentRequest)
        {
            throw new NotImplementedException();
        }

        public RefundPaymentResult Refund(RefundPaymentRequest refundPaymentRequest)
        {
            throw new NotImplementedException();
        }

        public VoidPaymentResult Void(VoidPaymentRequest voidPaymentRequest)
        {
            throw new NotImplementedException();
        }

        public ProcessPaymentResult ProcessRecurringPayment(ProcessPaymentRequest processPaymentRequest)
        {
            throw new NotImplementedException();
        }

        public CancelRecurringPaymentResult CancelRecurringPayment(CancelRecurringPaymentRequest cancelPaymentRequest)
        {
            throw new NotImplementedException();
        }

        public bool CanRePostProcessPayment(Order order)
        {
            return false;
        }

        public Type GetControllerType()
        {
            return typeof(GoQuoPaymentGateWaysController);
        }

        public bool SupportCapture { get { return false; } }

        public bool SupportPartiallyRefund { get { return false; } }

        public bool SupportRefund { get { return false; } }

        public bool SupportVoid { get { return false; } }

        public RecurringPaymentType RecurringPaymentType { get { return RecurringPaymentType.NotSupported; } }

        public PaymentMethodType PaymentMethodType { get { return PaymentMethodType.Redirection; } }

        public bool SkipPaymentInfo { get { return false; } }

        public void GetPaymentInfoRoute(out string actionName, out string controllerName,
            out RouteValueDictionary routeValues)
        {
            actionName = "PaymentInfo";
            controllerName = "GoQuoPaymentGateWays";
            routeValues = new RouteValueDictionary()
            {
                { "Namespaces", "Nop.Plugin.Payments.GQPay.Controllers" },
                { "area", null }
            };
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName,
            out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "GoQuoPaymentGateWays";
            routeValues = new RouteValueDictionary()
            {
                { "Namespaces", "Nop.Plugin.Payments.GQPay.Controllers" },
                { "area", null }
            };
        }
    }

}
