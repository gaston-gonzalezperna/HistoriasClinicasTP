using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HistoriasClinicas2.Data;
using HistoriasClinicas2.Models;

namespace HistoriasClinicas.Controllers
{
    public class NotasController : Controller
    {
        private readonly EFContext _context;

        public NotasController(EFContext context)
        {
            _context = context;
        }

        // GET: Notas
        public async Task<IActionResult> Index(int? id)
        {
            var eFContext = _context.Notas.Where(n => n.EvolucionId == id);
            var evolucion = await _context.Evoluciones.FindAsync(id);
            var idEpi = evolucion.EpisodioId;

            ViewBag.IdEvol = id;
            ViewBag.IdEpi = idEpi;
            return View(await eFContext.ToListAsync());
        }

        // GET: Notas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
                .Include(n => n.Evolucion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // GET: Notas/Create
        public IActionResult Create(int? id)
        {
            ViewBag.Id = id;
            return View();
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("Mensaje")] Nota nota)
        {
            if (ModelState.IsValid)
            {
                nota.NombreAutor = User.Identity.Name;
                nota.FechaYHora = DateTime.Now;
                nota.EvolucionId = (int)id;

                var evolucion = await _context.Evoluciones.FindAsync(id);
                nota.Evolucion = evolucion;

                if (evolucion.Notas == null)
                {
                    evolucion.Notas = new List<Nota>();
                }
                evolucion.Notas.Add(nota);

                _context.Add(nota);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id });
            }
            ViewData["EvolucionId"] = new SelectList(_context.Evoluciones, "Id", "Id", nota.EvolucionId);
            return View(nota);
        }

        // GET: Notas/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var nota = await _context.Notas.FindAsync(id);
        //    if (nota == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewBag.Id = nota.EvolucionId;
        //    return View(nota);
        //}

        // POST: Notas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Mensaje,FechaYHora,NombreAutor,EvolucionId")] Nota nota)
        //{
        //    if (id != nota.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var n = await _context.Notas.FindAsync(id);
        //            n.Mensaje = nota.Mensaje;

        //            _context.Update(n);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!NotaExists(nota.Id))
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
        //    ViewData["EvolucionId"] = new SelectList(_context.Evoluciones, "Id", "Id", nota.EvolucionId);
        //    return View(nota);
        //}

        // GET: Notas/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var nota = await _context.Notas
        //        .Include(n => n.Evolucion)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (nota == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(nota);
        //}

        //// POST: Notas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var nota = await _context.Notas.FindAsync(id);
        //    _context.Notas.Remove(nota);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool NotaExists(int id)
        {
            return _context.Notas.Any(e => e.Id == id);
        }
    }
}
