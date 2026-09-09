using System;

namespace SistemaCalificaciones.Core.Models
{
    public class Alumno
    {
        public int Id { get; set; }
        public int InstitutoId { get; set; }
        public string Matricula { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string ApPaterno { get; set; } = string.Empty;
        public string? ApMaterno { get; set; }
        public string Genero { get; set; } = string.Empty;
        public int Grado { get; set; }
        public string? Grupo { get; set; }
        public DateTime CreadoEn { get; set; }
        public bool Estatus { get; set; } = true;
    }
}