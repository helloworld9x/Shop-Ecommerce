//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Xml;
//using Nop.Core.Domain.Customers;
//using Nop.Services.Localization;

//namespace Nop.Services.Customers
//{
//    /// <summary>
//    /// Customer attribute parser
//    /// </summary>
//    public partial class CustomerAttributeParser : ICustomerAttributeParser
//    {
//        private readonly ICustomerAttributeService _customerAttributeService;
//        private readonly ILocalizationService _localizationService;

//        public CustomerAttributeParser(ICustomerAttributeService customerAttributeService,
//            ILocalizationService localizationService)
//        {
//            this._customerAttributeService = customerAttributeService;
//            this._localizationService = localizationService;
//        }

//        /// <summary>
//        /// Gets selected customer attribute identifiers
//        /// </summary>
//        /// <param name="attributesXml">Attributes in XML format</param>
//        /// <returns>Selected customer attribute identifiers</returns>
//        protected virtual IList<int> ParseCustomerAttributeIds(string attributesXml)
//        {
//            var ids = new List<int>();
//            if (String.IsNullOrEmpty(attributesXml))
//                return ids;

//            try
//            {
//                var xmlDoc = new XmlDocument();
//                xmlDoc.LoadXml(attributesXml);

//                foreach (XmlNode node in xmlDoc.SelectNodes(@"//Attributes/CustomerAttribute"))
//                {
//                    if (node.Attributes != null && node.Attributes["ID"] != null)
//                    {
//                        string str1 = node.Attributes["ID"].InnerText.Trim();
//                        int id;
//                        if (int.TryParse(str1, out id))
//                        {
//                            ids.Add(id);
//                        }
//                    }
//                }
//            }
//            catch (Exception exc)
//            {
//                Debug.Write(exc.ToString());
//            }
//            return ids;
//        }



//    }
//}
