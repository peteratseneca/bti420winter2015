using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// New...
using System.Web.Mvc;

namespace CarsAndMakers.Controllers
{
    public class VehicleAddForm
    {
        public string Model { get; set; }
        public string Trim { get; set; }
        public int ModelYear { get; set; }
        public int MSRP { get; set; }
        public SelectList Manufacturers { get; set; }
    }

    public class VehicleAdd
    {
        public string Model { get; set; }
        public string Trim { get; set; }
        public int ModelYear { get; set; }
        public int MSRP { get; set; }
        public int ManufacturerId { get; set; }
    }

    // Does not inherit from VehicleAdd...
    public class VehicleBase
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public int ModelYear { get; set; }
        public int MSRP { get; set; }
    }

    public class VehicleBaseWithManufacturer : VehicleBase
    {
        public ManufacturerBase Manufacturer { get; set; }
    }
}
