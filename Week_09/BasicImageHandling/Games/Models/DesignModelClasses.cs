using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;

namespace Games.Models
{
    public class Sport
    {
        public Sport()
        {
            this.Venues = new List<Venue>();
            this.Timestamp = DateTime.Now;
        }

        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        public byte[] Logo { get; set; }
        [Required, StringLength(50)]
        public string LogoContentType { get; set; }

        [Required, StringLength(5000)]
        public string Description { get; set; }

        [Required, StringLength(5000)]
        public string History { get; set; }

        [Required, StringLength(5000)]
        public string HowItWorks { get; set; }

        public byte[] Photo { get; set; }
        [Required, StringLength(50)]
        public string PhotoContentType { get; set; }

        [Required, StringLength(100)]
        public string VenueNames { get; set; }

        public ICollection<Venue> Venues { get; set; }
    }

    public class Venue
    {
        public Venue()
        {
            this.Sports = new List<Sport>();
        }

        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(5000)]
        public string Description { get; set; }

        [Required, StringLength(200)]
        public string Location { get; set; }

        public byte[] Photo { get; set; }
        [Required, StringLength(50)]
        public string PhotoContentType { get; set; }

        public byte[] Map { get; set; }
        [Required, StringLength(50)]
        public string MapContentType { get; set; }

        public string SportNames { get; set; }

        public ICollection<Sport> Sports { get; set; }
    }
}
