using System.Collections.Generic;
using Nop.Core.Domain.Payments;

namespace Nop.Plugin.Payments.GQPay.Domain
{
    public class ProcessPaymentResponse
    {
        public ProcessPaymentResponse()
        {
            Errors = new List<string>();
            PaymentStatus = PaymentStatus.Pending;

        }

        public IList<string> Errors { get; set; }

        public bool Success => (Errors.Count == 0);

        /// <summary>
        ///     Gets or sets a payment status after processing
        /// </summary>
        public PaymentStatus PaymentStatus { get; set; }

        public void AddError(string error)
        {
            Errors.Add(error);
        }

        /// <summary>
        /// Gets or sets a credit card type (Visa, Master Card, etc...)
        /// </summary>
        public string CreditCardType { get; set; }

        /// <summary>
        /// Gets or sets a credit card owner name
        /// </summary>
        public string CreditCardName { get; set; }

        /// <summary>
        /// Gets or sets a credit card number
        /// </summary>
        public string CreditCardNumber { get; set; }

        /// <summary>
        /// Gets or sets a credit card expire year
        /// </summary>
        public string CreditCardExpireYear { get; set; }

        /// <summary>
        /// Gets or sets a credit card expire month
        /// </summary>
        public string CreditCardExpireMonth { get; set; }

        /// <summary>
        /// Gets or sets a credit card CVV2 (Card Verification Value)
        /// </summary>
        public string CreditCardCvvCode { get; set; }

        public string RedirectUrl { get; set; }

        public string TransactionID { get; set; }

        public string Trans_Auth_Code { get; set; }

        public string RedirectMethod { get; set; }

        public string Request { get; set; }

        public IDictionary<string, string> RedirectValues { get; set; } 
    }
}