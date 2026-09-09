using System;

namespace SistemaCalificaciones.Core.Models
{
    public class Instituto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Domicilio { get; set; } = string.Empty;
        public string CodigoPostal { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;
        public DateTime CreadoEn { get; set; }
        public bool Estatus { get; set; } = true;
    }
}