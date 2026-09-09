using System;

namespace SistemaCalificaciones.Core.Models
{
    public class Periodo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Estatus { get; set; } = true;
    }
}