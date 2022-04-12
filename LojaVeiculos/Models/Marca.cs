using System.ComponentModel.DataAnnotations;
using LojaVeiculos.EnumModel;

namespace LojaVeiculos.Models { 

public class Marca
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Nome é obrigatório")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Status é obrigatório")]
    public Status Status { get; set; }
}
}