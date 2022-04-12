using LojaVeiculos.Data;
using LojaVeiculos.EnumModel;
using LojaVeiculos.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LojaVeiculos.Models { 

public class ProprietarioRepository : IProprietarioRepository
{
    private readonly Context _context;
    public ProprietarioRepository(Context context)
    {
        _context = context;
    }

    public Task<List<Proprietario>> GetAll()
    {
        return _context.Proprietario.ToListAsync();
    }

    public Task<int> Create(Proprietario proprietario)
    {
        _context.Proprietario.Add(proprietario);

        return _context.SaveChangesAsync();
    }



    public Task<Proprietario> Update(Proprietario proprietario)
    {
        Proprietario proprietarioFind = _context.Proprietario.FindAsync(proprietario.Id).Result;

        if (proprietarioFind != null)
        {

            proprietarioFind.Documento = proprietario.Documento;
            proprietarioFind.Email = proprietario.Email;
            proprietarioFind.Endereco = proprietario.Endereco;
            proprietarioFind.Nome = proprietario.Nome;
            proprietarioFind.Status = proprietario.Status;
            _context.SaveChanges();
        }

        return Task.FromResult(proprietarioFind);
    }

    public Task<Proprietario> DocumentIsUnique(Proprietario proprietario)
    {
        return _context.Proprietario.Where(x => x.Documento == proprietario.Documento && x.Id != proprietario.Id).FirstOrDefaultAsync();
    }


    public Task<Proprietario> find(int id)
    {
        Proprietario proprietario = _context.Proprietario.FindAsync(id).Result;

        return Task.FromResult(proprietario);
    }

    public Task<List<Proprietario>> findActives()
    {
        return _context.Proprietario.Where(x => x.Status == Status.ATIVO).ToListAsync();
    }
}
}