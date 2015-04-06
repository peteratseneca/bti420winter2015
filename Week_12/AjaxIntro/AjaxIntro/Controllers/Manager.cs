using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using AjaxIntro.Models;
using AutoMapper;

namespace AjaxIntro.Controllers
{
    public class Manager
    {
        private ApplicationDbContext ds = new ApplicationDbContext();

        public Manager()
        {
            Mapper.CreateMap<Models.Country, Controllers.CountryBase>();
            Mapper.CreateMap<Models.Region, Controllers.RegionBase>();
        }

        // Get all countries
        public IEnumerable<CountryBase> GetCountries()
        {
            return Mapper.Map<IEnumerable<CountryBase>>(ds.Countries.OrderBy(c => c.Name));
        }

        // Get country by its identifier
        public CountryBase GetCountryById(int id)
        {
            var fetchedObject = ds.Countries.Find(id);

            return fetchedObject == null ? null : Mapper.Map<CountryBase>(fetchedObject);
        }

        // Get region by its identifier
        public RegionBase GetRegionById(int id)
        {
            var fetchedObject = ds.Regions.Find(id);

            return fetchedObject == null ? null : Mapper.Map<RegionBase>(fetchedObject);
        }

        // Get regions by a country identifier
        public IEnumerable<RegionBase> GetRegionsByCountryId(int id)
        {
            var fetchedObject = ds.Countries.Include("Regions").SingleOrDefault(c => c.Id == id);

            return fetchedObject == null 
                ? null 
                : Mapper.Map<IEnumerable<RegionBase>>(fetchedObject.Regions.OrderBy(r => r.Name));
        }
    }
}
