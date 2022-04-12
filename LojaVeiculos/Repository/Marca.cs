using LojaVeiculos.Data;
using LojaVeiculos.EnumModel;
using LojaVeiculos.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LojaVeiculos.Models { 

public class MarcaRepository : IMarcaRepository
{
    private readonly Context _context;
    public MarcaRepository(Context context)
    {
        _context = context;
    }

    public Task<List<Marca>> GetAll()
    {
        return _context.Marca.ToListAsync();
    }

    public Task<int> Create(Marca marca)
    {
        _context.Marca.Add(marca);

        return _context.SaveChangesAsync();
    }

    public Task<Marca> NameIsUnique(Marca marca)
    {
                return _context.Marca.Where(x => x.Nome == marca.Nome && x.Id != marca.Id).FirstOrDefaultAsync();
    }


    public Task<Marca> Update(Marca marca)
    {
        Marca marcaFind = _context.Marca.FindAsync(marca.Id).Result;

        if (marcaFind != null)
        {
            marcaFind.Status = marca.Status;
            _context.SaveChanges();
        }

        return Task.FromResult(marcaFind);
    }


    public Task<Marca> find(int id)
    {
        Marca marca = _context.Marca.FindAsync(id).Result;

        return Task.FromResult(marca);
    }

    public Task<List<Marca>> findActives()
    {
        return _context.Marca.Where(x => x.Status == Status.ATIVO).ToListAsync();
    }

}
}