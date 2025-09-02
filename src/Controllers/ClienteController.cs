using ClientManager.src.DTOs;
using ClientManager.src.Infrastructure;
using ClientManager.src.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientManager.src.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly AplicationDbContext _context;

    public ClienteController(AplicationDbContext context)
    {
        _context = context;
    }

    // MÉTODO HTTP POST

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateClienteDto dto, CancellationToken ct)
    {
        try
        {
            var cliente = new Cliente(dto);

            await _context.Clientes.AddAsync(cliente, ct);
            await _context.SaveChangesAsync(ct);

            return Ok(cliente);

        } catch (Exception ex)
        {
            return BadRequest("Erro ao adicionar um Cliente.");
        }
    }
}
