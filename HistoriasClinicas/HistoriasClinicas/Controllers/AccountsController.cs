﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HistoriasClinicas.Data;
using HistoriasClinicas.Models;
using HistoriasClinicas.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HistoriasClinicas.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<Usuario> _usrmgr;
        private readonly SignInManager<Usuario> _signinmgr;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly EFContext _contexto;

        public AccountsController(
            UserManager<Usuario> usrmgr,
            SignInManager<Usuario> signinmgr,
            IHostingEnvironment hosting,
            EFContext contexto
            )
        {
            this._usrmgr = usrmgr;
            this._signinmgr = signinmgr;
            this._hostingEnvironment = hosting;
            this._contexto = contexto;
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarPaciente(PacienteDto model)
        {
            if (ModelState.IsValid)
            {
                Paciente paciente = new Paciente();
                paciente.ObraSocial = model.ObraSocial;
                paciente.Nombre = model.Nombre;
                paciente.Apellido = model.Apellido;
                paciente.DNI = model.DNI;
                paciente.Direccion = model.Direccion;
                paciente.PhoneNumber = model.Telefono;
                paciente.UserName = model.Email;
                paciente.NormalizedUserName = model.Email.ToUpper();
                paciente.Email = model.Email;
                paciente.NormalizedEmail = model.Email.ToUpper();

                var resultadoDeCreacion = await _usrmgr.CreateAsync(paciente, model.Password);

                if (resultadoDeCreacion.Succeeded)
                {
                    //ok la creación pulgares arriba
                    //Le agrego el rol

                    await _usrmgr.AddToRoleAsync(paciente, "Paciente");

                    await _signinmgr.SignInAsync(paciente, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                //tratamiento para los errores
                foreach (var error in resultadoDeCreacion.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }


            }

            return View(model);
        }
        
        //Solo accesible por Administrador y Empleado
        [HttpPost]
        public async Task<IActionResult> RegistrarMedico(MedicoDto model)
        {
            if (ModelState.IsValid)
            {
                Medico medico = new Medico();
                medico.Nombre = model.Nombre;
                medico.Apellido = model.Apellido;
                medico.Especialidad = model.Especialidad;
                medico.DNI = model.DNI;
                medico.Direccion = model.Direccion;
                medico.PhoneNumber = model.Telefono;
                medico.UserName = model.Email;
                medico.NormalizedUserName = model.Email.ToUpper();
                medico.Email = model.Email;
                medico.NormalizedEmail = model.Email.ToUpper();

                var resultadoDeCreacion = await _usrmgr.CreateAsync(medico, model.Password);

                if (resultadoDeCreacion.Succeeded)
                {
                    //ok la creación pulgares arriba
                    //Le agrego el rol

                    await _usrmgr.AddToRoleAsync(medico, "Medico");

                    await _signinmgr.SignInAsync(medico, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                //tratamiento para los errores
                foreach (var error in resultadoDeCreacion.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }


            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarAdmin()
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = new Usuario();
                usuario.Nombre = "Administrador";
                usuario.Apellido = "Administrador";
                usuario.UserName = "admin@madicus.com";
                usuario.NormalizedUserName = usuario.UserName.ToUpper();
                usuario.Email = usuario.UserName;
                usuario.NormalizedEmail = usuario.Email.ToUpper();

                var resultadoDeCreacion = await _usrmgr.CreateAsync(usuario, "administrador");

                if (resultadoDeCreacion.Succeeded)
                {
                    //ok la creación pulgares arriba
                    //Le agrego el rol

                    await _usrmgr.AddToRoleAsync(usuario, "Administrador");

                    await _signinmgr.SignInAsync(usuario, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                //tratamiento para los errores
                foreach (var error in resultadoDeCreacion.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }


            }
            return View();
        }
        //Solo accesible por el administrador
        [HttpPost]
        public async Task<IActionResult> RegistrarEmpleado(EmpleadoDto model)
        {
            if (ModelState.IsValid)
            {
                Empleado empleado = new Empleado();
                empleado.Nombre = model.Nombre;
                empleado.Apellido = model.Apellido;
                empleado.Legajo = empleado.Id + model.DNI;
                empleado.DNI = model.DNI;
                empleado.Direccion = model.Direccion;
                empleado.PhoneNumber = model.Telefono;
                empleado.UserName = model.Email;
                empleado.NormalizedUserName = model.Email.ToUpper();
                empleado.Email = model.Email;
                empleado.NormalizedEmail = model.Email.ToUpper();

                var resultadoDeCreacion = await _usrmgr.CreateAsync(empleado, model.Password);

                if (resultadoDeCreacion.Succeeded)
                {
                    //ok la creación pulgares arriba
                    //Le agrego el rol

                    await _usrmgr.AddToRoleAsync(empleado, "Empleado");

                    await _signinmgr.SignInAsync(empleado, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                //tratamiento para los errores
                foreach (var error in resultadoDeCreacion.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }


            }

            return View(model);
        }

        [HttpGet]
        public IActionResult IniciarSesion(string ReturnUrl)
        {
            TempData["returnUrl"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(LoginDto model)
        {
            string returnUrl = TempData["returnUrl"] as string;


            if (ModelState.IsValid)
            {
                var resultado = await _signinmgr.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (resultado.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        //Voy al returnurl
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Inicio de sesión inválido.");

            }

            return View(model);
        }


        public async Task<IActionResult> CerrarSesion()
        {
            await _signinmgr.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> EmailDisponible(string email)
        {
            var usuarioEncontrado = await _usrmgr.FindByEmailAsync(email);

            if (usuarioEncontrado != null)
            {
                //El mail ya está en uso
                return Json($"El correo {email} ya está en uso.");
            }
            else
            {
                //No hay un usuario existente con ese email
                return Json(true);
            }
        }
    }
}