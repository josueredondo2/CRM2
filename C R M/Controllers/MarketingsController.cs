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
    public class MarketingsController : Controller
    {
        private CRMEntities db = new CRMEntities();

        // GET: Marketings
        public async Task<ActionResult> Index()
        {
            var marketing = db.Marketing.Include(m => m.Empresa1).Include(m => m.Producto).Include(m => m.Producto1);
            return View(await marketing.ToListAsync());
        }

        // GET: Marketings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marketing marketing = await db.Marketing.FindAsync(id);
            if (marketing == null)
            {
                return HttpNotFound();
            }
            return View(marketing);
        }

        // GET: Marketings/Create
        public ActionResult Create()
        {
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre");
            ViewBag.Id_Producto = new SelectList(db.Producto, "Id_Producto", "Nombre");
            ViewBag.Sugerencia_Producto = new SelectList(db.Producto, "Id_Producto", "Nombre");
            return View();
        }

        // POST: Marketings/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id_Marketing,Id_Producto,Empresa,Sugerencia_Producto,URL")] Marketing marketing)
        {
            if (ModelState.IsValid)
            {
                db.Marketing.Add(marketing);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", marketing.Empresa);
            ViewBag.Id_Producto = new SelectList(db.Producto, "Id_Producto", "Nombre", marketing.Id_Producto);
            ViewBag.Sugerencia_Producto = new SelectList(db.Producto, "Id_Producto", "Nombre", marketing.Sugerencia_Producto);
            return View(marketing);
        }

        // GET: Marketings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marketing marketing = await db.Marketing.FindAsync(id);
            if (marketing == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", marketing.Empresa);
            ViewBag.Id_Producto = new SelectList(db.Producto, "Id_Producto", "Nombre", marketing.Id_Producto);
            ViewBag.Sugerencia_Producto = new SelectList(db.Producto, "Id_Producto", "Nombre", marketing.Sugerencia_Producto);
            return View(marketing);
        }

        // POST: Marketings/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id_Marketing,Id_Producto,Empresa,Sugerencia_Producto,URL")] Marketing marketing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(marketing).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", marketing.Empresa);
            ViewBag.Id_Producto = new SelectList(db.Producto, "Id_Producto", "Nombre", marketing.Id_Producto);
            ViewBag.Sugerencia_Producto = new SelectList(db.Producto, "Id_Producto", "Nombre", marketing.Sugerencia_Producto);
            return View(marketing);
        }

        // GET: Marketings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marketing marketing = await db.Marketing.FindAsync(id);
            if (marketing == null)
            {
                return HttpNotFound();
            }
            return View(marketing);
        }

        // POST: Marketings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Marketing marketing = await db.Marketing.FindAsync(id);
            db.Marketing.Remove(marketing);
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
