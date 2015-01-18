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
            // A common collection is List<T>
            // The letter "T" is a type placeholder
            // Replace it with the data type that you're using

            string br = Environment.NewLine;

            // Create a collection of colour names
            // 'var' is a convenience for programmers
            // http://msdn.microsoft.com/en-us/library/bb384061.aspx

            var colours = new List<string>();
            colours.Add("red");
            colours.Add("green");
            colours.Add("blue");
            colours.Add("purple");
            colours.Add("black");
            colours.Add("brown");
            colours.Add("yellow");
            colours.Add("gold");

            // Show them
            foreach (var colour in colours)
            {
                Console.WriteLine(colour);
            }

            Console.WriteLine(br);
            
            // ############################################################

            // Create a Person object
            Person peter = new Person() { Id = 2, Name = "Peter" };

            // Before adding favourite colour names...
            Console.WriteLine(string.Format("(before) {0} has {1} favourite colour(s)", 
                peter.Name, peter.FavouriteColours.Count));
            // Now add some objects to the collection...
            peter.FavouriteColours.Add("blue");
            peter.FavouriteColours.Add("red");
            // After adding...
            Console.WriteLine(string.Format("(after) {0} has {1} favourite colour(s)", 
                peter.Name, peter.FavouriteColours.Count));

            // Show them
            foreach (var colour in peter.FavouriteColours)
            {
                Console.WriteLine(colour);
            }

            Console.WriteLine(br);

            // Create another person object, and assign the colour collection
            // we created at the top of this example during initialization
            Person elliott = new Person() 
                { Id = 4, Name = "Elliott", FavouriteColours = colours };

            // Show some info
            Console.WriteLine(string.Format("(after) {0} has {1} favourite colour(s)", 
                elliott.Name, elliott.FavouriteColours.Count));

            // Show them
            foreach (var colour in elliott.FavouriteColours)
            {
                Console.WriteLine(colour);
            }

            Console.WriteLine(br);

            // ############################################################
            
            // Create a collection of Person objects
            List<Person> teachers = new List<Person>();
            teachers.Add(peter);
            teachers.Add(elliott);

            // Show some content
            foreach (var teacher in teachers)
            {
                Console.WriteLine(string.Format("Teacher '{0}' has {1} favourite colour(s)", 
                    teacher.Name, teacher.FavouriteColours.Count));
            }

            Console.WriteLine(br);
        }

    }

    // Person class
    // Notice that it contains a collection
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
