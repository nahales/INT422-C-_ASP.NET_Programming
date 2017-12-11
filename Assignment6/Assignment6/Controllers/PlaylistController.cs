using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

namespace Assignment6.Controllers
{
    public class PlaylistController : Controller
    {
        private Manager m = new Manager();
        // GET: Playlist
        public ActionResult Index()
        {
            return View(m.PlaylistGetAll());
        }

        // GET: Playlist/Details/5
        public ActionResult Details(int? id)
        {
            return View(m.PlaylistGetById(id.GetValueOrDefault()));
        }

        // GET: Playlist/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Playlist/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Playlist/Edit/5
        public ActionResult Edit(int? id)
        {
            var o = m.PlaylistGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var form = Mapper.Map<PlaylistEditTracksForm>(o);
                var selectedValues = o.Tracks.Select(t => t.TrackId);

                form.TrackList = new MultiSelectList
                    (items: m.TracksGetAll(),
                    dataValueField: "TrackId",
                    dataTextField: "Name",
                    selectedValues: selectedValues);

                form.TracksOnPlaylist = o.Tracks.OrderBy(t => t.Name);


                return View(form);
            }
        }

        // POST: Playlist/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, PlaylistEditTracks newItem)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Edit", new { id = newItem.PlaylistId });
                }

                if (id.GetValueOrDefault() != newItem.PlaylistId)
                {
                    return RedirectToAction("Index");
                }

                var o = m.PlaylistEdit(newItem);

                if (o == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Details", new { id = newItem.PlaylistId });
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Playlist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Playlist/Delete/5
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
