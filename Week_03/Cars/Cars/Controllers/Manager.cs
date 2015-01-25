using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// These 'using' directives were added...
using System.IO;
using Biggy.Core;
using Biggy.Data.Json;
using Cars.Models;
using AutoMapper;

namespace Cars.Controllers
{
    public class Manager
    {
        // Private fields that point to the data store and the collection(s)
        private JsonStore<Vehicle> store;
        private BiggyList<Vehicle> vehicles;

        // Public property, read only, with a getter (no setter)
        // Notice the OrderBy method
        // Notice the return type - must be an IEnumerable of a view model type
        public IEnumerable<VehicleBase> AllVehicles
        {
            get { return Mapper.Map<IEnumerable<VehicleBase>>(vehicles.OrderBy(mdl => mdl.Model)); }
        }

        public Manager()
        {
            // Configure AutoMapper
            Mapper.CreateMap<Vehicle, VehicleBase>();
            Mapper.CreateMap<VehicleAdd, Vehicle>();

            // Path to the app's read-write file system location
            var localData = HttpContext.Current.Server.MapPath("~/App_Data");

            // Open (or create) the data store
            store = new JsonStore<Vehicle>(localData, "cars", "vehicles");
            // Assign (or create) the collection(s)
            vehicles = new BiggyList<Vehicle>(store);
        }

        public VehicleBase AddVehicle(VehicleAdd newItem)
        {
            // Calculate the next value for the identifier
            int newId = (vehicles.Count > 0) ? newId = vehicles.Max(id => id.Id) + 1 : 1;

            // Create a new item; notice the property mapping
            var addedItem = new Vehicle
            {
                Id = newId,
                Model = newItem.Model,
                Trim = newItem.Trim,
                ModelYear = newItem.ModelYear,
                MSRP = newItem.MSRP
            };

            // Add the new item to the store
            vehicles.Add(addedItem);

            // Return the new item
            return Mapper.Map<VehicleBase>(addedItem);
        }

        public VehicleBase GetVehicleById(int id)
        {
            // Attempt to fetch the item
            var fetchedObject = vehicles.SingleOrDefault(i => i.Id == id);

            // Return the result
            return (fetchedObject == null) ? null : Mapper.Map<VehicleBase>(fetchedObject);
        }

    }
}
