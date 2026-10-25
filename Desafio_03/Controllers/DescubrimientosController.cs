using Desafio_03.Data;
using Desafio_03.DTOs;
using Desafio_03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Desafio_03.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DescubrimientosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DescubrimientosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var lista = await _context.Descubrimientos.ToListAsync();
        return Ok(lista);
    }

    [HttpGet("/api/cientificos/{cientificoId}/descubrimientos")]
    public async Task<IActionResult> GetByCientifico(int cientificoId)
    {
        var lista = await _context.Descubrimientos
            .Where(d => d.CientificoId == cientificoId)
            .ToListAsync();
        return Ok(lista);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DescubrimientoDto dto)
    {
        var cientificoExiste = await _context.Cientificos.AnyAsync(c => c.Id == dto.CientificoId);
        if (!cientificoExiste) return BadRequest(new { message = "El científico especificado no existe." });

        var descubrimiento = new Descubrimiento
        {
            Titulo = dto.Titulo,
            Descripcion = dto.Descripcion,
            FechaDescubrimiento = dto.FechaDescubrimiento,
            Lugar = dto.Lugar,
            CientificoId = dto.CientificoId
        };

        _context.Descubrimientos.Add(descubrimiento);
        await _context.SaveChangesAsync();
        return Ok(descubrimiento);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] DescubrimientoDto dto)
    {
        var entidad = await _context.Descubrimientos.FindAsync(id);
        if (entidad == null) return NotFound(new { message = "Descubrimiento no encontrado." });

        entidad.Titulo = dto.Titulo;
        entidad.Descripcion = dto.Descripcion;
        entidad.FechaDescubrimiento = dto.FechaDescubrimiento;
        entidad.Lugar = dto.Lugar;

        await _context.SaveChangesAsync();
        return Ok(entidad);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entidad = await _context.Descubrimientos.FindAsync(id);
        if (entidad == null) return NotFound(new { message = "Descubrimiento no encontrado." });

        _context.Descubrimientos.Remove(entidad);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Descubrimiento eliminado." });
    }
}