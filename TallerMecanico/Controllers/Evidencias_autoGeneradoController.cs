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
    public class Evidencias_autoGeneradoController : Controller
    {
        private Taller_Mecanico2Entities db = new Taller_Mecanico2Entities();

        // GET: Evidencias_autoGenerado
        public ActionResult Index()
        {
            var evidencias = db.Evidencias.Include(e => e.Carros);
            return View(evidencias.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evidencias evidencias = db.Evidencias.Find(id);
            if (evidencias == null)
            {
                return HttpNotFound();
            }
            return View(evidencias);
        }

        public ActionResult Create()
        {
            ViewBag.Id_Carro = new SelectList(db.Carros, "Id", "Marca");
            return View();
        }

       [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,Comentarios,foto,Id_Carro")] Evidencias evidencias)
        {
            if (ModelState.IsValid)
            {
                db.Evidencias.Add(evidencias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Carro = new SelectList(db.Carros, "Id", "Marca", evidencias.Id_Carro);
            return View(evidencias);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evidencias evidencias = db.Evidencias.Find(id);
            if (evidencias == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Carro = new SelectList(db.Carros, "Id", "Marca", evidencias.Id_Carro);
            return View(evidencias);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha,Comentarios,foto,Id_Carro")] Evidencias evidencias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evidencias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Carro = new SelectList(db.Carros, "Id", "Marca", evidencias.Id_Carro);
            return View(evidencias);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evidencias evidencias = db.Evidencias.Find(id);
            if (evidencias == null)
            {
                return HttpNotFound();
            }
            return View(evidencias);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evidencias evidencias = db.Evidencias.Find(id);
            db.Evidencias.Remove(evidencias);
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