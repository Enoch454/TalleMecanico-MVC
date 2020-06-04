using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TallerMecanico.Models;

namespace TallerMecanico.Controllers
{
    public class AdministradorController : Controller
    {
        private Taller_Mecanico2Entities db = new Taller_Mecanico2Entities();

        // GET: Administrador
        public ActionResult Index()
        {
            var carros = db.Carros.Include(c => c.Usuarios).Include(c => c.Usuarios1).Include(c => c.Estado);
            return View(carros.ToList());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carros carros = db.Carros.Find(id);
            if (carros == null)
            {
                return HttpNotFound();
            }
            return View(carros);
        }

        public ActionResult Create()
        {
            ViewBag.Id_Cliente = new SelectList(db.Usuarios, "Id", "Usuario");
            ViewBag.Id_Empleado = new SelectList(db.Usuarios, "Id", "Usuario");
            ViewBag.IdEstado = new SelectList(db.Estado, "Id", "Estado1");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Marca,Modelo,Color,codigo,DescripcionDetalles,IdEstado,Id_Cliente,Id_Empleado")] Carros carros)
        {
            if (ModelState.IsValid)
            {
                db.Carros.Add(carros);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Cliente = new SelectList(db.Usuarios, "Id", "Usuario", carros.Id_Cliente);
            ViewBag.Id_Empleado = new SelectList(db.Usuarios, "Id", "Usuario", carros.Id_Empleado);
            ViewBag.IdEstado = new SelectList(db.Estado, "Id", "Estado1", carros.IdEstado);
            return View(carros);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carros carros = db.Carros.Find(id);
            if (carros == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Cliente = new SelectList(db.Usuarios.Where(x=>x.Id== carros.Id_Cliente), "Id", "Usuario", carros.Id_Cliente);
           List< Usuarios> emple = db.Usuarios.Where(x=>x.IdRol==2).ToList();
            bool empleadosM = true;
            foreach (Usuarios item in emple)
            {
                if (carros.Id_Empleado == item.Id && item.IdEstado==2)
                {
                    ViewBag.Id_Empleado = new SelectList(db.Usuarios.Where(x => x.Id == item.Id), "Id", "Usuario", carros.Id_Empleado);
                    empleadosM = false;
                    break;
                }
            }

            if (empleadosM)
            {
            ViewBag.Id_Empleado = new SelectList(db.Usuarios.Where(x => x.IdRol == 2 && x.IdEstado ==1), "Id", "Usuario", carros.Id_Empleado);

            }
            ViewBag.IdEstado = new SelectList(db.Estado, "Id", "Estado1", carros.IdEstado);
            return View(carros);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Marca,Modelo,Color,codigo,DescripcionDetalles,IdEstado,Id_Cliente,Id_Empleado")] Carros carros)
        {
            if (ModelState.IsValid)
            {
                Usuarios trab = db.Usuarios.Where(x=>x.Id==carros.Id_Empleado).ToList()[0];
                trab.IdEstado =2;
                carros.IdEstado = 2;
                db.Entry(trab).State = EntityState.Modified;
                db.Entry(carros).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Cliente = new SelectList(db.Usuarios, "Id", "Usuario", carros.Id_Cliente);
            ViewBag.Id_Empleado = new SelectList(db.Usuarios, "Id", "Usuario", carros.Id_Empleado);
            ViewBag.IdEstado = new SelectList(db.Estado, "Id", "Estado1", carros.IdEstado);
            return View(carros);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carros carros = db.Carros.Find(id);
            if (carros == null)
            {
                return HttpNotFound();
            }
            return View(carros);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carros carros = db.Carros.Find(id);
            db.Carros.Remove(carros);
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