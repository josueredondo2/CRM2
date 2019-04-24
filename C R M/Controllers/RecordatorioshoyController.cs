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
    public class RecordatorioshoyController : Controller
    {
        private CRMEntities db = new CRMEntities();

        // GET: Recordatorioshoy
        public ActionResult Index()
        {
            var recordatorio = db.Recordatorio.Include(r => r.Empresa1).Include(r => r.Recordar);
            return View(recordatorio.ToList().Where(x=> x.Fecha.Value.Date == DateTime.Now.Date));
        }

        // GET: Recordatorioshoy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recordatorio recordatorio = db.Recordatorio.Find(id);
            if (recordatorio == null)
            {
                return HttpNotFound();
            }
            return View(recordatorio);
        }

        // GET: Recordatorioshoy/Create
        public ActionResult Create()
        {
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre");
            ViewBag.Id_Recordar = new SelectList(db.Recordar, "Id_Recordar", "Descripción");
            return View();
        }

        // POST: Recordatorioshoy/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Recordatorio,Tipo,Fecha,Hora,Minutos,Abreviatura,Detalle,Empresa,Id_Recordar")] Recordatorio recordatorio)
        {
            if (ModelState.IsValid)
            {
                db.Recordatorio.Add(recordatorio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", recordatorio.Empresa);
            ViewBag.Id_Recordar = new SelectList(db.Recordar, "Id_Recordar", "Descripción", recordatorio.Id_Recordar);
            return View(recordatorio);
        }

        // GET: Recordatorioshoy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recordatorio recordatorio = db.Recordatorio.Find(id);
            if (recordatorio == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", recordatorio.Empresa);
            ViewBag.Id_Recordar = new SelectList(db.Recordar, "Id_Recordar", "Descripción", recordatorio.Id_Recordar);
            return View(recordatorio);
        }

        // POST: Recordatorioshoy/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Recordatorio,Tipo,Fecha,Hora,Minutos,Abreviatura,Detalle,Empresa,Id_Recordar")] Recordatorio recordatorio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recordatorio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", recordatorio.Empresa);
            ViewBag.Id_Recordar = new SelectList(db.Recordar, "Id_Recordar", "Descripción", recordatorio.Id_Recordar);
            return View(recordatorio);
        }

        // GET: Recordatorioshoy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recordatorio recordatorio = db.Recordatorio.Find(id);
            if (recordatorio == null)
            {
                return HttpNotFound();
            }
            return View(recordatorio);
        }

        // POST: Recordatorioshoy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recordatorio recordatorio = db.Recordatorio.Find(id);
            db.Recordatorio.Remove(recordatorio);
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
