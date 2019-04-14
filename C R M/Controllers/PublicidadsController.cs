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
    public class PublicidadsController : Controller
    {
        private CRMEntities db = new CRMEntities();

        // GET: Publicidads
        public async Task<ActionResult> Index()
        {
            var publicidad = db.Publicidad.Include(p => p.Empresa1).Include(p => p.EstadodeCuenta).Include(p => p.MedioPublicitario);
            return View(await publicidad.ToListAsync());
        }

        // GET: Publicidads/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicidad publicidad = await db.Publicidad.FindAsync(id);
            if (publicidad == null)
            {
                return HttpNotFound();
            }
            return View(publicidad);
        }

        // GET: Publicidads/Create
        public ActionResult Create()
        {
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre");
            ViewBag.Credito_Disponible = new SelectList(db.EstadodeCuenta, "Id_Estado", "Id_Estado");
            ViewBag.Medio = new SelectList(db.MedioPublicitario, "Id_Medio_Publicitario", "Nombre");
            return View();
        }

        // POST: Publicidads/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id_Publicidad,Medio,Empresa,Credito_Disponible,Fecha_Inicio,Fecha_Caducidad,Costo")] Publicidad publicidad)
        {
            if (ModelState.IsValid)
            {
                db.Publicidad.Add(publicidad);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", publicidad.Empresa);
            ViewBag.Credito_Disponible = new SelectList(db.EstadodeCuenta, "Id_Estado", "Id_Estado", publicidad.Credito_Disponible);
            ViewBag.Medio = new SelectList(db.MedioPublicitario, "Id_Medio_Publicitario", "Nombre", publicidad.Medio);
            return View(publicidad);
        }

        // GET: Publicidads/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicidad publicidad = await db.Publicidad.FindAsync(id);
            if (publicidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", publicidad.Empresa);
            ViewBag.Credito_Disponible = new SelectList(db.EstadodeCuenta, "Id_Estado", "Id_Estado", publicidad.Credito_Disponible);
            ViewBag.Medio = new SelectList(db.MedioPublicitario, "Id_Medio_Publicitario", "Nombre", publicidad.Medio);
            return View(publicidad);
        }

        // POST: Publicidads/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id_Publicidad,Medio,Empresa,Credito_Disponible,Fecha_Inicio,Fecha_Caducidad,Costo")] Publicidad publicidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publicidad).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", publicidad.Empresa);
            ViewBag.Credito_Disponible = new SelectList(db.EstadodeCuenta, "Id_Estado", "Id_Estado", publicidad.Credito_Disponible);
            ViewBag.Medio = new SelectList(db.MedioPublicitario, "Id_Medio_Publicitario", "Nombre", publicidad.Medio);
            return View(publicidad);
        }

        // GET: Publicidads/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicidad publicidad = await db.Publicidad.FindAsync(id);
            if (publicidad == null)
            {
                return HttpNotFound();
            }
            return View(publicidad);
        }

        // POST: Publicidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Publicidad publicidad = await db.Publicidad.FindAsync(id);
            db.Publicidad.Remove(publicidad);
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
