using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCalificaciones.Core.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Porcentaje { get; set; } 
        public bool Estatus { get; set; } = true;
    }
}
