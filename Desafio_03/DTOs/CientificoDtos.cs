using System.ComponentModel.DataAnnotations;

namespace Desafio_03.DTOs;

#region Validaciones Personalizadas
public class MayorDeVeinteAñosAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime fechaNacimiento)
        {
            var hoy = DateTime.Today;
            var edad = hoy.Year - fechaNacimiento.Year;

            if (fechaNacimiento.Date > hoy.AddYears(-edad)) edad--;

            if (fechaNacimiento.Date > hoy)
            {
                return new ValidationResult("La fecha de nacimiento no puede ser una fecha futura.");
            }

            if (edad < 20)
            {
                return new ValidationResult("El científico debe tener al menos 20 años de edad.");
            }
        }

        return ValidationResult.Success;
    }
}

public class NoFechaFuturaAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime fecha)
        {
            if (fecha.Date > DateTime.Today)
            {
                return new ValidationResult("La fecha no puede ser una fecha futura.");
            }
        }

        return ValidationResult.Success;
    }
}
#endregion

#region DTOs de Científico
public class CientificoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public string Nacionalidad { get; set; } = null!;
    public DateTime FechaNacimiento { get; set; }
    public string Email { get; set; } = null!;
    public List<string> Especialidades { get; set; } = new();
    public List<DescubrimientoDto> Descubrimientos { get; set; } = new();
    public List<InvestigacionDto> Investigaciones { get; set; } = new();
}

public class CientificoResponseDto : CientificoDto { }

public class CrearCientificoDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    public string Apellido { get; set; } = null!;

    [Required(ErrorMessage = "La nacionalidad es obligatoria.")]
    public string Nacionalidad { get; set; } = null!;

    [MayorDeVeinteAños]
    public DateTime FechaNacimiento { get; set; }

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "Formato de correo no válido.")]
    public string Email { get; set; } = null!;

    public List<int>? EspecialidadIds { get; set; }
}

public class CientificoCreateDto : CrearCientificoDto { }

public class ActualizarCientificoDto : CrearCientificoDto { }
#endregion

#region DTOs de Descubrimiento
public class DescubrimientoDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public string? Descripcion { get; set; }
    public DateTime FechaDescubrimiento { get; set; }
    public string? Lugar { get; set; }
    public int CientificoId { get; set; }
}

public class CrearDescubrimientoDto
{
    [Required(ErrorMessage = "El título es obligatorio.")]
    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    [NoFechaFutura]
    public DateTime FechaDescubrimiento { get; set; }

    public string? Lugar { get; set; }

    [Required(ErrorMessage = "El ID del científico es obligatorio.")]
    public int CientificoId { get; set; }
}

public class ActualizarDescubrimientoDto : CrearDescubrimientoDto { }
#endregion

#region DTOs de Investigación
public class InvestigacionDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public string AreaEstudio { get; set; } = null!;
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public string Estado { get; set; } = null!;
    public int CientificoId { get; set; }
}

public class CrearInvestigacionDto
{
    [Required(ErrorMessage = "El título es obligatorio.")]
    public string Titulo { get; set; } = null!;

    [Required(ErrorMessage = "El área de estudio es obligatoria.")]
    public string AreaEstudio { get; set; } = null!;

    [NoFechaFutura]
    public DateTime FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio.")]
    public string Estado { get; set; } = null!;

    [Required(ErrorMessage = "El ID del científico es obligatorio.")]
    public int CientificoId { get; set; }
}

public class ActualizarInvestigacionDto : CrearInvestigacionDto { }
#endregion