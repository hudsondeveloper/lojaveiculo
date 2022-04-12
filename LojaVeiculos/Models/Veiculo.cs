using System.ComponentModel.DataAnnotations;
using LojaVeiculos.EnumModel;

namespace LojaVeiculos.Models { 

public class Veiculo
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Renavam é obrigatório")]
    public string Renavam { get; set; }
    [Required(ErrorMessage = "Modelo é obrigatório")]
    public string Modelo { get; set; }
    [Required(ErrorMessage = "Ano de fabricacao é obrigatório")]
    public int Anofabricacao { get; set; }
    [Required(ErrorMessage = "Ano de Modelo é obrigatório")]
    public int AnoModelo { get; set; }
    [Required(ErrorMessage = "Quilometragem é obrigatório")]
    public double Quilometragem { get; set; }
    [Required(ErrorMessage = "Valor é obrigatório")]
    public double Valor { get; set; }
    public StatusVeiculo StatusVeiculo { get; set; }
    public int ProprietarioID { get; set; }
    public virtual Proprietario Proprietario { get; set; }
    public int MarcaID { get; set; }
    public virtual Marca Marca { get; set; }
}
}