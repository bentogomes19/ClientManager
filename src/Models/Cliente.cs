using ClientManager.src.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientManager.src.Models;

[Table("Cliente")]
public class Cliente
{
    [Key]
    [Required]
    public Guid Id { get; private set; }

    [Required(ErrorMessage = "O nome é obrigatório"), StringLength(150, ErrorMessage = "O Nome deve possuir 150 caracteres.")]
    public string Nome { get; private set; } = string.Empty;

    [StringLength(11, ErrorMessage = "O CPF deve possuir 11 caracteres.")]
    public string Cpf { get; private set; } = string.Empty;

    [Required(ErrorMessage = "A data de nascimento é obrigatória")]
    public DateOnly DataNascimento { get; private set; }

    public DateTime DataCadastro { get; private set; } = DateTime.Now;

    //campo opcional
    [Column(TypeName = "decimal(18,4)"), Range(0, double.MaxValue, ErrorMessage = "A Renda Familiar deve ser um valor positivo.")]
    public decimal RendaFamiliar { get; private set; }

    public Cliente() : base() { }

    public Cliente(CreateClienteDto dto) : base()
    {
        Nome = dto.Nome;
        Cpf = dto.Cpf;
        DataNascimento = dto.DataNascimento;
        RendaFamiliar = dto.RendaFamiliar;
        DataCadastro = dto.DataCadastro;
    }

    public void UpdateCliente(UpdateClienteDto dto)
    {
        Nome = dto.Nome;
        Cpf = dto.Cpf;
        DataNascimento = dto.DataNascimento;
        RendaFamiliar = dto.RendaFamiliar;
    }
}
