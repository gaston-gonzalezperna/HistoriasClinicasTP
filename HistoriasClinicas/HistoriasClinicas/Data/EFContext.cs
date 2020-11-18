using HistoriasClinicas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas.Data
{
    public class EFContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int> 
    {
        public EFContext (DbContextOptions<EFContext> options) : base(options)
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
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }

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

            modelBuilder.Entity<Paciente>()
            .HasOne(a => a.HistoriaClinica)
            .WithOne(a => a.Paciente)
            .HasForeignKey<HistoriaClinica>(c => c.IdPaciente);

            modelBuilder.Entity<Episodio>()
            .HasOne(a => a.Epicrisis)
            .WithOne(a => a.Episodio)
            .HasForeignKey<Epicrisis>(c => c.IdEpisodio);

            modelBuilder.Entity<Epicrisis>()
            .HasOne(a => a.Diagnostico)
            .WithOne(a => a.Epicrisis)
            .HasForeignKey<Diagnostico>(c => c.IdEpicrisis);

            modelBuilder.Entity<IdentityUser<int>>()
            .HasKey(l => new { l.Id });

        }
    }
}
