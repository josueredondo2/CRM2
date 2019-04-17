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
    public class ContactoesController : Controller
    {
        private CRMEntities db = new CRMEntities();

        // GET: Contactoes
        public ActionResult Index()
        {
            var contacto = db.Contacto.Include(c => c.Empresa1);
            return View(contacto.ToList());
        }

        // GET: Contactoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contacto contacto = db.Contacto.Find(id);
            if (contacto == null)
            {
                return HttpNotFound();
            }
            return View(contacto);
        }

        // GET: Contactoes/Create
        public ActionResult Create()
        {
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre");
            return View();
        }

        // POST: Contactoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Contacto,Nombre,Apellido1,Apellido2,Puesto,Empresa")] Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                db.Contacto.Add(contacto);
                db.SaveChanges();
                return RedirectToAction("index","Empresas",new {id = 1});
            }

            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", contacto.Empresa);
            return View(contacto);
        }

        // GET: Contactoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contacto contacto = db.Contacto.Find(id);
            if (contacto == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", contacto.Empresa);
            return View(contacto);
        }

        // POST: Contactoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Contacto,Nombre,Apellido1,Apellido2,Puesto,Empresa")] Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contacto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Empresas", new { id = 1 });
            }
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", contacto.Empresa);
            return View(contacto);
        }

        // GET: Contactoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contacto contacto = db.Contacto.Find(id);
            if (contacto == null)
            {
                return HttpNotFound();
            }
            return View(contacto);
        }

        // POST: Contactoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contacto contacto = db.Contacto.Find(id);
            db.Contacto.Remove(contacto);
            db.SaveChanges();
            return RedirectToAction("Index");
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
