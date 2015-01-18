using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            string br = Environment.NewLine;

            // We will write classes for 'Supplier' and 'Product'
            // This is a typical one-to-many association
            // One Supplier object has a collection of zero or more Product objects
            // See the classes below

            // Create a Supplier object
            Supplier microsoft = new Supplier()
            {
                Id = 1,
                Name = "Microsoft Corporation",
                Country = "USA"
            };

            // Create a Product object
            var keyboard = new Product()
            {
                Id = 2,
                Name = "Keyboard",
                MSRP = 45,
                ProductCode = "qwe123",
                UPC = "0987654321"
            };

            // Add the Product object to the Supplier's collection of Product objects
            microsoft.Products.Add(keyboard);
            // For in-memory only objects, we must also set the other end
            keyboard.Supplier = microsoft;

            // Add a new Product object directly to the Supplier's collection
            // Notice that we set the supplier property for this in-memory only object
            microsoft.Products.Add(new Product()
            {
                Id = 1,
                MSRP = 25,
                Name = "Mouse",
                ProductCode = "abc123",
                UPC = "1234567890",
                Supplier = microsoft
            });

            // Show the results
            Console.WriteLine(string.Format("Supplier '{0}' sells these {1} products:",
                microsoft.Name, microsoft.Products.Count));
            foreach (var product in microsoft.Products)
            {
                Console.WriteLine(string.Format("{0} (UPC {1}), MSRP is {2}",
                    product.Name, product.UPC, product.MSRP.ToString("C")));
            }

            Console.WriteLine(br);

            // For the 'keyboard' product, walk through the navigation property
            // and get access to properties in the related object
            Console.WriteLine(string.Format("The {0} product is supplied by {1} ({2})",
                keyboard.Name, keyboard.Supplier.Name, keyboard.Supplier.Country));

            Console.WriteLine(br);

        }

    }

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
