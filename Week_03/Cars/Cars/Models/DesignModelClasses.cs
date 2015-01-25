using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cars.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public int ModelYear { get; set; }
        public int MSRP { get; set; }
    }

}
