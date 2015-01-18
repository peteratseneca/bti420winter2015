using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            string br = Environment.NewLine;

            // Create some collections first, so that we have something to work with

            // Create a collection of strings
            var colours = new List<string>();
            colours.Add("red");
            colours.Add("green");
            colours.Add("blue");
            colours.Add("purple");
            colours.Add("black");
            colours.Add("brown");
            colours.Add("yellow");
            colours.Add("gold");

            // Create a Person object
            Person peter = new Person() { Id = 2, Name = "Peter" };
            peter.FavouriteColours.Add("blue");
            peter.FavouriteColours.Add("red");

            // Create another person object, and assign the colour collection
            Person elliott = new Person() { Id = 4, Name = "Elliott", FavouriteColours = colours };

            // Create a collection of Person objects
            List<Person> teachers = new List<Person>();
            teachers.Add(peter);
            teachers.Add(elliott);

            // ############################################################

            // Common LINQ methods:
            // SingleOrDefault()
            // Where()
            // OrderBy(), OrderByDescending()
            // Contains()

            // Others:
            // ToArray(), ToList(), ToDictionary()
            // First(), Last()
            // FirstOrDefault()
            // Distinct()
            // Max(), Min(), Sum(), Average()

            // One colour
            Console.WriteLine("Object 'blue':   " + colours.SingleOrDefault(c => c == "blue"));

            // Filtered - colours that begin with the letter 'b'
            var bColours = colours.Where(c => c.ToUpper().StartsWith("B")).ToArray();
            var bColoursString = string.Join(", ", bColours);
            Console.WriteLine("'b' colours:     " + bColoursString);

            // Colours ordered (sorted) by name
            var orderedColours = colours.OrderBy(c => c).ToArray();
            var orderedColoursString = string.Join(", ", orderedColours);
            Console.WriteLine("Ordered colours: " + orderedColoursString);

            // Is brown or pink in the collection?
            bool hasBrown = colours.Contains("brown");
            bool hasPink = colours.Contains("pink");
            Console.WriteLine(string.Format("Has colours:     brown={0}; pink={1}", hasBrown, hasPink));

            Console.WriteLine(br);

            // ############################################################

            // One teacher
            var teacherPeter = teachers.SingleOrDefault(t => t.Name == "Peter");
            Console.WriteLine(string.Format("Peter exists?    {0}", (teacherPeter == null) ? "no" : "yes"));

            var teacherBarb = teachers.SingleOrDefault(t => t.Name == "Barb");
            Console.WriteLine(string.Format("Barb exists?     {0}", (teacherBarb == null) ? "no" : "yes"));

            Console.WriteLine(br);

            // Filtered - teachers with more than 3 favourite colours
            var colourfulTeachers = teachers.Where(t => t.FavouriteColours.Count > 3);
            Console.WriteLine("Colourful teachers:");
            foreach (var teacher in colourfulTeachers)
            {
                Console.WriteLine(teacher.Name);
            }

            Console.WriteLine(br);

            // Teachers ordered (sorted) by name
            var orderedTeachers = teachers.OrderBy(tn => tn.Name).ToArray();
            Console.WriteLine("Ordered teachers:");
            foreach (var teacher in orderedTeachers)
            {
                Console.WriteLine(teacher.Name);
            }

            Console.WriteLine(br);
        }

    }

    // Person class
    class Person
    {
        public Person()
        {
            this.FavouriteColours = new List<string>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<string> FavouriteColours { get; set; }
    }

}
