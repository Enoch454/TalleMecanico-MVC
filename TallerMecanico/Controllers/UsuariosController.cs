using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TallerMecanico.Models;

namespace TallerMecanico.Controllers
{
    public class UsuariosController : Controller
    {

        private Taller_Mecanico2Entities db = new Taller_Mecanico2Entities();
        // GET: Usuarios
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Usuarios u)
        {
            Usuarios usuario = db.Usuarios.FirstOrDefault
                (x => x.Usuario == u.Usuario && x.Pwd == u.Pwd);
            if (usuario == null)
            {
                ViewBag.Error = "Usuario y/o password incorrectos";
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(usuario.Usuario, false);
                Session["nombre"] = usuario.Usuario;
                Session["id"] = usuario.Id;
                string rol = usuario.Roles.Rol;
                Session["rol"] = rol;
                if (rol == "Administrador")
                {
                    return RedirectToAction("Index", "Administrador");

                }
                else if (rol == "Trabajador")
                {
                    return RedirectToAction("Index", "Trabajador");
                }
                else if (rol == "Cliente")
                {
                    return RedirectToAction("Index", "Cliente");
                }
                return RedirectToAction("Login", "Usuarios");
            }
        }

        public ActionResult RegistrarUsuario()
        {
            ViewBag.IdRol = new SelectList(db.Roles, "Id", "Rol");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarUsuario([Bind(Include = "Id,Usuario,Pwd,IdRol")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                usuarios.IdRol =3;
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            ViewBag.IdRol = new SelectList(db.Roles, "Id", "Rol", usuarios.IdRol);
            return View(usuarios);
        }

        public ActionResult LogOut(Usuarios usuario)
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login", "Usuarios");
        }
    }
}