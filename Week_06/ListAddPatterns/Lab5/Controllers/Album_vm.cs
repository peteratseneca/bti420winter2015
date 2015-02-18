using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lab5.Controllers
{
    public class AlbumList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // New properties...
        public string ArtistName { get; set; }
        public string AlbumAndArtist 
        { 
            get
            {
                return string.Format("{0} - {1}", this.Name, this.ArtistName);
            }
        }
    }

    public class AlbumAdd
    {
        public int ArtistId { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Album name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Album release date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public string Genre { get; set; }

        [Display(Name = "Length in minutes")]
        [Range(0.0, 100.0)]
        public double LengthInMinutes { get; set; }

        [Required, StringLength(200)]
        public string Producer { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Album cover URL")]
        public string AlbumCoverUrl { get; set; }
    }

    public class AlbumBase : AlbumAdd
    {
        public int Id { get; set; }

        // added
        [Display(Name = "Artist name")]
        public string ArtistName { get; set; }
    }

    public class AlbumAddForm
    {
        public SelectList ArtistId { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Album name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Album release date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public SelectList Genre { get; set; }

        [Display(Name = "Length in minutes")]
        [Range(0.0, 100.0)]
        public double LengthInMinutes { get; set; }

        [Required, StringLength(200)]
        public string Producer { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Album cover URL")]
        public string AlbumCoverUrl { get; set; }
    }

}
