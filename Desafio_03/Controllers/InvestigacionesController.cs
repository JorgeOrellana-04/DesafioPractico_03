using Desafio_03.Data;
using Desafio_03.DTOs;
using Desafio_03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Desafio_03.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvestigacionesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public InvestigacionesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var lista = await _context.Investigaciones.ToListAsync();
        return Ok(lista);
    }

    [HttpGet("estado/{estado}")]
    public async Task<IActionResult> GetByEstado(string estado)
    {
        var lista = await _context.Investigaciones
            .Where(i => i.Estado.ToLower() == estado.ToLower())
            .ToListAsync();
        return Ok(lista);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] InvestigacionDto dto)
    {
        var cientificoExiste = await _context.Cientificos.AnyAsync(c => c.Id == dto.CientificoId);
        if (!cientificoExiste) return BadRequest(new { message = "El científico especificado no existe." });

        var investigacion = new Investigacion
        {
            Titulo = dto.Titulo,
            AreaEstudio = dto.AreaEstudio,
            FechaInicio = dto.FechaInicio,
            FechaFin = dto.FechaFin,
            Estado = dto.Estado,
            CientificoId = dto.CientificoId
        };

        _context.Investigaciones.Add(investigacion);
        await _context.SaveChangesAsync();
        return Ok(investigacion);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] InvestigacionDto dto)
    {
        var entidad = await _context.Investigaciones.FindAsync(id);
        if (entidad == null) return NotFound(new { message = "Investigación no encontrada." });

        entidad.Titulo = dto.Titulo;
        entidad.AreaEstudio = dto.AreaEstudio;
        entidad.FechaInicio = dto.FechaInicio;
        entidad.FechaFin = dto.FechaFin;
        entidad.Estado = dto.Estado;

        await _context.SaveChangesAsync();
        return Ok(entidad);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entidad = await _context.Investigaciones.FindAsync(id);
        if (entidad == null) return NotFound(new { message = "Investigación no encontrada." });

        _context.Investigaciones.Remove(entidad);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Investigación eliminada." });
    }
}