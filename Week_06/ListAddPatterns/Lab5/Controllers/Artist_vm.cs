using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lab5.Controllers
{
    public class ArtistList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ArtistAdd
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Artist or stage name")]
        public string Name { get; set; }

        [StringLength(100)]
        [Display(Name = "Birth name")]
        public string BirthName { get; set; }

        [Required]
        [Display(Name = "Birth or start date")]
        [DataType(DataType.Date)]
        public DateTime BirthOrStartDate { get; set; }

        [Display(Name = "Decade started")]
        [Range(1880, 2100)]
        public int StartDecade { get; set; }

        [Required]
        public string Genre { get; set; }
    }

    public class ArtistBase : ArtistAdd
    {
        public int Id { get; set; }
    }

    public class ArtistAddForm
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Artist or stage name")]
        public string Name { get; set; }

        [StringLength(100)]
        [Display(Name = "Birth name")]
        public string BirthName { get; set; }

        [Required]
        [Display(Name = "Birth or start date")]
        [DataType(DataType.Date)]
        public DateTime BirthOrStartDate { get; set; }

        [Display(Name = "Decade started")]
        [Range(1880, 2100)]
        public int StartDecade { get; set; }

        [Required]
        public SelectList Genre { get; set; }
    }

}
