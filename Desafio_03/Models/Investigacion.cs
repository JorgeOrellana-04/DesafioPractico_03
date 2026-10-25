namespace Desafio_03.Models;

public class Investigacion
{
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public string AreaEstudio { get; set; } = null!;
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public string Estado { get; set; } = null!;

    public int CientificoId { get; set; }
    public Cientifico Cientifico { get; set; } = null!;
}