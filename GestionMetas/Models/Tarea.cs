using System;
using System.ComponentModel.DataAnnotations;

namespace GestionMetas.Models
{
    public class Tarea
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "La descripción debe tener entre 5 y 200 caracteres.")]
        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime? FechaLimite { get; set; }

        [Required]
        public EstadoTarea Estado { get; set; } = EstadoTarea.Pendiente;

        [Required]
        public Dificultad Dificultad { get; set; }

        [Range(0, 1000, ErrorMessage = "El tiempo estimado debe ser un número positivo.")]
        public double TiempoEstimado { get; set; } // en horas

        // Clave foránea
        [Required]
        public int MetaId { get; set; }

        // Propiedad de navegación
        public Meta Meta { get; set; }
    }
}

