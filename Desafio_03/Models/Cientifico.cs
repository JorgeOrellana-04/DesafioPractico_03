using System.Text.Json.Serialization;

namespace Desafio_03.Models;

public class Cientifico
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public string Nacionalidad { get; set; } = null!;
    public DateTime FechaNacimiento { get; set; }
    public string Email { get; set; } = null!;

    public ICollection<CientificoEspecialidad> CientificoEspecialidades { get; set; } = new List<CientificoEspecialidad>();
    public ICollection<Descubrimiento> Descubrimientos { get; set; } = new List<Descubrimiento>();
    public ICollection<Investigacion> Investigaciones { get; set; } = new List<Investigacion>();
}