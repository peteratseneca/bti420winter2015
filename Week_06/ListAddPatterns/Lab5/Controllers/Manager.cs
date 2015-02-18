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

        // ############################################################

        // ############################################################

        //AllAlbums
        //AllAlbumsList (ditto above)
        //AllSongs
        //AllSongsList (ditto above)
        //OneArtist
        //OneAlbum
        //OneSong
        //AddSong
    }
}
