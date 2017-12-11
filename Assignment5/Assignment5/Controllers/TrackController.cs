using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    public class TrackController : Controller
    {
        private Manager m = new Manager();
        // GET: Track
        public ActionResult Index()
        {
            return View(m.TrackGetAll());
        }

        // GET: Track/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                return View(m.TrackGetById(id));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Track/Create
        public ActionResult Create()
        {
            TrackAddForm form = new Controllers.TrackAddForm();
            ViewData.Model = form;
            form.AlbumList = new SelectList(m.AlbumGetAll().OrderBy(p => p.Title), "AlbumId", "Title");
            form.MediaTypeList = new SelectList(m.MediaTypeGetAll().OrderBy(p => p.Name), "MediaTypeId", "Name");
            return View(form);
        }

        // POST: Track/Create
        [HttpPost]
        public ActionResult Create(TrackAdd obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            var add = m.TrackAdd(obj);
            if (add != null)
            {
                return View("Details", m.TrackGetById(add.TrackId));
            }
            else
            {
                return RedirectToAction("Create");
            }
        }

        // GET: Track/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Track/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Track/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Track/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
