using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment4.Models;

namespace Assignment4.Controllers
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

        //STEPS FOR PROJECTS:
        /*
         1 - DM, DataContext
         2 - VM (InvoiceBase, InvoiceWithCustomer : Base)
         3 - AutoMapper
         4 - Manger methods
         5 - Controller (1st)
         6 - Views
         */

        public IEnumerable<InvoiceBase> InvoiceGetall()
        {
            return Mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceBase>>(ds.Invoices);
        }

        public InvoiceBase InvoiceGetById(int id)
        {
            var o = ds.Invoices.Find(id);
            return (o == null) ? null : Mapper.Map<Invoice, InvoiceBase>(o);
        }

        public InvoiceWithDetail InvoiceWithDetailsGetById(int id)
        {
            //chain Include("XX").Include("YY")..

            //var invoice = ds.Invoices.Include("Customer.Employee").Include("InvoiceLines.Track.Album.Artist").Include("InvoiceLines.Track.MediaType").SingleOrDefault(i=> i.InvoiceId == id);
            var invoice = ds.Invoices.Include("Customer.Employee").Include("Customer").
                Include("InvoiceLines.Track.Album.Artist").Include("InvoiceLines.Track.MediaType").SingleOrDefault(ce => ce.InvoiceId == id);
            return (invoice == null) ? null : Mapper.Map<Invoice, InvoiceWithDetail>(invoice);
        }
    }
}