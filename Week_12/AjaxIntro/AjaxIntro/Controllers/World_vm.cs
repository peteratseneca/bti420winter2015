using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.Web.Mvc;

namespace AjaxIntro.Controllers
{
    public class CountryBase
    {
        public CountryBase()
        {
            this.Regions = new List<RegionBase>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<RegionBase> Regions { get; set; }
    }

    public class RegionBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CountryBase Country { get; set; }
    }

    public class InfoForm
    {
        public SelectList Countries { get; set; }
    }

    public class InfoFormRegions
    {
        public SelectList Regions { get; set; }
    }

    public class InfoResult
    {
        public int Country { get; set; }

        public int Region { get; set; }
    }

}