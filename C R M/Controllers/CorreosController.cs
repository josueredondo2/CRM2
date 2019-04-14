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
    public class CorreosController : Controller
    {
        private CRMEntities db = new CRMEntities();

        // GET: Correos
        public async Task<ActionResult> Index()
        {
            var correo = db.Correo.Include(c => c.Contacto1);
            return View(await correo.ToListAsync());
        }

        // GET: Correos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Correo correo = await db.Correo.FindAsync(id);
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
        public async Task<ActionResult> Create([Bind(Include = "Id_Correo,Correo1,Contacto")] Correo correo)
        {
            if (ModelState.IsValid)
            {
                db.Correo.Add(correo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Contacto = new SelectList(db.Contacto, "Id_Contacto", "Nombre", correo.Contacto);
            return View(correo);
        }

        // GET: Correos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Correo correo = await db.Correo.FindAsync(id);
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
        public async Task<ActionResult> Edit([Bind(Include = "Id_Correo,Correo1,Contacto")] Correo correo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(correo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Contacto = new SelectList(db.Contacto, "Id_Contacto", "Nombre", correo.Contacto);
            return View(correo);
        }

        // GET: Correos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Correo correo = await db.Correo.FindAsync(id);
            if (correo == null)
            {
                return HttpNotFound();
            }
            return View(correo);
        }

        // POST: Correos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Correo correo = await db.Correo.FindAsync(id);
            db.Correo.Remove(correo);
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
