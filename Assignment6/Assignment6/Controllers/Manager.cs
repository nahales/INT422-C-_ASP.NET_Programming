using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment6.Models;

namespace Assignment6.Controllers
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


        public IEnumerable<TrackBase> TracksGetAll()
        {
            var o = ds.Tracks.OrderBy(item => item.Name);
            return Mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(o);
        }


        public IEnumerable<PlaylistWithDetails> PlaylistGetAll()
        {
            var o = ds.Playlists.Include("Tracks").OrderBy(item => item.Name);
            return Mapper.Map<IEnumerable<Playlist>, IEnumerable<PlaylistWithDetails>>(o);
        }

        public PlaylistWithDetails PlaylistGetById(int? id)
        {
            var o = ds.Playlists.Include("Tracks").SingleOrDefault(e => e.PlaylistId == id);
            return (o == null) ? null : Mapper.Map<Playlist, PlaylistWithDetails>(o);
        }

        public PlaylistWithDetails PlaylistEdit(PlaylistEditTracks edit)
        {
            // Attempt to fetch the object
            var o = ds.Playlists.Include("Tracks")
                .SingleOrDefault(e => e.PlaylistId == edit.PlaylistId);

            if (o == null)
            {
                return null;
            }
            else
            {
                o.Tracks.Clear(); ;

                foreach (var item in edit.TracksIds)
                {
                    // Search through the datacontext looking for each Track in plEdit.TrackList
                    var a = ds.Tracks.Find(item);
                    // Add the result
                    o.Tracks.Add(a);
                }
                // Save the changes
                ds.SaveChanges();

                return Mapper.Map<Playlist, PlaylistWithDetails>(o);
            }
        }



    }
}