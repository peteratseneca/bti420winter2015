using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using Games.Models;
using AutoMapper;

namespace Games.Controllers
{
    public class Manager
    {
        private ApplicationDbContext ds = new ApplicationDbContext();

        // ############################################################
        // Sports

        public SportBase AddSport(SportAdd newItem)
        {
            var addedItem = ds.Sports.Add(Mapper.Map<Sport>(newItem));

            // Handle the uploaded logo...

            // First, extract the bytes from the HttpPostedFile object
            byte[] logoBytes = new byte[newItem.LogoUpload.ContentLength];
            newItem.LogoUpload.InputStream.Read(logoBytes, 0, newItem.LogoUpload.ContentLength);

            // Then, configure the new object's properties
            addedItem.Logo = logoBytes;
            addedItem.LogoContentType = newItem.LogoUpload.ContentType;

            // Handle the uploaded photo...

            // First, extract the bytes from the HttpPostedFile object
            byte[] photoBytes = new byte[newItem.PhotoUpload.ContentLength];
            newItem.PhotoUpload.InputStream.Read(photoBytes, 0, newItem.PhotoUpload.ContentLength);

            // Then, configure the new object's properties
            addedItem.Photo = photoBytes;
            addedItem.PhotoContentType = newItem.PhotoUpload.ContentType;

            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<SportBase>(addedItem);
        }

        public IEnumerable<SportBase> AllSports()
        {
            var fetchedObjects = ds.Sports.OrderBy(s => s.Name);

            return Mapper.Map<IEnumerable<SportBase>>(fetchedObjects);
        }

        public IEnumerable<SportList> AllSportsForList()
        {
            var fetchedObjects = ds.Sports.OrderBy(s => s.Name);

            return Mapper.Map<IEnumerable<SportList>>(fetchedObjects);
        }

        public SportBase GetSportById(int id)
        {
            var fetchedObject = ds.Sports.Find(id);

            return (fetchedObject == null) ? null : Mapper.Map<SportBase>(fetchedObject);
        }

        public SportBaseWithVenues GetSportByIdWithVenues(int id)
        {
            var fetchedObject = ds.Sports
                .Include("Venues")
                .SingleOrDefault(i => i.Id == id);

            return (fetchedObject == null) ? null : Mapper.Map<SportBaseWithVenues>(fetchedObject);
        }

        public SportBaseWithImage GetSportByIdWithImage(int id)
        {
            var fetchedObject = ds.Sports.Find(id);

            return (fetchedObject == null) ? null : Mapper.Map<SportBaseWithImage>(fetchedObject);
        }

        public SportBase EditSport(SportEdit newItem)
        {
            // Validate the incoming data
            var fetchedObject = ds.Sports
                .Include("Venues")
                .SingleOrDefault(i => i.Id == newItem.Id);

            if (fetchedObject == null)
            {
                return null;
            }
            else
            {
                // Update the object with the incoming values
                // Before doing this, we may have to do some business-rule validations
                ds.Entry(fetchedObject).CurrentValues.SetValues(newItem);

                // The object that came back has a VenueIds collection
                // This is the to-many relationship data
                // We must re-create the collection

                // First, clear the collection
                fetchedObject.Venues.Clear();
                ds.SaveChanges();

                /*
                // Alternative to the above two statements
                while (fetchedObject.Venues.Count() > 0)
                {
                    // Get the first object
                    var firstObject = fetchedObject.Venues.First();
                    // Remove it
                    fetchedObject.Venues.Remove(firstObject);
                    ds.SaveChanges();
                }
                */

                // Next, go through the incoming list, and add these items to the collection
                foreach (var venueId in newItem.VenueIds)
                {
                    // Attempt to validate the venueId as an int
                    int id;
                    bool isNumber = Int32.TryParse(venueId, out id);

                    if (isNumber)
                    {
                        // Attempt to fetch the object
                        var venue = ds.Venues.Find(id);

                        if (venue != null)
                        {
                            fetchedObject.Venues.Add(venue);
                        }
                    }

                }

                ds.SaveChanges();

                // Prepare and return the object
                return Mapper.Map<SportBase>(fetchedObject);
            }
        }

        // ############################################################
        // Venues

        public VenueBase AddVenue(VenueAdd newItem)
        {
            var addedItem = ds.Venues.Add(Mapper.Map<Venue>(newItem));

            // Handle the uploaded photo...

            // First, extract the bytes from the HttpPostedFile object
            byte[] photoBytes = new byte[newItem.PhotoUpload.ContentLength];
            newItem.PhotoUpload.InputStream.Read(photoBytes, 0, newItem.PhotoUpload.ContentLength);

            // Then, configure the new object's properties
            addedItem.Photo = photoBytes;
            addedItem.PhotoContentType = newItem.PhotoUpload.ContentType;

            // Handle the uploaded map...

            // First, extract the bytes from the HttpPostedFile object
            byte[] mapBytes = new byte[newItem.MapUpload.ContentLength];
            newItem.MapUpload.InputStream.Read(mapBytes, 0, newItem.MapUpload.ContentLength);

            // Then, configure the new object's properties
            addedItem.Map = mapBytes;
            addedItem.MapContentType = newItem.MapUpload.ContentType;

            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<VenueBase>(addedItem);
        }

        public IEnumerable<VenueBase> AllVenues()
        {
            var fetchedObjects = ds.Venues.OrderBy(v => v.Name);

            return Mapper.Map<IEnumerable<VenueBase>>(fetchedObjects);
        }

        public IEnumerable<VenueBaseWithSports> AllVenuesWithSports()
        {
            var fetchedObjects = ds.Venues
                .Include("Sports")
                .OrderBy(v => v.Name);

            return Mapper.Map<IEnumerable<VenueBaseWithSports>>(fetchedObjects);
        }

        public IEnumerable<VenueList> AllVenuesForList()
        {
            var fetchedObjects = ds.Venues.OrderBy(v => v.Name);

            return Mapper.Map<IEnumerable<VenueList>>(fetchedObjects);
        }

        public VenueBase GetVenueById(int id)
        {
            var fetchedObject = ds.Venues.Find(id);

            return (fetchedObject == null) ? null : Mapper.Map<VenueBase>(fetchedObject);
        }

        public VenueBaseWithSports GetVenueByIdWithSports(int id)
        {
            var fetchedObject = ds.Venues.Include("Sports").SingleOrDefault(i => i.Id == id);

            return (fetchedObject == null) ? null : Mapper.Map<VenueBaseWithSports>(fetchedObject);
        }

        public VenueBaseWithImage GetVenueByIdWithImage(int id)
        {
            var fetchedObject = ds.Venues.Find(id);

            return (fetchedObject == null) ? null : Mapper.Map<VenueBaseWithImage>(fetchedObject);
        }


    }
}
