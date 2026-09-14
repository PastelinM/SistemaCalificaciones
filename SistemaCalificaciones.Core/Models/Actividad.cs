namespace SistemaCalificaciones.Core.Models;

public class Actividad
{
    public int Id { get; set; }
    public int CategoriaId { get; set; }
    public int PeriodoId { get; set; }
    public int MateriaId { get; set; }
    public int CicloEscolarId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal PuntajeMaximo { get; set; }
    public DateTime FechaEntrega { get; set; }
    public bool Estatus { get; set; } = true;
}