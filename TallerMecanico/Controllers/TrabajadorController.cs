using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TallerMecanico.Models;

namespace TallerMecanico.Controllers
{
    public class TrabajadorController : Controller
    {
        private Taller_Mecanico2Entities db = new Taller_Mecanico2Entities();

        // GET: Cliente
        public ActionResult Index()
        {
            int idEmpleado = int.Parse(Session["id"].ToString());
            var carros = db.Carros.Where(x=>x.Id_Empleado == idEmpleado);
            return View(carros.ToList());
        }   

        public ActionResult Evidencias(int? id)
        {
            int idUsr = int.Parse(Session["id"].ToString());
            var evidencias = db.Evidencias.Where(x => id == x.Id_Carro && x.Carros.Id_Empleado == idUsr);
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
            int idEmpleado = int.Parse(Session["id"].ToString());
            ViewBag.Id_Carro = new SelectList(db.Carros.Where(x=>x.Id_Empleado== idEmpleado), "Id", "Marca"); ;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EvidenciasCE evidencias)
        {
            if (ModelState.IsValid)
            {
                evidencias.Fecha = DateTime.Now;

                Evidencias temp = new Evidencias();
                temp.Id = evidencias.Id;
                temp.Comentarios = evidencias.Comentarios;
                temp.Id_Carro = evidencias.Id_Carro;
                temp.Fecha = evidencias.Fecha;

                using (Stream inputStream = evidencias.foto.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    temp.foto = memoryStream.ToArray();
                }

                db.Evidencias.Add(temp);
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
            ViewBag.Fecha = evidencias.Fecha;

            EvidenciasCE temp = new EvidenciasCE();
            temp.Id = evidencias.Id;
            temp.Comentarios = evidencias.Comentarios;
            temp.Fecha = evidencias.Fecha;
            temp.Id_Carro = evidencias.Id_Carro;
            //temp.foto = evidencias.foto;

            return View(temp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha,Comentarios,foto,Id_Carro")] EvidenciasCE evidencias)
        {
            if (ModelState.IsValid)
            {
                Evidencias temp = new Evidencias();
                temp.Id = evidencias.Id;
                temp.Comentarios = evidencias.Comentarios;
                temp.Id_Carro = evidencias.Id_Carro;
                temp.Fecha = evidencias.Fecha;

                using (Stream inputStream = evidencias.foto.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    temp.foto = memoryStream.ToArray();
                }

                db.Entry(temp).State = EntityState.Modified;
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

        public ActionResult Liberar(int? id)
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

        [HttpPost, ActionName("Liberar")]
        [ValidateAntiForgeryToken]
        public ActionResult LiberarConfirmado(int? id)
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
            Usuarios trab = db.Usuarios.Find(carros.Id_Empleado);
            carros.IdEstado = 4;
            trab.IdEstado = 1;
            db.Entry(carros).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public byte[] ToByteArray(HttpPostedFileBase img) {
            byte[] imgData = null;
            using (var binaryReader = new BinaryReader(img.InputStream)) {
                imgData = binaryReader.ReadBytes(img.ContentLength);
            }
                return imgData;
        }

        public FileContentResult GetImg(int Id) {
            using (var db = new Taller_Mecanico2Entities()) {
                byte[] byteArray = db.Evidencias.Find(Id).foto;
                if (byteArray != null)
                {
                    return new FileContentResult(byteArray, "image/jpg");
                }
                else {
                    return null;
                }
            }
        }
        
        public ActionResult VerImagen() {
            //var foto = db.Evidencias.Where(x => x.foto == x.foto);
            // Path.GetFileName(foto.GetImg);
            //ViewBag.Id_Carro = new SelectList(db.Evidencias, "Id", "foto"); 

            return View(db.Evidencias.ToList());        
        }
    }
}