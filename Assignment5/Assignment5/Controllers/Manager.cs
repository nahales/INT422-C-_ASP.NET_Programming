using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment5.Models;

namespace Assignment5.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        public Manager()
        {
            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;

            // If necessary, add more constructor code here...

        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()



        /** Album Methods **/
        public IEnumerable<AlbumBase> AlbumGetAll()
        {
            return Mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBase>>(ds.Albums.OrderBy(p => p.AlbumId));
        }

        public AlbumBase AlbumGetById(int? id)
        {
            return Mapper.Map<Album, AlbumBase>(ds.Albums.Find(id.GetValueOrDefault()));
        }


        /** Artist Methods **/
        public IEnumerable<ArtistBase> ArtistGetAll()
        {
            return Mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBase>>(ds.Artists.OrderBy(item => item.ArtistId));
        }
        public ArtistBase ArtistGetById(int? id)
        {
            return Mapper.Map<Artist, ArtistBase>(ds.Artists.Find(id.GetValueOrDefault()));
        }


        /** MediaType Methods **/
        public IEnumerable<MediaTypeBase> MediaTypeGetAll()
        {
            return Mapper.Map<IEnumerable<MediaType>, IEnumerable<MediaTypeBase>>(ds.MediaTypes.OrderBy(item => item.MediaTypeId));
        }

        public MediaTypeBase MediaTypeGetById(int? id)
        {
            return Mapper.Map<MediaType, MediaTypeBase>(ds.MediaTypes.Find(id.GetValueOrDefault()));
        }


        /** Track Methods **/
        public IEnumerable<TrackWithDetail> TrackGetAll()
        {
            return Mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetail>>(ds.Tracks.Include("Album.Artist").Include("Mediatype").OrderBy(p => p.Album.Artist.Name));
        }

        public TrackWithDetail TrackGetById(int? id)
        {
            return Mapper.Map<Track, TrackWithDetail>(ds.Tracks.Include("Album.Artist").Include("Mediatype").Where(p => p.TrackId == id).Single());
        }

        public TrackBase TrackAdd(TrackAdd obj)
        {
            var media = ds.MediaTypes.SingleOrDefault(p => p.MediaTypeId == obj.MediaTypeId);
            var album = ds.Albums.SingleOrDefault(p => p.AlbumId == obj.AlbumId);

            if (media == null || album == null) { return null; }

            var forged_track = ds.Tracks.Add(Mapper.Map<TrackAdd, Track>(obj));
            forged_track.MediaType = media;
            forged_track.Album = album;

            ds.SaveChanges();

            return (forged_track == null) ? null : Mapper.Map<Track, TrackBase>(forged_track);
        }

    }
}