using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Plugin.Payments.GQPay.Domain;

namespace Nop.Plugin.Payments.GQPay.Services
{
    public class ProcessorKeyValueService : IProcessorKeyValueService
    {
        private readonly IRepository<ProcessorKeyValue> _processKeyValueRepository;
        private readonly ICacheManager _cacheManager;

        public ProcessorKeyValueService(IRepository<ProcessorKeyValue> processKeyValueRepository, ICacheManager cacheManager)
        {
            _processKeyValueRepository = processKeyValueRepository;
            _cacheManager = cacheManager;
        }

        public  IList<ProcessorKeyValue> GetAllProcessKeyValue(int processId)
        {
            var records = _processKeyValueRepository.Find(x => x.ProcessId == processId).ToList();
            
            return records;

        }

        public void InssertProcessKeyValue(ProcessorKeyValue processorKeyValue)
        {
            if (processorKeyValue == null)
            {
                throw new ArgumentNullException(nameof(processorKeyValue));
            }
            _processKeyValueRepository.Insert(processorKeyValue);
        }

        public void DeleteProcessKeyValue(ProcessorKeyValue processorKeyValue)
        {
            if (processorKeyValue == null)
            {
                throw new ArgumentNullException(nameof(processorKeyValue));
            }
            _processKeyValueRepository.Delete(processorKeyValue);
        }

        public void UpdateProcessKeyValue(ProcessorKeyValue processorKeyValue)
        {
            if (processorKeyValue != null)
            _processKeyValueRepository.Update(processorKeyValue);
        }

        public ProcessorKeyValue GetProcessorKeyValueById(int id)
        {
            if (id == 0)
            {
                return null;
            }
            return _processKeyValueRepository.GetById(id);
        }

        //public  Dictionary<string, KeyValuePair<int, string>> GetAllProcessKeyValue(int processId)
        //{
        //    string key = string.Format("Nop.lsr.all-{0}", processId);
        //    return _cacheManager.Get(key, () =>
        //    {
        //        //we use no tracking here for performance optimization
        //        //anyway records are loaded only for read-only operations
        //        var query = from l in _processKeyValueRepository.TableNoTracking
        //                    orderby l.ProcessKey
        //                    where l.ProcessId == processId
        //                    select l;
        //        var locales = query.ToList();
        //        //format: <name, <id, value>>
        //        var dictionary = new Dictionary<string, KeyValuePair<int, string>>();
        //        foreach (var locale in locales)
        //        {
        //            var ProcessKey = locale.ProcessKey.ToLowerInvariant();
        //            if (!dictionary.ContainsKey(ProcessKey))
        //                dictionary.Add(ProcessKey, new KeyValuePair<int, string>(locale.Id, locale.ProcessValue));
        //        }
        //        return dictionary;
        //    });
        //}
    }
}
