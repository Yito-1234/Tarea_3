using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.IO;
using TrenCr.Models;
namespace TrenCr.Controllers
{
    public class EstacionController : Controller
    {


        private readonly trencrContext _context;

        private Estacion estaciones;
        private CompraBoleto compra;

        public EstacionController(trencrContext context)
        {

            _context = context;
        }

        public IActionResult Index()
        {
            var esta = _context.Estacions.Include(d => d.IdRutaNavigation);
            return View(esta.ToList());
        }

        public IActionResult Compra()
        {
            var esta = _context.CompraBoletos.Include(d => d.IdEstacionNavigation).Include(d => d.IdRutaNavigation);

            return View(esta.ToList());
        }

     




        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estacion esta = _context.Estacions.Find(id);
            if (esta == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRuta = new SelectList(_context.Rutas, "IdRuta", "Nombre", esta.IdRutaNavigation);
            return View(esta);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("IdEstacion,IdRuta,NomEstacion,Horario,EspaciosDisponibles,CantBoletos")] Estacion esta)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(esta).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdRuta = new SelectList(_context.Rutas, "IdRuta", "Nombre", esta.IdRutaNavigation);
            return View(esta);
        }

    }

    internal class HttpStatusCodeResult : ActionResult
    {
        private HttpStatusCode badRequest;

        public HttpStatusCodeResult(HttpStatusCode badRequest)
        {
            this.badRequest = badRequest;
        }
    }
}
