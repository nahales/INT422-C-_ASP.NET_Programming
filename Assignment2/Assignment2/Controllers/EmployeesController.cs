using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment2.Controllers
{
    public class EmployeeController : Controller
    {
        private Manager m = new Manager();
        // GET: Employee
        public ActionResult Index()
        {
            return View(m.EmployeeGetAll());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id != null)
                return View(m.EmployeeGetById(id.GetValueOrDefault()));
            else return HttpNotFound();
        }

        // GET: Employee/Create
        public ActionResult Create() // get empty form
        {
            return View(new EmployeeAdd());
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(EmployeeAdd e)
        {
            try
            {
                // TODO: Add insert logic here
                if (e != null)
                {
                    var obj = m.EmployeeAddNew(e);
                    if (obj != null)
                    {
                        return RedirectToAction("Details/" + obj.EmployeeId);
                    }
                }
                else
                {
                    return HttpNotFound();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
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

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
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
