using System.ComponentModel.DataAnnotations;

namespace ClientManager.src.DTOs;

public class CreateClienteDto
{

    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public DateTime DataNascimento { get; set; }

    public DateTime DataCadastro { get; set; } = DateTime.Now;

    public decimal RendaFamiliar { get; set; }

}

public class UpdateClienteDto : CreateClienteDto { }
