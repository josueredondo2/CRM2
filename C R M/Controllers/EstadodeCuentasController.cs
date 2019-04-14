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
    public class EstadodeCuentasController : Controller
    {
        private CRMEntities db = new CRMEntities();

        // GET: EstadodeCuentas
        public async Task<ActionResult> Index()
        {
            var estadodeCuenta = db.EstadodeCuenta.Include(e => e.Empresa1);
            return View(await estadodeCuenta.ToListAsync());
        }

        // GET: EstadodeCuentas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadodeCuenta estadodeCuenta = await db.EstadodeCuenta.FindAsync(id);
            if (estadodeCuenta == null)
            {
                return HttpNotFound();
            }
            return View(estadodeCuenta);
        }

        // GET: EstadodeCuentas/Create
        public ActionResult Create()
        {
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre");
            return View();
        }

        // POST: EstadodeCuentas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id_Estado,Id_Credito_Disponible,Empresa")] EstadodeCuenta estadodeCuenta)
        {
            if (ModelState.IsValid)
            {
                db.EstadodeCuenta.Add(estadodeCuenta);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", estadodeCuenta.Empresa);
            return View(estadodeCuenta);
        }

        // GET: EstadodeCuentas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadodeCuenta estadodeCuenta = await db.EstadodeCuenta.FindAsync(id);
            if (estadodeCuenta == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", estadodeCuenta.Empresa);
            return View(estadodeCuenta);
        }

        // POST: EstadodeCuentas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id_Estado,Id_Credito_Disponible,Empresa")] EstadodeCuenta estadodeCuenta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadodeCuenta).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", estadodeCuenta.Empresa);
            return View(estadodeCuenta);
        }

        // GET: EstadodeCuentas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadodeCuenta estadodeCuenta = await db.EstadodeCuenta.FindAsync(id);
            if (estadodeCuenta == null)
            {
                return HttpNotFound();
            }
            return View(estadodeCuenta);
        }

        // POST: EstadodeCuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EstadodeCuenta estadodeCuenta = await db.EstadodeCuenta.FindAsync(id);
            db.EstadodeCuenta.Remove(estadodeCuenta);
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
