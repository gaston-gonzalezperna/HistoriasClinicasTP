using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HistoriasClinicas.Data;
using HistoriasClinicas.Models;

namespace HistoriasClinicas.Controllers
{
    public class EvolucionesController : Controller
    {
        private readonly EFContext _context;

        public EvolucionesController(EFContext context)
        {
            _context = context;
        }

        // GET: Evoluciones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Evoluciones.ToListAsync());
        }

        // GET: Evoluciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evolucion = await _context.Evoluciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evolucion == null)
            {
                return NotFound();
            }

            return View(evolucion);
        }

        // GET: Evoluciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Evoluciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DescripcionAtencion,FechaYHora,EstadoAbierto")] Evolucion evolucion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evolucion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(evolucion);
        }

        // GET: Evoluciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evolucion = await _context.Evoluciones.FindAsync(id);
            if (evolucion == null)
            {
                return NotFound();
            }
            return View(evolucion);
        }

        // POST: Evoluciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DescripcionAtencion,FechaYHora,EstadoAbierto")] Evolucion evolucion)
        {
            if (id != evolucion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evolucion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvolucionExists(evolucion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(evolucion);
        }

        // GET: Evoluciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evolucion = await _context.Evoluciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evolucion == null)
            {
                return NotFound();
            }

            return View(evolucion);
        }

        // POST: Evoluciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evolucion = await _context.Evoluciones.FindAsync(id);
            _context.Evoluciones.Remove(evolucion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvolucionExists(int id)
        {
            return _context.Evoluciones.Any(e => e.Id == id);
        }
    }
}
