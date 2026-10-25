namespace Desafio_03.Models;

public class Descubrimiento
{
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public string? Descripcion { get; set; }
    public DateTime FechaDescubrimiento { get; set; }
    public string? Lugar { get; set; }

    public int CientificoId { get; set; }
    public Cientifico Cientifico { get; set; } = null!;
}