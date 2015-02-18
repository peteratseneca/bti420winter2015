using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lab5.Models
{
    public class DataContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public DataContext() : base("name=DataContext") { }

        public System.Data.Entity.DbSet<Lab5.Models.Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }

    public class StoreInitializer : CreateDatabaseIfNotExists<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            context.Genres.Add(new Genre { Name = "Rock" });
            context.Genres.Add(new Genre { Name = "Pop" });
            context.Genres.Add(new Genre { Name = "Jazz" });
            context.Genres.Add(new Genre { Name = "Classical" });
            context.Genres.Add(new Genre { Name = "Country" });
            context.Genres.Add(new Genre { Name = "Folk" });
            context.Genres.Add(new Genre { Name = "Hip-Hop" });
            context.Genres.Add(new Genre { Name = "Blues" });

            context.SaveChanges();
        }
    }

}
