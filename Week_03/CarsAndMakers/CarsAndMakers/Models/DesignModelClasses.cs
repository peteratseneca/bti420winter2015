using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarsAndMakersAndMakers.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public int ModelYear { get; set; }
        public int MSRP { get; set; }

        // Notice the "to-one" navigation property...
        public Manufacturer Manufacturer { get; set; }
    }

    public class Manufacturer
    {
        public Manufacturer()
        {
            // In a domain model class, a best practice is to initialize the collection
            this.Vehicles = new List<Vehicle>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int YearStarted { get; set; }

        // Notice the "to-many" navigation property...
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
