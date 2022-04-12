using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LojaVeiculos.EnumModel;

namespace LojaVeiculos.Models { 

public class Proprietario
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Nome é obrigatório")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "Documento é obrigatório")]
    public string Documento { get; set; }
    [Required(ErrorMessage = "Email é obrigatório")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Endereco é obrigatório")]
    public string Endereco { get; set; }
    [NotMapped]
    public string Cep { get; set; }
    [Required(ErrorMessage = "Status é obrigatório")]
    public Status Status { get; set; }
}
}