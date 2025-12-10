using Microsoft.EntityFrameworkCore;
using GestionMetas.Models;

namespace GestionMetas.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor: recibe las opciones de configuración (cadena de conexión, proveedor, etc.)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Estas propiedades representan las tablas de la base de datos
        public DbSet<Meta> Metas { get; set; }
        public DbSet<Tarea> Tareas { get; set; }

        // Aquí podemos configurar detalles adicionales del modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Llamamos a la versión base para que EF configure lo que ya sabe
            base.OnModelCreating(modelBuilder);

            // Configurar relación uno-a-muchos entre Meta y Tarea
            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.Meta)          // Una tarea tiene UNA meta
                .WithMany(m => m.Tareas)      // Una meta tiene MUCHAS tareas
                .HasForeignKey(t => t.MetaId) // Clave foránea en Tarea
                .OnDelete(DeleteBehavior.Cascade); // Si borro la meta, se borran sus tareas
        }
    }
}
