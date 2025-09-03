using ClientManager.src.DTOs;
using ClientManager.src.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClientManager.src.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController(ClienteService service) : ControllerBase
{
    private readonly ClienteService _service = service;

    // MÉTODO HTTP POST (Cadastrar um cliente)
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateClienteDto dto, CancellationToken ct)
    {
        var cliente = await _service.AddAsync(dto, ct);
        return Ok(new ApiResponse<ClienteResponseDto>
        {
            Message = "Cliente Criado com sucesso.",
            Data = cliente
        });
    }

    // MÉTODO HTTP GET ALL (Buscar todos os clientes cadastrados)
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken ct, [FromQuery] string? name = null, [FromQuery] string? cpf = null, [FromQuery] int offset = 0, [FromQuery] int pageSize = 20)
    {
        var clientes = await _service.GetAllAsync(name, cpf, offset, pageSize, ct);
        return Ok(clientes);
    }

    // MÉTODO HTTP GET BY ID (Buscar cliente por um Id)
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken ct)
    {
        var cliente = await _service.GetByIdAsync(id, ct);

        return Ok(new ApiResponse<ClienteResponseDto?>
        {
            Message = "Cliente encontrado com suceso.",
            Data = cliente
        });

    }

    // MÉTODO HTTP PUT (Atualizar dados)
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateClienteDto dto, CancellationToken ct)
    {
        var cliente = await _service.UpdateAsync(id, dto, ct);

        return Ok(new ApiResponse<ClienteResponseDto>
        {
            Message = "Cliente Atualizado com sucesso.",
            Data = cliente
        });
    }

    // MÉTODO HTTP DELETE (Apagar Dados)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        return Ok(new ApiResponseMessage
        {
            Message = "Cliente Excluído com sucesso."
        });
    }

    // MÉTODO GET RELATÓRIOS
    [HttpGet("relatorios")]
    public async Task<IActionResult> GetRelatoriosAsync([FromQuery] string filtro, CancellationToken ct)
    {
        var relatorios = await _service.GetRelatorioAsync(filtro, ct);
        return Ok(relatorios);
    }

}
