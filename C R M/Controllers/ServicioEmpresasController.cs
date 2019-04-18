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
    public class ServicioEmpresasController : Controller
    {
        private CRMEntities db = new CRMEntities();

        // GET: ServicioEmpresas
        public ActionResult Index()
        {
            var servicioEmpresa = db.ServicioEmpresa.Include(s => s.Empresa1).Include(s => s.Producto);
            return View(servicioEmpresa.ToList());
        }

        // GET: ServicioEmpresas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicioEmpresa servicioEmpresa = db.ServicioEmpresa.Find(id);
            if (servicioEmpresa == null)
            {
                return HttpNotFound();
            }
            return View(servicioEmpresa);
        }

        // GET: ServicioEmpresas/Create
        public ActionResult Create()
        {
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre");
            ViewBag.Id_Producto = new SelectList(db.Producto, "Id_Producto", "Nombre");
            return View();
        }

        // POST: ServicioEmpresas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Servicio_Empresa,Id_Producto,Descripcion,Fecha_Creacion,Primer_Pago,Renovacion,Empresa,Precio")] ServicioEmpresa servicioEmpresa)
        {
            if (ModelState.IsValid)
            {
                db.ServicioEmpresa.Add(servicioEmpresa);
                db.SaveChanges();
                return RedirectToAction("index", "Empresas", new { id = 1 });
            }

            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", servicioEmpresa.Empresa);
            ViewBag.Id_Producto = new SelectList(db.Producto, "Id_Producto", "Nombre", servicioEmpresa.Id_Producto);
            return View(servicioEmpresa);
        }

        // GET: ServicioEmpresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicioEmpresa servicioEmpresa = db.ServicioEmpresa.Find(id);
            if (servicioEmpresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", servicioEmpresa.Empresa);
            ViewBag.Id_Producto = new SelectList(db.Producto, "Id_Producto", "Nombre", servicioEmpresa.Id_Producto);
            return View(servicioEmpresa);
        }

        // POST: ServicioEmpresas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Servicio_Empresa,Id_Producto,Descripcion,Fecha_Creacion,Primer_Pago,Renovacion,Empresa,Precio")] ServicioEmpresa servicioEmpresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicioEmpresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index", "Empresas", new { id = 1 });
            }
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", servicioEmpresa.Empresa);
            ViewBag.Id_Producto = new SelectList(db.Producto, "Id_Producto", "Nombre", servicioEmpresa.Id_Producto);
            return View(servicioEmpresa);
        }

        // GET: ServicioEmpresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicioEmpresa servicioEmpresa = db.ServicioEmpresa.Find(id);
            if (servicioEmpresa == null)
            {
                return HttpNotFound();
            }
            return View(servicioEmpresa);
        }

        // POST: ServicioEmpresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServicioEmpresa servicioEmpresa = db.ServicioEmpresa.Find(id);
            db.ServicioEmpresa.Remove(servicioEmpresa);
            db.SaveChanges();
            return RedirectToAction("index", "Empresas", new { id = 1 });
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
