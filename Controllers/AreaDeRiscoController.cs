using FloodAlertAPI.Data;
using FloodAlertAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FloodAlertAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AreaDeRiscoController : ControllerBase
{
    private readonly AppDbContext _context;

    public AreaDeRiscoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AreaDeRisco>>> Get()
    {
        return await _context.AreasDeRisco.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AreaDeRisco>> Get(int id)
    {
        var area = await _context.AreasDeRisco.FindAsync(id);
        if (area == null) return NotFound();
        return area;
    }

    [HttpPost]
    public async Task<ActionResult> Post(AreaDeRisco area)
    {
        _context.AreasDeRisco.Add(area);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = area.Id }, area);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, AreaDeRisco area)
    {
        if (id != area.Id) return BadRequest();

        _context.Entry(area).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var area = await _context.AreasDeRisco.FindAsync(id);
        if (area == null) return NotFound();

        _context.AreasDeRisco.Remove(area);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
