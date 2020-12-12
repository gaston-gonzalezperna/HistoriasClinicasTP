using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HistoriasClinicas.ViewModels;
using HistoriasClinicas2.Data;
using HistoriasClinicas2.Models;
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

        [HttpGet]
        public IActionResult RegistrarPaciente()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarPaciente(PacienteDto model)
        {

            if (ModelState.IsValid)
            {
                Usuario usuario = new Usuario();
                usuario.NormalizedEmail = model.Email.ToUpper();
                usuario.Email = model.Email;
                usuario.UserName = model.Email;
                usuario.NormalizedUserName = model.Email.ToUpper();

                var resultadoDeCreacion = await _usrmgr.CreateAsync(usuario, model.Password);

                if (resultadoDeCreacion.Succeeded)
                {
                    Paciente paciente = new Paciente();
                    paciente.ObraSocial = model.ObraSocial;
                    paciente.Nombre = model.Nombre;
                    paciente.Apellido = model.Apellido;
                    paciente.DNI = model.DNI;
                    paciente.FechaAlta = DateTime.Now;
                    paciente.Direccion = model.Direccion;
                    paciente.Telefono = model.Telefono;
                    paciente.Email = model.Email;
                    paciente.UsuarioId = usuario.Id;
                    paciente.Usuario = usuario;
                    _contexto.Pacientes.Add(paciente);

                    var nuevaHistoriaClinica = new HistoriaClinica();
                    nuevaHistoriaClinica.PacienteId = paciente.Id;
                    nuevaHistoriaClinica.Paciente = paciente;
                    _contexto.HistoriaClinicas.Add(nuevaHistoriaClinica);
                    paciente.HistoriaClinica = nuevaHistoriaClinica;

                    _contexto.SaveChanges();


                    await _usrmgr.AddToRoleAsync(usuario, "Paciente");

                    if (!_signinmgr.IsSignedIn(User))
                    {
                        await _signinmgr.SignInAsync(usuario, isPersistent: false);
                    }

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
        public IActionResult RegistrarMedico()
        {
            return View();
        }

        //Solo accesible por Administrador y Empleado
        [HttpPost]
        public async Task<IActionResult> RegistrarMedico(MedicoDto model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = new Usuario();
                usuario.NormalizedEmail = model.Email.ToUpper();
                usuario.Email = model.Email;
                usuario.UserName = model.Email;
                usuario.NormalizedUserName = model.Email.ToUpper();

                var resultadoDeCreacion = await _usrmgr.CreateAsync(usuario, model.Password);

                if (resultadoDeCreacion.Succeeded)
                {
                    Medico medico = new Medico();
                    medico.Matricula = model.Matricula;
                    medico.FechaAlta = DateTime.Now;
                    medico.Nombre = model.Nombre;
                    medico.Apellido = model.Apellido;
                    medico.Especialidad = model.Especialidad;
                    medico.DNI = model.DNI;
                    medico.Direccion = model.Direccion;
                    medico.Telefono = model.Telefono;
                    medico.Email = model.Email;
                    medico.UsuarioId = usuario.Id;
                    medico.Usuario = usuario;
                    _contexto.Medicos.Add(medico);
                    _contexto.SaveChanges();

                    await _usrmgr.AddToRoleAsync(usuario, "Medico");

                    if (!_signinmgr.IsSignedIn(User))
                    {
                        await _signinmgr.SignInAsync(usuario, isPersistent: false);
                    }

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
        public IActionResult RegistrarEmpleado()
        {
            return View();
        }

        //Solo accesible por el administrador
        [HttpPost]
        public async Task<IActionResult> RegistrarEmpleado(EmpleadoDto model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = new Usuario();
                usuario.NormalizedEmail = model.Email.ToUpper();
                usuario.Email = model.Email;
                usuario.UserName = model.Email;
                usuario.NormalizedUserName = model.Email.ToUpper();

                var resultadoDeCreacion = await _usrmgr.CreateAsync(usuario, model.Password);
 
                if (resultadoDeCreacion.Succeeded)
                {
                    Empleado empleado = new Empleado();
                    empleado.FechaAlta = DateTime.Now;
                    empleado.Nombre = model.Nombre;
                    empleado.Apellido = model.Apellido;
                    empleado.Legajo = empleado.Id + model.DNI;
                    empleado.DNI = model.DNI;
                    empleado.Direccion = model.Direccion;
                    empleado.Telefono = model.Telefono;
                    empleado.Email = model.Email;
                    empleado.UsuarioId = usuario.Id;
                    empleado.Usuario = usuario;
                    _contexto.Empleados.Add(empleado);
                    _contexto.SaveChanges();

                    await _usrmgr.AddToRoleAsync(usuario, "Empleado");

                    if (!_signinmgr.IsSignedIn(User))
                    {
                        await _signinmgr.SignInAsync(usuario, isPersistent: false);
                    }

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

        public async Task<IActionResult> ActualizarEmail(string nuevoEmail, string idUsuario)
        {
            var usuarioEncontrado = await _usrmgr.FindByIdAsync(idUsuario);

            if (usuarioEncontrado != null)
            {
                usuarioEncontrado.UserName = nuevoEmail;
                usuarioEncontrado.NormalizedUserName = nuevoEmail.ToUpper();
                usuarioEncontrado.Email = nuevoEmail;
                usuarioEncontrado.NormalizedEmail = nuevoEmail.ToUpper();

                var resultadoDeUpdate = await _usrmgr.UpdateAsync(usuarioEncontrado);

                if (resultadoDeUpdate.Succeeded)
                {
                    await _signinmgr.SignInAsync(usuarioEncontrado, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }                    

            }
            
            return Json($"Usuario no encontrado");             
        }

        public async Task<IActionResult> BorrarUsuario(string email)
        {
            var usuarioEncontrado = await _usrmgr.FindByEmailAsync(email);

            if (usuarioEncontrado != null)
            {
                var resultadoDelete = await _usrmgr.DeleteAsync(usuarioEncontrado);

                if (resultadoDelete.Succeeded)
                {
                    return RedirectToAction("Index", "Pacientes");
                }
                else
                {
                    return Json($"Usuario no eliminado");
                }
            }

            return Json($"Usuario no eliminado");
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