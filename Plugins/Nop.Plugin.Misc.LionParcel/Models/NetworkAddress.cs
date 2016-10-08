using System.Collections.Generic;

namespace Nop.Plugin.Misc.LionParcel.Models
{
    public class NetworkAddres
    {
        public string No { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Address { get; set; }

        public string Contact1 { get; set; }

        public string Contact2 { get; set; }

        public string City { get; set; }

        public string Latitude { get; set; }

        public string Longtitude { get; set; }
    }

    public class Addreses
    {
        public List<NetworkAddres> Addresses { get; set; }
    }
}
