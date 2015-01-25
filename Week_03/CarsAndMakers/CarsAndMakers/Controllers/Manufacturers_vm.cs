using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarsAndMakers.Controllers
{
    public class ManufacturerList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ManufacturerAdd
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public int YearStarted { get; set; }
    }

    public class ManufacturerBase : ManufacturerAdd
    {
        public int Id { get; set; }
    }

    public class ManufacturerBaseWithVehicles : ManufacturerBase
    {
        public IEnumerable<VehicleBase> Vehicles { get; set; }
    }
}
