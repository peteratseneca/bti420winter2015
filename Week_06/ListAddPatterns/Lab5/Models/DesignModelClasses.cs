using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class Artist
    {
        public Artist()
        {
            this.Albums = new List<Album>();
            this.BirthOrStartDate = DateTime.Now;
        }

        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string BirthName { get; set; }
        [Required]
        public DateTime BirthOrStartDate { get; set; }
        public int StartDecade { get; set; }
        [Required]
        public string Genre { get; set; }

        public ICollection<Album> Albums { get; set; }
    }

    public class Album
    {
        public Album()
        {
            this.Songs = new List<Song>();
            this.ReleaseDate = DateTime.Now;
        }

        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Name { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string Genre { get; set; }
        public double LengthInMinutes { get; set; }
        [Required, StringLength(200)]
        public string Producer { get; set; }
        [Required, StringLength(200)]
        public string AlbumCoverUrl { get; set; }

        [Required]
        public Artist Artist { get; set; }
        public ICollection<Song> Songs { get; set; }
    }

    public class Song
    {
        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Name { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required, StringLength(200)]
        public string Composer { get; set; }
        public int TrackNumber { get; set; }
        public DateTime? ReleaseDateAsSingle { get; set; }

        [Required]
        public Album Album { get; set; }
    }

    public class Genre
    {
        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Name { get; set; }
    }

}
