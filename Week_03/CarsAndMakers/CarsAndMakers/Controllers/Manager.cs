using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// These 'using' directives were added...
using System.IO;
using Biggy.Core;
using Biggy.Data.Json;
using CarsAndMakersAndMakers.Models;
using AutoMapper;

namespace CarsAndMakers.Controllers
{
    public class Manager
    {
        // Private fields that point to the data store and the collection(s)
        private JsonStore<Vehicle> vehicleStore;
        private BiggyList<Vehicle> vehicles;
        private JsonStore<Manufacturer> manufacturerStore;
        private BiggyList<Manufacturer> manufacturers;

        // Public properties, read only, with a getter (no setter)
        // Notice the OrderBy method
        // Notice the return type - must be an IEnumerable of a view model type
        public IEnumerable<VehicleBase> AllVehicles
        {
            get { return Mapper.Map<IEnumerable<VehicleBase>>(vehicles.OrderBy(mdl => mdl.Model)); }
        }

        public IEnumerable<ManufacturerBase> AllManufacturers
        {
            get { return Mapper.Map<IEnumerable<ManufacturerBase>>(manufacturers.OrderBy(nm => nm.Name)); }
        }

        public IEnumerable<ManufacturerList> ListOfManufacturers
        {
            get { return Mapper.Map<IEnumerable<ManufacturerList>>(manufacturers.OrderBy(nm => nm.Name)); }
        }

        public Manager()
        {
            // Configure AutoMapper
            Mapper.CreateMap<Vehicle, VehicleBase>();
            Mapper.CreateMap<Vehicle, VehicleBaseWithManufacturer>();
            Mapper.CreateMap<VehicleAdd, Vehicle>();
            Mapper.CreateMap<Manufacturer, ManufacturerBase>();
            Mapper.CreateMap<Manufacturer, ManufacturerBaseWithVehicles>();
            Mapper.CreateMap<Manufacturer, ManufacturerList>();
            Mapper.CreateMap<ManufacturerAdd, Manufacturer>();

            // Path to the app's read-write file system location
            var localData = HttpContext.Current.Server.MapPath("~/App_Data");

            // Open (or create) the data store
            vehicleStore = new JsonStore<Vehicle>(localData, "CarsAndMakers", "vehicles");
            // Assign (or create) the collection
            vehicles = new BiggyList<Vehicle>(vehicleStore);

            // Do it again for the other entity class
            manufacturerStore = new JsonStore<Manufacturer>(localData, "CarsAndMakers", "manufacturers");
            manufacturers = new BiggyList<Manufacturer>(manufacturerStore);
        }

        public VehicleBase AddVehicle(VehicleAdd newItem)
        {
            // Calculate the next value for the identifier
            int newId = (vehicles.Count > 0) ? newId = vehicles.Max(id => id.Id) + 1 : 1;

            // Ensure that the new item's association property is value
            var associatedObject = manufacturers.SingleOrDefault(i => i.Id == newItem.ManufacturerId);
            if (associatedObject == null)
            {
                // Uh oh...
                return null;
            }

            // Create a new item; notice the property mapping
            var addedItem = new Vehicle
            {
                Id = newId,
                Model = newItem.Model,
                Trim = newItem.Trim,
                ModelYear = newItem.ModelYear,
                MSRP = newItem.MSRP,
                // New...
                Manufacturer = associatedObject
            };

            // Alternative... use AutoMapper to do the job...
            //addedItem = Mapper.Map<Vehicle>(newItem);
            //addedItem.Id = newId;
            //addedItem.Manufacturer = associatedObject;

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

        public IEnumerable<VehicleBaseWithManufacturer> AllVehiclesWithManufacturer()
        {
            // Fetch the collection
            var fetchedObjects = vehicles.OrderBy(mdl => mdl.Model).AsEnumerable();
            return Mapper.Map<IEnumerable<VehicleBaseWithManufacturer>>(fetchedObjects);
        }


        public ManufacturerBase AddManufacturer(ManufacturerAdd newItem)
        {
            // Calculate the next value for the identifier
            int newId = (manufacturers.Count > 0) ? newId = manufacturers.Max(id => id.Id) + 1 : 1;

            // Create a new item; notice the property mapping
            var addedItem = new Manufacturer
            {
                Id = newId,
                Name = newItem.Name,
                Country = newItem.Country,
                YearStarted = newItem.YearStarted
            };

            // Alternative... use AutoMapper to do the job...
            addedItem = Mapper.Map<Manufacturer>(newItem);
            addedItem.Id = newId;

            // Add the new item to the store
            manufacturers.Add(addedItem);

            // Return the new item
            return Mapper.Map<ManufacturerBase>(addedItem);
        }

        public ManufacturerBase GetManufacturerById(int id)
        {
            // Attempt to fetch the item
            var fetchedObject = manufacturers.SingleOrDefault(i => i.Id == id);

            // Return the result
            return (fetchedObject == null) ? null : Mapper.Map<ManufacturerBase>(fetchedObject);
        }

    }

}
