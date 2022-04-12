using System.ComponentModel.DataAnnotations;

namespace LojaVeiculos.EnumModel { 

public enum Status
{
    [Display(Name = "Ativo")] ATIVO, [Display(Name = "Cancelado")] CANCELADO
}
}