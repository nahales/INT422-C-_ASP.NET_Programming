using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment3.Models;

namespace Assignment3.Controllers
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

        public IEnumerable<EmployeeBase> EmployeeGetAll()
        {
            return Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeBase>>(ds.Employees);
        }

        public EmployeeBase EmployeeGetById(int id)
        {
            var valid = ds.Employees.Find(id);
            return (valid == null) ? null : Mapper.Map<Employee, EmployeeBase>(valid);
        }

        public EmployeeBase EmployeeEditContactInfo(EmployeeEdit e)
        {
            var o = ds.Employees.Find(e.EmployeeId);

            if (o != null)
            {
                ds.Entry(o).CurrentValues.SetValues(e);
                ds.SaveChanges();
                return Mapper.Map<Employee, EmployeeBase>(o);
            }
            return null;
        }//EmployeeEditContactInfo

        public IEnumerable<TrackBase> TrackGetAll()
        {
            return Mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(ds.Tracks.OrderBy(p => p.Name).OrderBy(p => p.Milliseconds));
        }

        public IEnumerable<TrackBase> TrackGetAllPop()
        {
            return Mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(ds.Tracks.Where(p => p.GenreId == 9).OrderBy(p => p.Name));
        }

        public IEnumerable<TrackBase> TrackGetAllDeepPurple()
        {
            return Mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(ds.Tracks.Where(p => p.Composer.Contains("Jon Lord")).OrderBy(p => p.TrackId));
        }

        public IEnumerable<TrackBase> TrackGetAllTop100Longest()
        {
            return Mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(ds.Tracks.OrderByDescending(p => p.Milliseconds).Take(100));
        }

    }//class
}//namespace