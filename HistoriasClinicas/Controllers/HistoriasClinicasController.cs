﻿using System;
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
    public class EpisodiosController : Controller
    {
        private readonly EFContext _context;

        public EpisodiosController(EFContext context)
        {
            _context = context;
        }

        // GET: Episodios
        public async Task<IActionResult> Index(int? Id)
        {
            ViewBag.Id = Id;
            return View(await _context.Episodios.Where(e => e.HistoriaClinicaId == Id).ToListAsync());
        }

        // GET: Episodios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episodio = await _context.Episodios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (episodio == null)
            {
                return NotFound();
            }

            return View(episodio);
        }

        // GET: Episodios/Create
        public IActionResult Create(int? id)
        {
            ViewBag.Id = id;
            return View();
        }

        // POST: Episodios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Motivo,Descripcion")] Episodio episodio)
        {
            if (ModelState.IsValid)
            {
                var usuario = _context.Usuarios.First(usuario => usuario.NormalizedEmail == User.Identity.Name);
  //              episodio.EmpleadoRegistra = (Empleado)usuario;
                episodio.EstadoAbierto = true;
                episodio.Evoluciones = new List<Evolucion>();
                episodio.FechaYHoraInicio = DateTime.Now;
                episodio.HistoriaClinicaId = id;

                _context.Add(episodio);
                await _context.SaveChangesAsync();

                var hc = _context.HistoriaClinicas.Find(id);

                if (hc.Episodios == null)
                {
                    hc.Episodios = new List<Episodio>();
                }
                hc.Episodios.Add(episodio);

                return RedirectToAction("Details", "HistoriasClinicas", new { id = id });
            }

            return RedirectToAction("Details", "HistoriasClinicas", new { id = id });
        }

        // GET: Episodios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episodio = await _context.Episodios.FindAsync(id);
            if (episodio == null)
            {
                return NotFound();
            }
            return View(episodio);
        }

        // POST: Episodios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Motivo,Descripcion,FechaYHoraInicio,FechaYHoraAlta,FechaYHoraCierre,EstadoAbierto")] Episodio episodio)
        {
            if (id != episodio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(episodio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpisodioExists(episodio.Id))
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
            return View(episodio);
        }

        // GET: Episodios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episodio = await _context.Episodios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (episodio == null)
            {
                return NotFound();
            }

            return View(episodio);
        }

        // POST: Episodios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var episodio = await _context.Episodios.FindAsync(id);
            _context.Episodios.Remove(episodio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EpisodioExists(int id)
        {
            return _context.Episodios.Any(e => e.Id == id);
        }
    }
}