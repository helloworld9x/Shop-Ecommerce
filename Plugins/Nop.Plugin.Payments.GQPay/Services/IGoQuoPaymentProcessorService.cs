using System.Collections.Generic;
using Nop.Plugin.Payments.GQPay.Domain;

namespace Nop.Plugin.Payments.GQPay.Services
{
    public interface IGoQuoPaymentProcessorService
    {
        /// <summary>
        /// Logs the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        void Log(GoQuoPayProcessor record);

        void UpdateProcessor(GoQuoPayProcessor goQuoPaymentProcessor);

        void DeleteProcessor(GoQuoPayProcessor goQuoPaymentProcessor);

        GoQuoPayProcessor GetById(int id);

        GoQuoPayProcessor GetByCurrency(string currency);

        IList<GoQuoPayProcessor> GetAllProcessors();

        void Insert(GoQuoPayProcessor record);

        string GetSha512Hash(string rawData);


    }
}
