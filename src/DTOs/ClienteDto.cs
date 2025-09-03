using ClientManager.src.Attributes;
using ClientManager.src.DTOs.Base;
using ClientManager.src.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientManager.src.DTOs;

public class CreateClienteDto : BaseDto
{
    [Required(ErrorMessage = "O nome é obrigatório"), StringLength(150, ErrorMessage = "O Nome deve possuir 150 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [StringLength(14, ErrorMessage = "O CPF deve possuir 14 caracteres.")]
    [Cpf(ErrorMessage = "CPF inválido")]
    public string Cpf { get; set; } = string.Empty;

    [Required(ErrorMessage = "A data de nascimento é obrigatória")]
    [DataType(DataType.Date)]
    [DateMaxtoDay("1900-01-01", ErrorMessage = "A data deve estar entre 1900-01-01 e hoje")]
    public DateOnly DataNascimento { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "A Renda Familiar deve ser um valor positivo.")]
    public decimal RendaFamiliar { get; set; }

}

public class UpdateClienteDto : CreateClienteDto { }


public class ClienteResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public DateOnly DataNascimento { get; set; }
    public decimal RendaFamiliar { get; set; }
    public ClienteResponseDto()
    {

    }
    public ClienteResponseDto(Cliente cliente)
    {
        this.Id = cliente.Id;
        this.Nome = cliente.Nome;
        this.Cpf = cliente.Cpf;
        this.DataNascimento = cliente.DataNascimento;
        this.RendaFamiliar = cliente.RendaFamiliar;
    }
}

