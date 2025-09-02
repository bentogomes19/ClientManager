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

    [Required(ErrorMessage = "O email é obrigatório"), StringLength(100, ErrorMessage = "O Email deve possuir 100 caracteres."), EmailAddress(ErrorMessage = "O email informado não é válido.")]
    public string Email { get; private set; } = string.Empty;

    [Required(ErrorMessage = "A data de nascimento é obrigatória"), StringLength(10, ErrorMessage = "A Data de Nascimento deve possuir 10 caracteres.")]
    public DateTime DataNascimento { get; private set; }

    public DateTime DataCadastro { get; private set; } = DateTime.Now;

    //campo opcional
    [Range(0, double.MaxValue, ErrorMessage = "A Renda Familiar deve ser um valor positivo.")]
    public decimal RendaFamiliar { get; private set; }

    public Cliente() : base() { } 

    public Cliente(CreateClienteDto dto) : base()
    {
        Nome = dto.Nome;
        Email = dto.Email;
        DataNascimento = dto.DataNascimento;
        DataCadastro = dto.DataCadastro;
    }

    public void UpdateCliente(UpdateClienteDto dto)
    {
        this.Nome = dto.Nome;
        this.Email = dto.Email;
        this.DataNascimento = dto.DataNascimento;
        this.DataCadastro = dto.DataCadastro;
    }


}
