using LojaVeiculos.Models;

namespace LojaVeiculos.IRepository
{
    public interface IMarcaRepository
    {
        Task<List<Marca>> GetAll();

        Task<int> Create(Marca marca);
        Task<Marca> NameIsUnique(Marca marca);

        Task<Marca> Update(Marca marca);

        Task<Marca> find(int id);

        Task<List<Marca>> findActives();

    }
}