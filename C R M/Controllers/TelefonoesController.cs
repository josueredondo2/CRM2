using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using C_R_M.Models;

namespace C_R_M.Controllers
{
    public class TelefonoesController : Controller
    {
        private CRMEntities db = new CRMEntities();

        // GET: Telefonoes
        public ActionResult Index(int? id)
        {
            var telefono = db.Telefono.Include(t => t.Contacto1);
            return View(telefono.ToList().Where(x => x.Contacto==id));
        }

        // GET: Telefonoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefono telefono = db.Telefono.Find(id);
            if (telefono == null)
            {
                return HttpNotFound();
            }
            return View(telefono);
        }

        // GET: Telefonoes/Create
        public ActionResult Create()
        {
            ViewBag.Contacto = new SelectList(db.Contacto, "Id_Contacto", "Nombre");
            return View();
        }

        // POST: Telefonoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Telefono,Telefono1,Contacto")] Telefono telefono)
        {
            if (ModelState.IsValid)
            {
                db.Telefono.Add(telefono);
                db.SaveChanges();
                return RedirectToAction("Index",new {id=telefono.Contacto});
            }

            ViewBag.Contacto = new SelectList(db.Contacto, "Id_Contacto", "Nombre", telefono.Contacto);
            return View(telefono);
        }

        // GET: Telefonoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefono telefono = db.Telefono.Find(id);
            if (telefono == null)
            {
                return HttpNotFound();
            }
            ViewBag.Contacto = new SelectList(db.Contacto, "Id_Contacto", "Nombre", telefono.Contacto);
            return View(telefono);
        }

        // POST: Telefonoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Telefono,Telefono1,Contacto")] Telefono telefono)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telefono).State = EntityState.Modified;
                db.SaveChanges();
                 return RedirectToAction("Index",new {id=telefono.Contacto});
            }
            ViewBag.Contacto = new SelectList(db.Contacto, "Id_Contacto", "Nombre", telefono.Contacto);
            return View(telefono);
        }

        // GET: Telefonoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefono telefono = db.Telefono.Find(id);
            if (telefono == null)
            {
                return HttpNotFound();
            }
            return View(telefono);
        }

        // POST: Telefonoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Telefono telefono = db.Telefono.Find(id);
            var idex = telefono.Contacto.Value;
            db.Telefono.Remove(telefono);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = idex });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
