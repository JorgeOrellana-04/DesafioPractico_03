namespace Desafio_03.Models;

public class Especialidad
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }

    public ICollection<CientificoEspecialidad> CientificoEspecialidades { get; set; } = new List<CientificoEspecialidad>();
}