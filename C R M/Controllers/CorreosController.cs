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
    public class CorreosController : Controller
    {
        private CRMEntities db = new CRMEntities();

        // GET: Correos
        public ActionResult Index(int? id)
        {
            var correo = db.Correo.Include(c => c.Contacto1);
            return View(correo.ToList().Where(x=>x.Contacto==id));
        }

        // GET: Correos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Correo correo = db.Correo.Find(id);
            if (correo == null)
            {
                return HttpNotFound();
            }
            return View(correo);
        }

        // GET: Correos/Create
        public ActionResult Create()
        {
            ViewBag.Contacto = new SelectList(db.Contacto, "Id_Contacto", "Nombre");
            return View();
        }

        // POST: Correos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Correo,Correo1,Contacto")] Correo correo)
        {
            if (ModelState.IsValid)
            {
                db.Correo.Add(correo);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = correo.Contacto });
            }

            ViewBag.Contacto = new SelectList(db.Contacto, "Id_Contacto", "Nombre", correo.Contacto);
            return View(correo);
        }

        // GET: Correos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Correo correo = db.Correo.Find(id);
            if (correo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Contacto = new SelectList(db.Contacto, "Id_Contacto", "Nombre", correo.Contacto);
            return View(correo);
        }

        // POST: Correos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Correo,Correo1,Contacto")] Correo correo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(correo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index",new { id = correo.Contacto});
            }
            ViewBag.Contacto = new SelectList(db.Contacto, "Id_Contacto", "Nombre", correo.Contacto);
            return View(correo);
        }

        // GET: Correos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Correo correo = db.Correo.Find(id);
            if (correo == null)
            {
                return HttpNotFound();
            }
            return View(correo);
        }

        // POST: Correos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Correo correo = db.Correo.Find(id);
            var idex = correo.Contacto;
            db.Correo.Remove(correo);
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
