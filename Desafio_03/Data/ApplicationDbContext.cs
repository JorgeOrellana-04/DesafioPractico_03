using Desafio_03.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Desafio_03.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Cientifico> Cientificos { get; set; }
    public DbSet<Especialidad> Especialidades { get; set; }
    public DbSet<CientificoEspecialidad> CientificoEspecialidades { get; set; }
    public DbSet<Descubrimiento> Descubrimientos { get; set; }
    public DbSet<Investigacion> Investigaciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relación Muchos a Muchos entre Cientifico y Especialidad
        modelBuilder.Entity<CientificoEspecialidad>()
            .HasKey(ce => new { ce.CientificoId, ce.EspecialidadId });

        modelBuilder.Entity<CientificoEspecialidad>()
            .HasOne(ce => ce.Cientifico)
            .WithMany(c => c.CientificoEspecialidades)
            .HasForeignKey(ce => ce.CientificoId);

        modelBuilder.Entity<CientificoEspecialidad>()
            .HasOne(ce => ce.Especialidad)
            .WithMany(e => e.CientificoEspecialidades)
            .HasForeignKey(ce => ce.EspecialidadId);

        // Validación CHECK para el Estado de Investigación
        modelBuilder.Entity<Investigacion>()
            .ToTable(t => t.HasCheckConstraint("CK_Investigacion_Estado", "Estado IN ('En curso', 'Finalizada', 'Publicada')"));
    }
}