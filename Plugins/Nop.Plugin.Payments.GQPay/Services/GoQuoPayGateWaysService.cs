using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Plugin.Payments.GQPay.Domain;

namespace Nop.Plugin.Payments.GQPay.Services
{
    public class GoQuoPaymentGateWaysService : IGoQuoPaymentProcessorService
    {
        //private const string ProcessAllKey = "Nop.process.all-{0}";
        private readonly IRepository<GoQuoPayProcessor> _goquoPaymentGateWaysRepository;

        public GoQuoPaymentGateWaysService(IRepository<GoQuoPayProcessor> goquoPaymentGateWaysRepository, ICacheManager cacheManager)
        {
            _goquoPaymentGateWaysRepository = goquoPaymentGateWaysRepository;
        }

        /// <summary>
        /// Logs the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        public void Log(GoQuoPayProcessor record)
        {
            _goquoPaymentGateWaysRepository.Insert(record);
        }

        public void UpdateProcessor(GoQuoPayProcessor goQuoPaymentProcessor)
        {
            if (goQuoPaymentProcessor != null)
                _goquoPaymentGateWaysRepository.Update(goQuoPaymentProcessor);
        }

        public void DeleteProcessor(GoQuoPayProcessor goQuoPaymentProcessor)
        {
            if (goQuoPaymentProcessor == null)
                throw new ArgumentNullException(nameof(goQuoPaymentProcessor));

            _goquoPaymentGateWaysRepository.Delete(goQuoPaymentProcessor);
        }

        public GoQuoPayProcessor GetById(int id)
        {
            if (id == 0)
            {
                return null;
            }
            return _goquoPaymentGateWaysRepository.GetById(id);
        }

        public GoQuoPayProcessor GetByCurrency(string currency)
        {
            return _goquoPaymentGateWaysRepository.Table.FirstOrDefault(x => x.Currency.Contains(currency) && x.Active);
        }

        public IList<GoQuoPayProcessor> GetAllProcessors()
        {
            var findOptions = new FindOptions<GoQuoPayProcessor>();
            findOptions.SortAscending(x => x.Id);

            var query = _goquoPaymentGateWaysRepository.Find(null, findOptions);
            var records = query.ToList();

            return records;
        }

        public string GetSha512Hash(string rawData)
        // ReSharper restore InconsistentNaming
        {
            var objSha512 = new SHA512CryptoServiceProvider();
            byte[] bytValue = Encoding.UTF8.GetBytes(rawData);
            byte[] bytHash = objSha512.ComputeHash(bytValue);
            return BitConverter.ToString(bytHash).Replace("-", "");
        }

        public void Insert(GoQuoPayProcessor record)
        {
            if (record != null)
                _goquoPaymentGateWaysRepository.Insert(record);
        }
    }
}
