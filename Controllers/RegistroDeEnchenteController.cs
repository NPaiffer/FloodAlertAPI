using FloodAlertAPI.Data;
using FloodAlertAPI.Models;
using FloodAlertAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FloodAlertAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistroDeEnchenteController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly RabbitMQService _rabbit;
    private readonly PredictService _predict;

    public RegistroDeEnchenteController(AppDbContext context, RabbitMQService rabbit, PredictService predict)
    {
        _context = context;
        _rabbit = rabbit;
        _predict = predict;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RegistroDeEnchente>>> Get()
    {
        return await _context.RegistrosDeEnchente
            .Include(r => r.AreaDeRisco)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RegistroDeEnchente>> Get(int id)
    {
        var registro = await _context.RegistrosDeEnchente
            .Include(r => r.AreaDeRisco)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (registro == null)
            return NotFound();

        return registro;
    }

    [HttpPost]
    public async Task<ActionResult> Post(RegistroDeEnchente registro)
    {
        var predicao = _predict.PreverEnchente(registro.NivelDaAgua);
        _rabbit.PublicarMensagem($"Novo Registro de Enchente: {predicao}");

        _context.RegistrosDeEnchente.Add(registro);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = registro.Id }, registro);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, RegistroDeEnchente registro)
    {
        if (id != registro.Id)
            return BadRequest("ID não confere.");

        var registroExistente = await _context.RegistrosDeEnchente.FindAsync(id);
        if (registroExistente == null)
            return NotFound("Registro não encontrado.");

        registroExistente.Data = registro.Data;
        registroExistente.Descricao = registro.Descricao;
        registroExistente.NivelDaAgua = registro.NivelDaAgua;
        registroExistente.AreaDeRiscoId = registro.AreaDeRiscoId;

        _context.Entry(registroExistente).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        _rabbit.PublicarMensagem($"Registro de Enchente ID {id} foi atualizado.");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var registro = await _context.RegistrosDeEnchente.FindAsync(id);
        if (registro == null)
            return NotFound("Registro não encontrado.");

        _context.RegistrosDeEnchente.Remove(registro);
        await _context.SaveChangesAsync();

        _rabbit.PublicarMensagem($"Registro de Enchente ID {id} foi removido.");

        return NoContent();
    }
}
