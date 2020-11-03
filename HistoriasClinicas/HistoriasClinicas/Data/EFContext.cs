using HistoriasClinicas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas.Data
{
    public class EFContext : DbContext
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
        public DbSet<Persona> Personas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Diagnostico>()
            .HasOne(a => a.Epicrisis)
            .WithOne(a => a.Diagnostico);
            //.HasForeignKey<CapitalCity>(c => c.CountryID);
            modelBuilder.Entity<Diagnostico>()
            .HasOne(a => a.Epicrisis)
            .WithOne(a => a.Diagnostico);
        }

    }
}
