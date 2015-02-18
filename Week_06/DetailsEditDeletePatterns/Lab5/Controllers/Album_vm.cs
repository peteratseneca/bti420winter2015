using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace Lab5.Controllers
{
    public static partial class ConfigureMaps
    {
        public static void ForAlbum()
        {
            AutoMapper.Mapper.CreateMap<Models.Album, Controllers.AlbumBase>();
            AutoMapper.Mapper.CreateMap<Models.Album, Controllers.AlbumBaseWithSongs>();
            AutoMapper.Mapper.CreateMap<Models.Album, Controllers.AlbumList>();
            AutoMapper.Mapper.CreateMap<Controllers.AlbumAdd, Models.Album>();
            AutoMapper.Mapper.CreateMap<Controllers.AlbumBase, Controllers.AlbumEditForm>();
            AutoMapper.Mapper.CreateMap<Controllers.AlbumEdit, Controllers.AlbumEditForm>();
        }
    }

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

        [Display(Name = "Artist name")]
        public string ArtistName { get; set; }

        // Needed to support 'get one' scenarios
        public ArtistBase Artist { get; set; }
    }

    public class AlbumBaseWithSongs : AlbumBase
    {
        // For display only, so no constructor is needed
        // Make sure property name matches the 
        // name of the property in the design model class
        public IEnumerable<SongBase> Songs { get; set; }
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

    public class AlbumEdit
    {
        // For display only
        public int Id { get; set; }

        // For display only
        [Display(Name = "Artist name")]
        public string ArtistName { get; set; }

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

    public class AlbumEditForm
    {
        // For display only
        [HiddenInput]
        public int Id { get; set; }

        // For display only
        [Display(Name = "Artist name")]
        public string ArtistName { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Album name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Album release date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public SelectList Genres { get; set; }

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
