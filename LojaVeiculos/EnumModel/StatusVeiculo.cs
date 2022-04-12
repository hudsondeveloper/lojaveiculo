using System.ComponentModel.DataAnnotations;

namespace LojaVeiculos.EnumModel { 

public enum StatusVeiculo
{
    [Display(Name = "Disponível")] DISPONIVEL, [Display(Name = "Indisponível")] INDISPONIVEL, [Display(Name = "Vendido")] VENDIDO
}
}