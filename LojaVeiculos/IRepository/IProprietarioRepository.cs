using LojaVeiculos.Models;

namespace LojaVeiculos.IRepository
{
    public interface IProprietarioRepository
    {
        Task<List<Proprietario>> GetAll();

        Task<int> Create(Proprietario proprietario);

        Task<Proprietario> Update(Proprietario proprietario);

        Task<Proprietario> DocumentIsUnique(Proprietario proprietario);

        Task<Proprietario> find(int id);

        Task<List<Proprietario>> findActives();
    }
}