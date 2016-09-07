using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Admin.Models.Customers;
using Nop.Admin.Models.Stores;
using Nop.Admin.Validators.Flights;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Models.Flights
{
    [Validator(typeof(FlightStatusValidator))]
    public class FlightStatusModel : BaseNopEntityModel
    {
        public FlightStatusModel()
        {
            AvailableFlightStatus = new List<SelectListItem>();
        }

        public List<SelectListItem> AvailableFlightStatus { get; set; }

        [NopResourceDisplayName("Admin.Flight.FlightStatus.Fields.CommercialName")]
        [AllowHtml]
        public string CommercialName { get; set; }

        [NopResourceDisplayName("Admin.Flight.FlightStatus.Fields.From")]
        [AllowHtml]
        public string From { get; set; }

        [NopResourceDisplayName("Admin.Flight.FlightStatus.Fields.To")]
        [AllowHtml]
        public string To { get; set; }

        [NopResourceDisplayName("Admin.Flight.FlightStatus.Fields.AirlineCode")]
        [AllowHtml]
        public string AirlineCode { get; set; }

        [NopResourceDisplayName("Admin.Flight.FlightStatus.Fields.AirlineName")]
        [AllowHtml]
        public string AirlineName { get; set; }

        [NopResourceDisplayName("Admin.Flight.FlightStatus.Fields.FlightNumber")]
        [AllowHtml]
        public string FlightNumber { get; set; }

        [NopResourceDisplayName("Admin.Flight.FlightStatus.Fields.Currency")]
        [AllowHtml]
        public string Currency { get; set; }

        [NopResourceDisplayName("Admin.Flight.FlightStatus.Fields.Active")]
        public bool Active { get; set; }

        [NopResourceDisplayName("Admin.Flight.FlightStatus.Fields.Deleted")]
        public bool Deleted { get; set; }

        //ACL
        [NopResourceDisplayName("Admin.Flight.FlightStatus.Fields.SubjectToAcl")]
        public bool SubjectToAcl { get; set; }
        [NopResourceDisplayName("Admin.Flight.FlightStatus.Fields.AclCustomerRoles")]
        public List<CustomerRoleModel> AvailableCustomerRoles { get; set; }
        public int[] SelectedCustomerRoleIds { get; set; }

        //Store mapping
        [NopResourceDisplayName("Admin.Flight.FlightStatus.Fields.LimitedToStores")]
        public bool LimitedToStores { get; set; }

        [NopResourceDisplayName("Admin.Flight.FlightStatus.Fields.AvailableStores")]
        public List<StoreModel> AvailableStores { get; set; }

        public int[] SelectedStoreIds { get; set; }

        public string Breadcrumb { get; set; }
    }
}