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
    public class CarrosPruebaController : Controller
    {
        private Taller_Mecanico2Entities db = new Taller_Mecanico2Entities();

        // GET: CarrosPrueba
        public ActionResult Index()
        {
            var carros = db.Carros.Include(c => c.Usuarios).Include(c => c.Usuarios1).Include(c => c.Estado);
            return View(carros.ToList());
        }

        // GET: CarrosPrueba/Details/5
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

        // GET: CarrosPrueba/Create
        public ActionResult Create()
        {
            ViewBag.Id_Cliente = new SelectList(db.Usuarios, "Id", "Usuario");
            ViewBag.Id_Empleado = new SelectList(db.Usuarios, "Id", "Usuario");
            ViewBag.IdEstado = new SelectList(db.Estado, "Id", "Estado1");
            return View();
        }

        // POST: CarrosPrueba/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: CarrosPrueba/Edit/5
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

        // POST: CarrosPrueba/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: CarrosPrueba/Delete/5
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

        // POST: CarrosPrueba/Delete/5
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
