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
    public class EmpresasController : Controller
    {
        private CRMEntities db = new CRMEntities();

        // GET: Empresas
        public ActionResult Index()
        {
            var empresa = db.Empresa.Include(e => e.Canton).Include(e => e.Distrito).Include(e => e.Provincia);
            return View(empresa.ToList());
        }

        // GET: Empresas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresa.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // GET: Empresas/Create
        public ActionResult Create()
        {
            ViewBag.Id_Canton = new SelectList(db.Canton, "Id_Canton", "Nombre");
            ViewBag.Id_Distrito = new SelectList(db.Distrito, "Id_Distrito", "Nombre");
            ViewBag.Id_Provincia = new SelectList(db.Provincia, "Id_Provincia", "Nombre");
            return View();
        }

        // POST: Empresas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Empresa,Nombre,Correo,Cedula,Pais,Id_Provincia,Id_Canton,Id_Distrito,Otras_Señas,Codigo_Postal")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Empresa.Add(empresa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Canton = new SelectList(db.Canton, "Id_Canton", "Nombre", empresa.Id_Canton);
            ViewBag.Id_Distrito = new SelectList(db.Distrito, "Id_Distrito", "Nombre", empresa.Id_Distrito);
            ViewBag.Id_Provincia = new SelectList(db.Provincia, "Id_Provincia", "Nombre", empresa.Id_Provincia);
            return View(empresa);
        }

        // GET: Empresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresa.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Canton = new SelectList(db.Canton, "Id_Canton", "Nombre", empresa.Id_Canton);
            ViewBag.Id_Distrito = new SelectList(db.Distrito, "Id_Distrito", "Nombre", empresa.Id_Distrito);
            ViewBag.Id_Provincia = new SelectList(db.Provincia, "Id_Provincia", "Nombre", empresa.Id_Provincia);
            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Empresa,Nombre,Correo,Cedula,Pais,Id_Provincia,Id_Canton,Id_Distrito,Otras_Señas,Codigo_Postal")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Canton = new SelectList(db.Canton, "Id_Canton", "Nombre", empresa.Id_Canton);
            ViewBag.Id_Distrito = new SelectList(db.Distrito, "Id_Distrito", "Nombre", empresa.Id_Distrito);
            ViewBag.Id_Provincia = new SelectList(db.Provincia, "Id_Provincia", "Nombre", empresa.Id_Provincia);
            return View(empresa);
        }

        // GET: Empresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresa.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empresa empresa = db.Empresa.Find(id);
            db.Empresa.Remove(empresa);
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
