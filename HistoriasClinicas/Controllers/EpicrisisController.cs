﻿using System;
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
    public class EpicrisisController : Controller
    {
        private readonly EFContext _context;

        public EpicrisisController(EFContext context)
        {
            _context = context;
        }


        //public async Task<IActionResult> Index()
        //{
        //    var eFContext = _context.Epicrisis.Include(e => e.Episodio);
        //    return View(await eFContext.ToListAsync());
        //}

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epicrisis = await _context.Epicrisis
                .Include(e => e.Episodio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (epicrisis == null)
            {
                return NotFound();
            }

            return View(epicrisis);
        }

        [Authorize(Roles = "Empleado, Medico")]
        public IActionResult Create(int? idEpi)
        {
            ViewBag.EpisodioId = idEpi;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreMedico,EpisodioId")] Epicrisis epicrisis)
        {

            if (ModelState.IsValid)
            {
                epicrisis.FechaYHora = DateTime.Now;

                var Episodio = await _context.Episodios
                .FirstOrDefaultAsync(m => m.Id == epicrisis.EpisodioId);
                Episodio.EstadoAbierto = false;

                epicrisis.Episodio = Episodio;

                _context.Add(epicrisis);
                _context.Update(Episodio);

                await _context.SaveChangesAsync();


                return RedirectToAction("Create", "Diagnosticos", new { @idEpi = epicrisis.Id });
            }
            ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Id", epicrisis.EpisodioId);
            return View(epicrisis);
        }

        // GET: Epicrisis/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var epicrisis = await _context.Epicrisis.FindAsync(id);
        //    if (epicrisis == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Id", epicrisis.EpisodioId);
        //    return View(epicrisis);
        //}

        //// POST: Epicrisis/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,NombreMedico,FechaYHora,EpisodioId")] Epicrisis epicrisis)
        //{
        //    if (id != epicrisis.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(epicrisis);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!EpicrisisExists(epicrisis.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Id", epicrisis.EpisodioId);
        //    return View(epicrisis);
        //}

        // GET: Epicrisis/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var epicrisis = await _context.Epicrisis
        //        .Include(e => e.Episodio)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (epicrisis == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(epicrisis);
        //}

        //// POST: Epicrisis/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var epicrisis = await _context.Epicrisis.FindAsync(id);
        //    _context.Epicrisis.Remove(epicrisis);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool EpicrisisExists(int id)
        {
            return _context.Epicrisis.Any(e => e.Id == id);
        }
    }
}
