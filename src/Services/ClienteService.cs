using ClientManager.src.DTOs;
using ClientManager.src.Interfaces;
using ClientManager.src.Models;
using ClientManager.src.Exceptions;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace ClientManager.src.Services;

public class ClienteService(IClienteRepository repository)
{
    private readonly IClienteRepository _repository = repository;

    public async Task<ClienteResponseDto> AddAsync(CreateClienteDto dto, CancellationToken ct)
    {
        dto.Validate();
        dto.Cpf = Regex.Replace(dto.Cpf, @"[^\d]", "");

        var clienteExistente = await _repository.GetAsync(c => c.Cpf == dto.Cpf, ct);
        if(clienteExistente != null) throw new BadRequestException("Esse Cpf já foi cadastrado no banco!"); 
        
        Cliente cliente = new(dto);
        await _repository.CreateAsync(cliente, ct);
        return new ClienteResponseDto(cliente);
    }

    public async Task<ApiResponseTable<ClienteResponseDto>> GetAllAsync(string? name, string? cpf, int offset, int limit, CancellationToken ct)
    {
        Expression<Func<Cliente, bool>> filter = c =>
        (string.IsNullOrEmpty(name) || c.Nome.Contains(name)) &&
        (string.IsNullOrEmpty(cpf) || c.Cpf == cpf);
        
        
        var clientes = await _repository.GetAllAsync(filter, offset, limit, ct);
        var totalItems = await _repository.CountAsync(filter, ct);

        return new ApiResponseTable<ClienteResponseDto>
        {
            Message = "Clientes retornados com sucesso!",
            Data = [.. clientes.Select(c => new ClienteResponseDto(c))],
            TotalItems = totalItems
        };

    }

    public async Task<ClienteResponseDto?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var cliente = await _repository.GetByIdAsync(id, ct);
        return cliente == null ? null : new ClienteResponseDto(cliente);
    }

    public async Task<ClienteResponseDto> UpdateAsync(Guid id, UpdateClienteDto dto, CancellationToken ct)
    {
        var cliente = await _repository.GetByIdAsync(id, ct) ?? throw new NotFoundException("Cliente não encontrado.");
        dto.Validate();

        dto.Cpf = Regex.Replace(dto.Cpf, @"[^\d]", "");

        var clienteExistente = await _repository.GetAsync(c => c.Cpf == dto.Cpf && c.Id != cliente.Id, ct);
        if(clienteExistente != null) throw new BadRequestException("Esse Cpf já foi cadastrado no banco!"); 

        cliente.UpdateCliente(dto);

        await _repository.UpdateAsync(cliente, ct);
        return new ClienteResponseDto(cliente);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var cliente = await _repository.GetByIdAsync(id, ct) 
            ?? throw new NotFoundException("Cliente não encontrado.");

        await _repository.DeleteAsync(cliente, ct);
    }
    public async Task<RelatorioResponseDto> GetRelatorioAsync(string filtro, CancellationToken ct)
    {
        var hoje = DateTime.Today;

        if (string.IsNullOrWhiteSpace(filtro))
            throw new BadRequestException("Filtro é obrigatório");
        DateTime? inicioPeriodo = filtro.ToLower() switch
        {
            "hoje" => hoje,
            "semana" => hoje.AddDays(-(int)hoje.DayOfWeek),
            "mes" => new DateTime(hoje.Year, hoje.Month, 1),
            _ => throw new BadRequestException("Filtro invalido")
        };

        var rendaMedia = await _repository.GetRendaMediaAsync(c => inicioPeriodo == null || c.DataCadastro >= inicioPeriodo.Value, ct);


        var data18anos = hoje.AddYears(-18);
        var qtdClientes = await _repository.CountAsync(c =>
            (inicioPeriodo == null || c.DataCadastro >= inicioPeriodo.Value) &&
            c.DataNascimento <= DateOnly.FromDateTime(data18anos) &&
            c.RendaFamiliar >= rendaMedia, ct);


        var qtdClasseA = await _repository.CountAsync(c =>
            (inicioPeriodo == null || c.DataCadastro >= inicioPeriodo.Value) &&
            (c.RendaFamiliar <= 980), ct);

        var qtdClasseB = await _repository.CountAsync(c =>
            (inicioPeriodo == null || c.DataCadastro >= inicioPeriodo.Value) &&
            c.RendaFamiliar > 980 && c.RendaFamiliar <= 2500, ct);

        var qtdClasseC = await _repository.CountAsync(c =>
            (inicioPeriodo == null || c.DataCadastro >= inicioPeriodo.Value) &&
            (c.RendaFamiliar > 2500), ct);

        return new RelatorioResponseDto
        {
            QtdClientesMaior18Anos = qtdClientes,
            QtdClientesClasseA = qtdClasseA,
            QtdClientesClasseB = qtdClasseB,
            QtdClientesClasseC = qtdClasseC
        };
    }
}