using System.ComponentModel.DataAnnotations;

namespace GestionMetas.Models
{
    public enum Categoria
    {
        [Display(Name = "Desarrollo personal")]
        DesarrolloPersonal,

        [Display(Name = "Carrera")]
        Carrera,

        [Display(Name = "Salud")]
        Salud,

        [Display(Name = "Finanzas")]
        Finanzas,

        [Display(Name = "Relaciones")]
        Relaciones,

        [Display(Name = "Otros")]
        Otros
    }

    public enum Prioridad
    {
        [Display(Name = "Alta")]
        Alta,

        [Display(Name = "Media")]
        Media,

        [Display(Name = "Baja")]
        Baja
    }

    public enum EstadoMeta
    {
        [Display(Name = "No iniciada")]
        NoIniciada,

        [Display(Name = "En progreso")]
        EnProgreso,

        [Display(Name = "Completada")]
        Completada,

        [Display(Name = "Abandonada")]
        Abandonada
    }

    public enum EstadoTarea
    {
        [Display(Name = "Pendiente")]
        Pendiente,

        [Display(Name = "En progreso")]
        EnProgreso,

        [Display(Name = "Completada")]
        Completada
    }

    public enum Dificultad
    {
        [Display(Name = "Fácil")]
        Facil,

        [Display(Name = "Media")]
        Media,

        [Display(Name = "Difícil")]
        Dificil
    }
}
