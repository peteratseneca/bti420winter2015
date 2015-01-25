using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cars.Controllers
{
    public class VehicleAdd
    {
        public string Model { get; set; }
        public string Trim { get; set; }
        public int ModelYear { get; set; }
        public int MSRP { get; set; }
    }

    public class VehicleBase : VehicleAdd
    {
        public int Id { get; set; }
    }
}
