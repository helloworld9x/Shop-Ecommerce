using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Admin.Models.Directory;
using Nop.Core.Domain.Catalog;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Misc.Region.Models
{
  public partial class ConfigurationModel : BaseNopModel
    {
      public ConfigurationModel()
      {
            AvailableCountries = new List<CountryModel>();
            Restricted = new Dictionary<int, Dictionary<int, bool>>();
        }
        public string ProductName { get; set; }

        public Product Product { get; set; }

        public IList<CountryModel> AvailableCountries { get; set; }

        public IDictionary<int, Dictionary<int,bool>> Restricted { get; set; }
    }
}
