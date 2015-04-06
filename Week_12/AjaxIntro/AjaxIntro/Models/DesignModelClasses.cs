using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;

namespace AjaxIntro.Models
{
    public class Country
    {
        public Country()
        {
            this.Regions = new List<Region>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Region> Regions { get; set; }
    }

    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        public Country Country { get; set; }
    }

}
