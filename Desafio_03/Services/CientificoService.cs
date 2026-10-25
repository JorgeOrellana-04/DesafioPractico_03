using Desafio_03.Data;
using Desafio_03.DTOs;
using Desafio_03.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio_03.Services;

public interface ICientificoService
{
    Task<IEnumerable<CientificoResponseDto>> GetAllAsync();
    Task<CientificoResponseDto?> GetByIdAsync(int id);
    Task<CientificoResponseDto> CreateAsync(CientificoCreateDto dto);
    Task<bool> AddEspecialidadAsync(int cientificoId, int especialidadId);
    Task<bool> RemoveEspecialidadAsync(int cientificoId, int especialidadId);
    Task<bool> DeleteAsync(int id);
}

public class CientificoService : ICientificoService
{
    private readonly ApplicationDbContext _context;

    public CientificoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CientificoResponseDto>> GetAllAsync()
    {
        return await _context.Cientificos
            .Include(c => c.CientificoEspecialidades)
                .ThenInclude(ce => ce.Especialidad)
            .Select(c => new CientificoResponseDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Nacionalidad = c.Nacionalidad,
                FechaNacimiento = c.FechaNacimiento,
                Email = c.Email,
                Especialidades = c.CientificoEspecialidades.Select(e => e.Especialidad.Nombre).ToList()
            }).ToListAsync();
    }

    public async Task<CientificoResponseDto?> GetByIdAsync(int id)
    {
        var c = await _context.Cientificos
            .Include(c => c.CientificoEspecialidades).ThenInclude(ce => ce.Especialidad)
            .Include(c => c.Descubrimientos)
            .Include(c => c.Investigaciones)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (c == null) return null;

        return new CientificoResponseDto
        {
            Id = c.Id,
            Nombre = c.Nombre,
            Apellido = c.Apellido,
            Nacionalidad = c.Nacionalidad,
            FechaNacimiento = c.FechaNacimiento,
            Email = c.Email,
            Especialidades = c.CientificoEspecialidades.Select(e => e.Especialidad.Nombre).ToList(),
            Descubrimientos = c.Descubrimientos.Select(d => new DescubrimientoDto { Id = d.Id, Titulo = d.Titulo, Descripcion = d.Descripcion, FechaDescubrimiento = d.FechaDescubrimiento, Lugar = d.Lugar, CientificoId = d.CientificoId }).ToList(),
            Investigaciones = c.Investigaciones.Select(i => new InvestigacionDto { Id = i.Id, Titulo = i.Titulo, AreaEstudio = i.AreaEstudio, FechaInicio = i.FechaInicio, FechaFin = i.FechaFin, Estado = i.Estado, CientificoId = i.CientificoId }).ToList()
        };
    }

    public async Task<CientificoResponseDto> CreateAsync(CientificoCreateDto dto)
    {
        var cientifico = new Cientifico
        {
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Nacionalidad = dto.Nacionalidad,
            FechaNacimiento = dto.FechaNacimiento,
            Email = dto.Email
        };

        _context.Cientificos.Add(cientifico);
        await _context.SaveChangesAsync();

        foreach (var espId in dto.EspecialidadIds)
        {
            _context.CientificoEspecialidades.Add(new CientificoEspecialidad { CientificoId = cientifico.Id, EspecialidadId = espId });
        }
        await _context.SaveChangesAsync();

        return (await GetByIdAsync(cientifico.Id))!;
    }

    public async Task<bool> AddEspecialidadAsync(int cientificoId, int especialidadId)
    {
        var exists = await _context.CientificoEspecialidades.AnyAsync(x => x.CientificoId == cientificoId && x.EspecialidadId == especialidadId);
        if (exists) return false;

        _context.CientificoEspecialidades.Add(new CientificoEspecialidad { CientificoId = cientificoId, EspecialidadId = especialidadId });
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveEspecialidadAsync(int cientificoId, int especialidadId)
    {
        var entity = await _context.CientificoEspecialidades.FirstOrDefaultAsync(x => x.CientificoId == cientificoId && x.EspecialidadId == especialidadId);
        if (entity == null) return false;

        _context.CientificoEspecialidades.Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var cientifico = await _context.Cientificos.FindAsync(id);
        if (cientifico == null) return false;

        _context.Cientificos.Remove(cientifico);
        return await _context.SaveChangesAsync() > 0;
    }
}