using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;

namespace Games.Controllers
{
    public class VenueList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class VenueAddForm
    {
        [Required, StringLength(50)]
        [Display(Name = "Venue name")]
        public string Name { get; set; }

        [Required, StringLength(5000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required, StringLength(200)]
        public string Location { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Sports at this venue")]
        public string SportNames { get; set; }
    }

    public class VenueAdd
    {
        [Required, StringLength(50)]
        [Display(Name = "Venue name")]
        public string Name { get; set; }

        [Required, StringLength(5000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required, StringLength(200)]
        public string Location { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Sports at this venue")]
        public string SportNames { get; set; }

        public HttpPostedFileBase PhotoUpload { get; set; }
        public HttpPostedFileBase MapUpload { get; set; }
    }

    public class VenueBase
    {
        private string url = "";

        public VenueBase()
        {
            // Get the base URL
            url = HttpContext.Current.Request.Url.GetLeftPart(System.UriPartial.Authority);
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string SportNames { get; set; }
        public string MapUrl
        {
            get
            {
                return string.Format("{0}/image/venue/map/{1}", url, Id);
            }
        }
        public string PhotoUrl
        {
            get
            {
                return string.Format("{0}/image/venue/photo/{1}", url, Id);
            }
        }

    }

    public class VenueBaseWithImage : VenueAdd
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoContentType { get; set; }
        public byte[] Map { get; set; }
        public string MapContentType { get; set; }
    }

    public class VenueBaseWithSports : VenueBase 
    {
        public VenueBaseWithSports()
        {
            this.Sports = new List<SportBase>();
        }

        public IEnumerable<SportBase> Sports { get; set; }
    }

    // ############################################################
    // For web service

    public class VenueWithLink : VenueBase
    {
        public Link Link { get; set; }
    }

    public class VenueLinked : LinkedItem<VenueWithLink> 
    { 
        // A refactoring experiment...
        public VenueLinked(VenueWithLink item)
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

    public class VenuesLinked : LinkedCollection<VenueWithLink> 
    { 
        // A refactoring experiment...
        public VenuesLinked(IEnumerable<VenueWithLink> collection)
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

    public class VenueWithSportsWithLink : VenueBaseWithSports
    {
        public Link Link { get; set; }
    }

    public class VenueWithSportsLinked : LinkedItem<VenueWithSportsWithLink>
    {
        // A refactoring experiment...
        public VenueWithSportsLinked(VenueWithSportsWithLink item)
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

    public class VenuesWithSportsLinked : LinkedCollection<VenueWithSportsWithLink>
    {
        // A refactoring experiment...
        public VenuesWithSportsLinked(IEnumerable<VenueWithSportsWithLink> collection)
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

}
