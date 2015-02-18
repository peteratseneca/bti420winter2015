using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lab5.Controllers
{
    public static partial class ConfigureMaps
    {
        public static void ForArtist()
        {
            AutoMapper.Mapper.CreateMap<Models.Artist, Controllers.ArtistBase>();
            AutoMapper.Mapper.CreateMap<Models.Artist, Controllers.ArtistBaseWithAlbums>();
            AutoMapper.Mapper.CreateMap<Models.Artist, Controllers.ArtistList>();
            AutoMapper.Mapper.CreateMap<Controllers.ArtistAdd, Models.Artist>();
        }
    }

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

    public class ArtistBaseWithAlbums : ArtistBase
    {
        // For display only, so no constructor is needed
        // Make sure property name matches the 
        // name of the property in the design model class
        public IEnumerable<AlbumBase> Albums { get; set; }
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
