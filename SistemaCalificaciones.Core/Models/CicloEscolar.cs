using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCalificaciones.Core.Models
{
    public class CicloEscolar
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Estatus { get; set; } = true;
    }
}
