using System.Collections.Generic;
using Nop.Plugin.Payments.GQPay.Domain;

namespace Nop.Plugin.Payments.GQPay.Services
{
    public interface IProcessorKeyValueService
    {
        IList<ProcessorKeyValue> GetAllProcessKeyValue(int processId);

        void InssertProcessKeyValue(ProcessorKeyValue processorKeyValue);

        void DeleteProcessKeyValue(ProcessorKeyValue processorKeyValue);

        void UpdateProcessKeyValue(ProcessorKeyValue processorKeyValue);

        ProcessorKeyValue GetProcessorKeyValueById(int id);



    }
}
