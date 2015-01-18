using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelatedData.Controllers
{
    public class Supplier
    {
        public Supplier()
        {
            this.Products = new List<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        // 'Navigation property' - association - collection of Product objects
        public ICollection<Product> Products { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public string UPC { get; set; }
        public double MSRP { get; set; }

        // 'Navigation property' - association - Supplier object
        public Supplier Supplier { get; set; }
    }

}