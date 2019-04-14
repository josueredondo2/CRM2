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
    public class TelefonoesController : Controller
    {
        private CRMEntities db = new CRMEntities();

        // GET: Telefonoes
        public async Task<ActionResult> Index()
        {
            var telefono = db.Telefono.Include(t => t.Contacto1);
            return View(await telefono.ToListAsync());
        }

        // GET: Telefonoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefono telefono = await db.Telefono.FindAsync(id);
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
        public async Task<ActionResult> Create([Bind(Include = "Id_Telefono,Telefono1,Contacto")] Telefono telefono)
        {
            if (ModelState.IsValid)
            {
                db.Telefono.Add(telefono);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Contacto = new SelectList(db.Contacto, "Id_Contacto", "Nombre", telefono.Contacto);
            return View(telefono);
        }

        // GET: Telefonoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefono telefono = await db.Telefono.FindAsync(id);
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
        public async Task<ActionResult> Edit([Bind(Include = "Id_Telefono,Telefono1,Contacto")] Telefono telefono)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telefono).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Contacto = new SelectList(db.Contacto, "Id_Contacto", "Nombre", telefono.Contacto);
            return View(telefono);
        }

        // GET: Telefonoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefono telefono = await db.Telefono.FindAsync(id);
            if (telefono == null)
            {
                return HttpNotFound();
            }
            return View(telefono);
        }

        // POST: Telefonoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Telefono telefono = await db.Telefono.FindAsync(id);
            db.Telefono.Remove(telefono);
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
