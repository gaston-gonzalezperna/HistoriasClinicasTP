using HistoriasClinicas2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas2.Data
{
public class EFContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
        }

        public DbSet<Diagnostico> Diagnosticos { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Epicrisis> Epicrisis { get; set; }
        public DbSet<Episodio> Episodios { get; set; }
        public DbSet<Evolucion> Evoluciones { get; set; }
        public DbSet<HistoriaClinica> HistoriaClinicas { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }

  //      public DbSet<Usuario> Usuarios { get; set; }

    //    public DbSet<Rol> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole<int>>(builder =>
            {
                builder.ToTable("Roles");
            });

            modelBuilder.Entity<IdentityUser<int>>(builder =>
            {
                builder.ToTable("Usuarios");
            });

        }
    }
}
