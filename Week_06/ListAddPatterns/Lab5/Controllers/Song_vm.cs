using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lab5.Controllers
{
    public class SongList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SongAdd
    {
        public int AlbumId { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Song name")]
        public string Name { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Composer(s)")]
        public string Composer { get; set; }

        [Range(1, 100)]
        public int TrackNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release date, as a single")]
        public DateTime? ReleaseDateAsSingle { get; set; }
    }

    public class SongBase : SongAdd
    {
        public int Id { get; set; }

        // added
        [Display(Name = "Album name")]
        public string AlbumName { get; set; }
        [Display(Name = "Artist name")]
        public string AlbumArtistName { get; set; }
    }

    public class SongAddForm
    {
        public SelectList AlbumId { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Song name")]
        public string Name { get; set; }

        [Required]
        public SelectList Genre { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Composer(s)")]
        public string Composer { get; set; }

        [Range(1, 100)]
        public int TrackNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release date, as a single")]
        public DateTime? ReleaseDateAsSingle { get; set; }
    }

}
