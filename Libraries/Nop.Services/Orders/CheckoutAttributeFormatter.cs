using System;
using System.Text;
using System.Web;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Html;
using Nop.Services.Catalog;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Tax;

namespace Nop.Services.Orders
{
    /// <summary>
    /// Checkout attribute helper
    /// </summary>
    public class CheckoutAttributeFormatter : ICheckoutAttributeFormatter
    {
        private readonly IWorkContext _workContext;
        private readonly ICheckoutAttributeService _checkoutAttributeService;
        private readonly ICheckoutAttributeParser _checkoutAttributeParser;
        private readonly ICurrencyService _currencyService;
        private readonly ITaxService _taxService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IWebHelper _webHelper;

        public CheckoutAttributeFormatter(IWorkContext workContext,
            ICheckoutAttributeService checkoutAttributeService,
            ICheckoutAttributeParser checkoutAttributeParser,
            ICurrencyService currencyService,
            ITaxService taxService,
            IPriceFormatter priceFormatter,
            IWebHelper webHelper)
        {
            _workContext = workContext;
            _checkoutAttributeService = checkoutAttributeService;
            _checkoutAttributeParser = checkoutAttributeParser;
            _currencyService = currencyService;
            _taxService = taxService;
            _priceFormatter = priceFormatter;
            _webHelper = webHelper;
        }

        /// <summary>
        /// Formats attributes
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>Attributes</returns>
        public virtual string FormatAttributes(string attributesXml)
        {
            var customer = _workContext.CurrentCustomer;
            return FormatAttributes(attributesXml, customer);
        }

        /// <summary>
        /// Formats attributes
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <param name="customer">Customer</param>
        /// <param name="serapator">Serapator</param>
        /// <param name="htmlEncode">A value indicating whether to encode (HTML) values</param>
        /// <param name="renderPrices">A value indicating whether to render prices</param>
        /// <param name="allowHyperlinks">A value indicating whether to HTML hyperink tags could be rendered (if required)</param>
        /// <returns>Attributes</returns>
        public virtual string FormatAttributes(string attributesXml,
            Customer customer, 
            string serapator = "<br />", 
            bool htmlEncode = true, 
            bool renderPrices = true,
            bool allowHyperlinks = true)
        {
            var result = new StringBuilder();

            var attributes = _checkoutAttributeParser.ParseCheckoutAttributes(attributesXml);
            for (int i = 0; i < attributes.Count; i++)
            {
                var attribute = attributes[i];
                var valuesStr = _checkoutAttributeParser.ParseValues(attributesXml, attribute.Id);
                for (int j = 0; j < valuesStr.Count; j++)
                {
                    string valueStr = valuesStr[j];
                    string formattedAttribute = "";
                    if (!attribute.ShouldHaveValues())
                    {
                        //no values
                        if (attribute.AttributeControlType == AttributeControlType.MultilineTextbox)
                        {
                            //multiline textbox
                            var attributeName = attribute.GetLocalized(a => a.Name, _workContext.WorkingLanguage.Id);
                            //encode (if required)
                            if (htmlEncode)
                                attributeName = HttpUtility.HtmlEncode(attributeName);
                            formattedAttribute = string.Format("{0}: {1}", attributeName, HtmlHelper.FormatText(valueStr, false, true, false, false, false, false));
                            //we never encode multiline textbox input
                        }
                        else if (attribute.AttributeControlType == AttributeControlType.FileUpload)
                        {
                            //file upload
                            Guid downloadGuid;
                            Guid.TryParse(valueStr, out downloadGuid);
                        }
                        else
                        {
                            //other attributes (textbox, datepicker)
                            formattedAttribute = string.Format("{0}: {1}", attribute.GetLocalized(a => a.Name, _workContext.WorkingLanguage.Id), valueStr);
                            //encode (if required)
                            if (htmlEncode)
                                formattedAttribute = HttpUtility.HtmlEncode(formattedAttribute);
                        }
                    }
                    else
                    {
                        int attributeValueId;
                        if (int.TryParse(valueStr, out attributeValueId))
                        {
                            var attributeValue = _checkoutAttributeService.GetCheckoutAttributeValueById(attributeValueId);
                            if (attributeValue != null)
                            {
                                formattedAttribute = string.Format("{0}: {1}", attribute.GetLocalized(a => a.Name, _workContext.WorkingLanguage.Id), attributeValue.GetLocalized(a => a.Name, _workContext.WorkingLanguage.Id));
                                if (renderPrices)
                                {
                                    decimal priceAdjustmentBase = _taxService.GetCheckoutAttributePrice(attributeValue, customer);
                                    decimal priceAdjustment = _currencyService.ConvertFromPrimaryStoreCurrency(priceAdjustmentBase, _workContext.WorkingCurrency);
                                    if (priceAdjustmentBase > 0)
                                    {
                                        string priceAdjustmentStr = _priceFormatter.FormatPrice(priceAdjustment);
                                        formattedAttribute += string.Format(" [+{0}]", priceAdjustmentStr);
                                    }
                                }
                            }
                            //encode (if required)
                            if (htmlEncode)
                                formattedAttribute = HttpUtility.HtmlEncode(formattedAttribute);
                        }
                    }

                    if (!String.IsNullOrEmpty(formattedAttribute))
                    {
                        if (i != 0 || j != 0)
                            result.Append(serapator);
                        result.Append(formattedAttribute);
                    }
                }
            }

            return result.ToString();
        }
    }
}
