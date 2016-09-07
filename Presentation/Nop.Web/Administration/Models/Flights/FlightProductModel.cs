using System.Collections.Generic;
using Nop.Admin.Models.Customers;
using Nop.Admin.Models.Stores;
using Nop.Core.Domain.Flights;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Models.Flights
{
    public class FlightProductModel : BaseNopEntityModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Currency { get; set; }

        public decimal Price { get; set; }

        public string Attributes { get; set; }

        public FlightProductType Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the flight has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        //ACL
        [NopResourceDisplayName("Subject To Acl")]
        public bool SubjectToAcl { get; set; }

        [NopResourceDisplayName("Acl Customer Roles")]
        public List<CustomerRoleModel> AvailableCustomerRoles { get; set; }

        public int[] SelectedCustomerRoleIds { get; set; }
        
        public bool LimitedToStores { get; set; }

        [NopResourceDisplayName("Available Stores")]
        public List<StoreModel> AvailableStores { get; set; }

        public int[] SelectedStoreIds { get; set; }
    }
}