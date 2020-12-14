using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HistoriasClinicas2.Data;
using HistoriasClinicas2.Models;
using Microsoft.AspNetCore.Authorization;

namespace HistoriasClinicas2.Controllers
{
    public class EvolucionesController : Controller
    {
        private readonly EFContext _context;

        public EvolucionesController(EFContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index(int? id)
        {
            var eFContext = _context.Evoluciones.Where(e => e.EpisodioId == id); //trae las evoluciones de ese episodio
            var episodio = _context.Episodios.Where(e => e.Id == id).FirstOrDefault(); //trae el episodio de ese id
            var idHistoria = episodio.HistoriaClinicaId; //busca la HC de ese episodio
            ViewBag.Id = idHistoria;
            ViewBag.IdEpisodio = episodio.Id;
            ViewBag.EstadoAbierto = episodio.EstadoAbierto;
            return View(await eFContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evolucion = await _context.Evoluciones
                .Include(e => e.Episodio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evolucion == null)
            {
                return NotFound();
            }

            return View(evolucion);
        }

        [Authorize(Roles = "Medico")]
        public IActionResult Create(int? id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("DescripcionAtencion")] Evolucion evolucion)
        {
            if (ModelState.IsValid)
            {
                evolucion.Medico = User.Identity.Name;
                evolucion.FechaYHora = DateTime.Now;
                evolucion.EstadoAbierto = true;
                evolucion.EpisodioId = (int)id;

                Episodio e = _context.Episodios.Where(e => e.Id == id).FirstOrDefault();
                evolucion.Episodio = e;
                evolucion.Notas = new List<Nota>();

                if (e.Evoluciones == null)
                {
                    e.Evoluciones = new List<Evolucion>();
                }
                e.Evoluciones.Add(evolucion);

                _context.Add(evolucion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id });
            }
            return View(evolucion);
        }

        // GET: Evoluciones/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var evolucion = await _context.Evoluciones.FindAsync(id);
        //    if (evolucion == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Id", evolucion.EpisodioId);
        //    return View(evolucion);
        //}

        // POST: Evoluciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id, DescripcionAtencion")] Evolucion evolucion)
        //{
        //    if (id != evolucion.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var e = await _context.Evoluciones.FindAsync(id);
        //            e.DescripcionAtencion = evolucion.DescripcionAtencion;

        //            _context.Update(e);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!EvolucionExists(evolucion.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction("Index", new { id });
        //    }
        //    ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Id", evolucion.EpisodioId);
        //    return View(evolucion);
        //}

        // GET: Evoluciones/Delete/5
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> Cerrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evolucion = await _context.Evoluciones
                .Include(e => e.Episodio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evolucion == null)
            {
                return NotFound();
            }

            return View(evolucion);
        }

        [HttpPost, ActionName("Cerrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CerrarConfirmed(int id)
        {
            var evolucion = await _context.Evoluciones.FindAsync(id);
            evolucion.EstadoAbierto = false;
            evolucion.FechaYHoraAlta = DateTime.Now;
            evolucion.FechaYHoraCierre = DateTime.Now;

            _context.Evoluciones.Update(evolucion);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id = evolucion.EpisodioId});
        }

        private bool EvolucionExists(int id)
        {
            return _context.Evoluciones.Any(e => e.Id == id);
        }
    }
}
