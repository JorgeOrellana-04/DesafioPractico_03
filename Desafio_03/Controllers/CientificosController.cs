using Desafio_03.DTOs;
using Desafio_03.Services;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_03.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CientificosController : ControllerBase
{
    private readonly ICientificoService _service;

    public CientificosController(ICientificoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var cientifico = await _service.GetByIdAsync(id);
        return cientifico == null ? NotFound(new { message = "Científico no encontrado" }) : Ok(cientifico);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CientificoCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPost("{id}/especialidades/{especialidadId}")]
    public async Task<IActionResult> AddEspecialidad(int id, int especialidadId)
    {
        var result = await _service.AddEspecialidadAsync(id, especialidadId);
        return result ? Ok(new { message = "Especialidad asignada correctamente" }) : BadRequest(new { message = "No se pudo asignar la especialidad" });
    }

    [HttpDelete("{id}/especialidades/{especialidadId}")]
    public async Task<IActionResult> RemoveEspecialidad(int id, int especialidadId)
    {
        var result = await _service.RemoveEspecialidadAsync(id, especialidadId);
        return result ? Ok(new { message = "Especialidad removida" }) : NotFound(new { message = "Relación no encontrada" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        return result ? Ok(new { message = "Científico eliminado" }) : NotFound(new { message = "Científico no encontrado" });
    }
}