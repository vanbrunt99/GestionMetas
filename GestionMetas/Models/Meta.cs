using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace GestionMetas.Models
{
    public class Meta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "El título debe tener entre 5 y 100 caracteres.")]
        public string Titulo { get; set; }

        public string? Descripcion { get; set; }

        [Required]
        public Categoria Categoria { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime? FechaLimite { get; set; }

        [Required]
        public Prioridad Prioridad { get; set; }

        [Required]
        public EstadoMeta Estado { get; set; } = EstadoMeta.NoIniciada;

        // Relación uno-a-muchos: una Meta puede tener muchas Tareas
        public List<Tarea> Tareas { get; set; } = new();
    }
}
