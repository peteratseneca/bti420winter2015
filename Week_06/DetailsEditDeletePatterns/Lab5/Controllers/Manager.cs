using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using Lab5.Models;
using AutoMapper;

namespace Lab5.Controllers
{
    public class Manager
    {
        private DataContext ds = new DataContext();

        public IEnumerable<string> AllGenres()
        {
            return ds.Genres.OrderBy(g => g.Name).Select(g => g.Name).ToList();
        }

        // ############################################################

        // All artists

        public IEnumerable<ArtistBase> AllArtists()
        {
            var fetchedObjects = ds.Artists.OrderBy(a => a.Name);

            return Mapper.Map<IEnumerable<ArtistBase>>(fetchedObjects);
        }

        // All artists for a user interface list 
        public IEnumerable<ArtistList> AllArtistsList()
        {
            var fetchedObjects = ds.Artists.OrderBy(a => a.Name);

            return Mapper.Map<IEnumerable<ArtistList>>(fetchedObjects);
        }

        // Add artist
        public ArtistBase AddArtist(ArtistAdd newItem)
        {
            var addedItem = ds.Artists.Add(Mapper.Map<Artist>(newItem));
            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<ArtistBase>(addedItem);
        }

        // Get artist, with albums
        public ArtistBaseWithAlbums GetArtistByIdWithAlbums(int id)
        {
            // Notice that we must use SingleOrDefault,
            // because we have used the Include() method
            var fetchedObject = ds.Artists
                .Include("Albums")
                .SingleOrDefault(a => a.Id == id);

            return (fetchedObject == null)
                ? null
                : Mapper.Map<ArtistBaseWithAlbums>(fetchedObject);
        }

        // ############################################################

        // All albums
        public IEnumerable<AlbumBase> AllAlbums()
        {
            // added the Include() method

            var fetchedObjects = ds.Albums.Include("Artist").OrderBy(a => a.Name);

            return Mapper.Map<IEnumerable<AlbumBase>>(fetchedObjects);
        }

        // All albums list
        public IEnumerable<AlbumList> AllAlbumsList()
        {
            // added the Include() method

            var fetchedObjects = ds.Albums.Include("Artist").OrderBy(a => a.Name);

            return Mapper.Map<IEnumerable<AlbumList>>(fetchedObjects);
        }

        // Add album
        public AlbumBase AddAlbum(AlbumAdd newItem)
        {
            // Validate the incoming data
            var artist = ds.Artists.Find(newItem.ArtistId);

            if (artist == null)
            {
                return null;
            }
            else
            {
                var addedItem = ds.Albums.Add(Mapper.Map<Album>(newItem));
                addedItem.Artist = artist;
                ds.SaveChanges();

                return (addedItem == null) ? null : Mapper.Map<AlbumBase>(addedItem);
            }
        }

        // Edit album
        public AlbumBase EditAlbum(AlbumEdit newItem)
        {
            // Validate the incoming data
            var fetchedObject = ds.Albums
                .Include("Artist")
                .SingleOrDefault(a => a.Id == newItem.Id);

            if (fetchedObject == null)
            {
                return null;
            }
            else
            {
                // Update the object with the incoming values
                // Before doing this, we may have to do some business-rule validations
                ds.Entry(fetchedObject).CurrentValues.SetValues(newItem);
                ds.SaveChanges();

                // Prepare and return the object
                return Mapper.Map<AlbumBase>(fetchedObject);
            }
        }

        // Delete album
        public bool DeleteAlbumById(int id)
        {
            // Attempt to fetch the object to be deleted
            var itemToDelete = ds.Albums.Find(id);

            if (itemToDelete == null)
            {
                return false;
            }
            else
            {
                // Un-comment these statements to enable delete
                //ds.Albums.Remove(itemToDelete);
                //ds.SaveChanges();

                // Also... must respect object associations
                // Cannot delete a 'parent' if 'children' exist
                // So, may have to change the fetch (above),
                // and see whether it has any child objects

                return true;
            }
        }


        // Get album
        public AlbumBase GetAlbumById(int id)
        {
            var fetchedObject = ds.Albums.Include("Artist").SingleOrDefault(a => a.Id == id);

            return (fetchedObject == null)
                ? null
                : Mapper.Map<AlbumBase>(fetchedObject);
        }

        // Get album, with songs
        public AlbumBaseWithSongs GetAlbumByIdWithSongs(int id)
        {
            // Notice that we must use SingleOrDefault,
            // because we have used the Include() method
            var fetchedObject = ds.Albums
                .Include("Artist")
                .Include("Songs")
                .SingleOrDefault(a => a.Id == id);

            return (fetchedObject == null)
                ? null
                : Mapper.Map<AlbumBaseWithSongs>(fetchedObject);
        }

        // ############################################################

        // All songs
        public IEnumerable<SongBase> AllSongs()
        {
            // added the Include() method

            var fetchedObjects = ds.Songs.Include("Album.Artist").OrderBy(a => a.Name);

            return Mapper.Map<IEnumerable<SongBase>>(fetchedObjects);
        }

        // Add song
        public SongBase AddSong(SongAdd newItem)
        {
            // Validate the incoming data
            var album = ds.Albums.Find(newItem.AlbumId);

            if (album == null)
            {
                return null;
            }
            else
            {
                var addedItem = ds.Songs.Add(Mapper.Map<Song>(newItem));
                addedItem.Album = album;
                ds.SaveChanges();

                return (addedItem == null) ? null : Mapper.Map<SongBase>(addedItem);
            }
        }

        // Get song
        public SongBase GetSongById(int id)
        {
            // Notice that we must use SingleOrDefault,
            // because we have used the Include() method
            var fetchedObject = ds.Songs
                .Include("Album.Artist")
                .SingleOrDefault(a => a.Id == id);

            return (fetchedObject == null)
                ? null
                : Mapper.Map<SongBase>(fetchedObject);
        }

        // ############################################################

    }
}
