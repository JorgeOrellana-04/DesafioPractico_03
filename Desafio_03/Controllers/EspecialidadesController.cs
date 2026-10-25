using Desafio_03.Data;
using Desafio_03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Desafio_03.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EspecialidadesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EspecialidadesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var especialidades = await _context.Especialidades.ToListAsync();
        return Ok(especialidades);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Especialidad especialidad)
    {
        _context.Especialidades.Add(especialidad);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { id = especialidad.Id }, especialidad);
    }
}