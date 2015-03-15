using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Games.Controllers
{
    public class SportList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SportAddForm
    {
        [Required, StringLength(50)]
        [Display(Name = "Sport name")]
        public string Name { get; set; }

        [Required, StringLength(5000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required, StringLength(5000)]
        [DataType(DataType.MultilineText)]
        public string History { get; set; }

        [Required, StringLength(5000)]
        [Display(Name = "How it works")]
        [DataType(DataType.MultilineText)]
        public string HowItWorks { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Venues")]
        public string VenueNames { get; set; }
    }

    public class SportAdd
    {
        [Required, StringLength(50)]
        [Display(Name = "Sport name")]
        public string Name { get; set; }

        [Required, StringLength(5000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required, StringLength(5000)]
        [DataType(DataType.MultilineText)]
        public string History { get; set; }

        [Required, StringLength(5000)]
        [Display(Name = "How it works")]
        [DataType(DataType.MultilineText)]
        public string HowItWorks { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Venues")]
        public string VenueNames { get; set; }

        public HttpPostedFileBase LogoUpload { get; set; }
        public HttpPostedFileBase PhotoUpload { get; set; }
    }

    public class SportEditForm
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "Sport name")]
        public string Name { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Venues")]
        public string VenueNames { get; set; }

        public MultiSelectList Venues { get; set; }
    }

    public class SportEdit
    {
        public SportEdit()
        {
            this.VenueIds = new List<string>();
        }

        [HiddenInput]
        public int Id { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "Sport name")]
        public string Name { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Venues")]
        public string VenueNames { get; set; }
        public IEnumerable<string> VenueIds { get; set; }
    }

    public class SportBase
    {
        private string url = "";

        public SportBase()
        {
            // Get the base URL
            url = HttpContext.Current.Request.Url.GetLeftPart(System.UriPartial.Authority);
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string History { get; set; }
        public string HowItWorks { get; set; }
        public string VenueNames { get; set; }
        public string LogoUrl 
        { 
            get
            {
                return string.Format("{0}/image/sport/logo/{1}", url, Id);
            }
        }
        public string PhotoUrl
        {
            get
            {
                return string.Format("{0}/image/sport/photo/{1}", url, Id);
            }
        }
    }

    public class SportBaseWithImage : SportAdd
    {
        public int Id { get; set; }
        public byte[] Logo { get; set; }
        public string LogoContentType { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoContentType { get; set; }
    }

    public class SportBaseWithVenues : SportBase 
    {
        public IEnumerable<VenueBase> Venues { get; set; }
    }

    // ############################################################
    // For web service

    public class SportWithLink : SportBase
    {
        public Link Link { get; set; }
    }

    public class SportLinked : LinkedItem<SportWithLink> 
    { 
        // A refactoring experiment...
        public SportLinked(SportWithLink item)
        {
            this.Item = item;

            // Get the current request URL
            var absolutePath = HttpContext.Current.Request.Url.AbsolutePath;

            // Link relation for 'self' in the item
            this.Item.Link = new Link() { Rel = "self", Href = absolutePath };

            // Link relation for 'self'
            this.Links.Add(new Link() { Rel = "self", Href = absolutePath });
            //this.Links.Add(this.Item.Link);

            // Link relation for 'collection'
            string[] u = absolutePath.Split(new char[] { '/' });
            this.Links.Add(new Link() { Rel = "collection", Href = string.Format("/{0}/{1}", u[1], u[2]) });
        }
    }

    public class SportsLinked : LinkedCollection<SportWithLink> 
    { 
        // A refactoring experiment...
        public SportsLinked(IEnumerable<SportWithLink> collection)
        {
            this.Collection = collection;

            // Get the current request URL
            var absolutePath = HttpContext.Current.Request.Url.AbsolutePath;

            // Link relation for 'self'
            this.Links.Add(new Link() { Rel = "self", Href = absolutePath });

            // Add 'item' links for each item in the collection
            foreach (var item in this.Collection)
            {
                item.Link = new Link() { Rel = "item", Href = string.Format("{0}/{1}", absolutePath, item.Id) };
            }
        }
    }

    // With template info, to do...

}
