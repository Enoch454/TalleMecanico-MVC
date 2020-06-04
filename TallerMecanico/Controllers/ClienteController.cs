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
    public class ClienteController : Controller
    {
        private Taller_Mecanico2Entities db = new Taller_Mecanico2Entities();

        // GET: Cliente
        public ActionResult Index()
        {
            int idCliente = int.Parse(Session["id"].ToString());
            var carros = db.Carros.Where(x=>x.Id_Cliente== idCliente);
            ViewBag.empleado = new SelectList(db.Carros.ToList(), "Id", "Id_Empleado");
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

        public ActionResult RegistrarCarro()
        {
            ViewBag.Id_Cliente = new SelectList(db.Usuarios, "Id", "Usuario");
            ViewBag.Id_Empleado = new SelectList(db.Usuarios, "Id", "Usuario");
            ViewBag.IdEstado = new SelectList(db.Estado, "Id", "Estado1");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarCarro([Bind(Include = "Id,Marca,Modelo,Color,codigo,DescripcionDetalles,IdEstado,Id_Cliente,Id_Empleado")] Carros carros)
        {
            if (ModelState.IsValid)
            {
                carros.Id_Cliente = int.Parse(Session["id"].ToString());
                carros.IdEstado = 1;
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
            ViewBag.Id_Cliente = new SelectList(db.Usuarios, "Id", "Usuario", carros.Id_Cliente);
            ViewBag.Id_Empleado = new SelectList(db.Usuarios, "Id", "Usuario", carros.Id_Empleado);
            ViewBag.IdEstado = new SelectList(db.Estado, "Id", "Estado1", carros.IdEstado);
            return View(carros);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Marca,Modelo,Color,codigo,DescripcionDetalles,IdEstado,Id_Cliente,Id_Empleado")] Carros carros)
        {
            if (ModelState.IsValid)
            {
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
        public ActionResult Evidencias(int? id)
        {
            int idUsr = int.Parse(Session["id"].ToString());
            var evidencias = db.Evidencias.Where(x => id == x.Id_Carro && x.Carros.Id_Cliente == idUsr);
            return View(evidencias.ToList());
        }
    }
}