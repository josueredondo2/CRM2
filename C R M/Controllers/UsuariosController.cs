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
    public class UsuariosController : Controller
    {
        private CRMEntities db = new CRMEntities();

        // GET: Usuarios
        public async Task<ActionResult> Index()
        {
            var usuario = db.Usuario.Include(u => u.Empresa1).Include(u => u.Rol1);
            return View(await usuario.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await db.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre");
            ViewBag.Rol = new SelectList(db.Rol, "Id_Rol", "Nombre");
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id_Usuario,Nombre,Apellido1,Apellido2,Correo,Contraseña,Fecha_Creacion,Empresa,Rol")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Fecha_Creacion = DateTime.Now;
                db.Usuario.Add(usuario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", usuario.Empresa);
            ViewBag.Rol = new SelectList(db.Rol, "Id_Rol", "Nombre", usuario.Rol);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await db.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", usuario.Empresa);
            ViewBag.Rol = new SelectList(db.Rol, "Id_Rol", "Nombre", usuario.Rol);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id_Usuario,Nombre,Apellido1,Apellido2,Correo,Contraseña,Fecha_Creacion,Empresa,Rol")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.Entry(usuario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", usuario.Empresa);
            ViewBag.Rol = new SelectList(db.Rol, "Id_Rol", "Nombre", usuario.Rol);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await db.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Usuario usuario = await db.Usuario.FindAsync(id);
            db.Usuario.Remove(usuario);
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

        public bool Logeo(string email,string contraseña)
        {
             var usuario = db.Usuario.Include(u => u.Empresa1).Include(u => u.Rol1);
            var resultado = usuario.Where(x => x.Correo == email && x.Contraseña == contraseña).FirstOrDefault();
            if (resultado != null)
            {
                return true;
            }
            return false;
        }
    }
}
