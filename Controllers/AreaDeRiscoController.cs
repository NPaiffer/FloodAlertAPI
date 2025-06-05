using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FloodAlertAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class AreaDeRiscoController : ControllerBase
{
    private readonly AppDbContext _context;

    public AreaDeRiscoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetAreas()
    {
        var areas = await _context.AreasDeRisco.ToListAsync();
        return areas.Select(area => new
        {
            area.Id,
            area.Nome,
            area.Latitude,
            area.Longitude,
            area.NivelDeRisco,
            area.Descricao,
            _links = new
            {
                self = Url.Action(nameof(GetArea), new { id = area.Id }),
                registros = Url.Action("GetRegistros", "RegistroDeEnchente", new { areaId = area.Id }),
                update = Url.Action(nameof(UpdateArea), new { id = area.Id }),
                delete = Url.Action(nameof(DeleteArea), new { id = area.Id })
            }
        }).ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AreaDeRisco>> GetArea(int id)
    {
        var area = await _context.AreasDeRisco.FindAsync(id);
        if (area == null) return NotFound();
        return area;
    }

    [HttpPost]
    public async Task<ActionResult<AreaDeRisco>> CreateArea(AreaDeRisco area)
    {
        _context.AreasDeRisco.Add(area);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetArea), new { id = area.Id }, area);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateArea(int id, AreaDeRisco area)
    {
        if (id != area.Id) return BadRequest();
        _context.Entry(area).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArea(int id)
    {
        var area = await _context.AreasDeRisco.FindAsync(id);
        if (area == null) return NotFound();
        _context.AreasDeRisco.Remove(area);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
