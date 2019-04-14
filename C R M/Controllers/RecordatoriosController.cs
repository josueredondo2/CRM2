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
    public class RecordatoriosController : Controller
    {
        private CRMEntities db = new CRMEntities();

        // GET: Recordatorios
        public async Task<ActionResult> Index()
        {
            var recordatorio = db.Recordatorio.Include(r => r.Empresa1).Include(r => r.Recordar);
            return View(await recordatorio.ToListAsync());
        }

        // GET: Recordatorios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recordatorio recordatorio = await db.Recordatorio.FindAsync(id);
            if (recordatorio == null)
            {
                return HttpNotFound();
            }
            return View(recordatorio);
        }

        // GET: Recordatorios/Create
        public ActionResult Create()
        {
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre");
            ViewBag.Id_Recordar = new SelectList(db.Recordar, "Id_Recordar", "Descripción");
            return View();
        }

        // POST: Recordatorios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id_Recordatorio,Tipo,Fecha,Hora,Minutos,Abreviatura,Detalle,Empresa,Id_Recordar")] Recordatorio recordatorio)
        {
            if (ModelState.IsValid)
            {
                db.Recordatorio.Add(recordatorio);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", recordatorio.Empresa);
            ViewBag.Id_Recordar = new SelectList(db.Recordar, "Id_Recordar", "Descripción", recordatorio.Id_Recordar);
            return View(recordatorio);
        }

        // GET: Recordatorios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recordatorio recordatorio = await db.Recordatorio.FindAsync(id);
            if (recordatorio == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", recordatorio.Empresa);
            ViewBag.Id_Recordar = new SelectList(db.Recordar, "Id_Recordar", "Descripción", recordatorio.Id_Recordar);
            return View(recordatorio);
        }

        // POST: Recordatorios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id_Recordatorio,Tipo,Fecha,Hora,Minutos,Abreviatura,Detalle,Empresa,Id_Recordar")] Recordatorio recordatorio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recordatorio).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", recordatorio.Empresa);
            ViewBag.Id_Recordar = new SelectList(db.Recordar, "Id_Recordar", "Descripción", recordatorio.Id_Recordar);
            return View(recordatorio);
        }

        // GET: Recordatorios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recordatorio recordatorio = await db.Recordatorio.FindAsync(id);
            if (recordatorio == null)
            {
                return HttpNotFound();
            }
            return View(recordatorio);
        }

        // POST: Recordatorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Recordatorio recordatorio = await db.Recordatorio.FindAsync(id);
            db.Recordatorio.Remove(recordatorio);
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
