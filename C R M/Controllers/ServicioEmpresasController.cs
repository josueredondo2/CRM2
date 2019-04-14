using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Index()
        {
            var servicioEmpresa = db.ServicioEmpresa.Include(s => s.Empresa1).Include(s => s.Producto);
            return View(await servicioEmpresa.ToListAsync());
        }

        // GET: ServicioEmpresas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicioEmpresa servicioEmpresa = await db.ServicioEmpresa.FindAsync(id);
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
        public async Task<ActionResult> Create([Bind(Include = "Id_Servicio_Empresa,Id_Producto,Descripcion,Fecha_Creacion,Primer_Pago,Renovacion,Empresa,Precio")] ServicioEmpresa servicioEmpresa)
        {
            if (ModelState.IsValid)
            {
                db.ServicioEmpresa.Add(servicioEmpresa);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", servicioEmpresa.Empresa);
            ViewBag.Id_Producto = new SelectList(db.Producto, "Id_Producto", "Nombre", servicioEmpresa.Id_Producto);
            return View(servicioEmpresa);
        }

        // GET: ServicioEmpresas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicioEmpresa servicioEmpresa = await db.ServicioEmpresa.FindAsync(id);
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
        public async Task<ActionResult> Edit([Bind(Include = "Id_Servicio_Empresa,Id_Producto,Descripcion,Fecha_Creacion,Primer_Pago,Renovacion,Empresa,Precio")] ServicioEmpresa servicioEmpresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicioEmpresa).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", servicioEmpresa.Empresa);
            ViewBag.Id_Producto = new SelectList(db.Producto, "Id_Producto", "Nombre", servicioEmpresa.Id_Producto);
            return View(servicioEmpresa);
        }

        // GET: ServicioEmpresas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicioEmpresa servicioEmpresa = await db.ServicioEmpresa.FindAsync(id);
            if (servicioEmpresa == null)
            {
                return HttpNotFound();
            }
            return View(servicioEmpresa);
        }

        // POST: ServicioEmpresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ServicioEmpresa servicioEmpresa = await db.ServicioEmpresa.FindAsync(id);
            db.ServicioEmpresa.Remove(servicioEmpresa);
            await db.SaveChangesAsync();
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
