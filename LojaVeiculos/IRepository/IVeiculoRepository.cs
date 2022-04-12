using LojaVeiculos.Models;

namespace LojaVeiculos.IRepository
{
    public interface IVeiculoRepository
    {
        Task<List<Veiculo>> GetAll();

        Task<int> Create(Veiculo veiculo);


        Task<Veiculo> Update(Veiculo veiculo);


        Task<Veiculo> RenavamIsUnique(Veiculo veiculo);

        Task<Veiculo> find(int id);

    }
}