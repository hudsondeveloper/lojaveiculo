using LojaVeiculos.Data;
using LojaVeiculos.EnumModel;
using LojaVeiculos.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LojaVeiculos.Models { 

public class VeiculoRepository : IVeiculoRepository
{
    private readonly Context _context;
    public VeiculoRepository(Context context)
    {
        _context = context;
    }

    public Task<List<Veiculo>> GetAll()
    {
        return _context.Veiculo.ToListAsync();
    }

    public Task<int> Create(Veiculo veiculo)
    {
        _context.Veiculo.Add(veiculo);

        return _context.SaveChangesAsync();
    }



    public Task<Veiculo> Update(Veiculo veiculo)
    {
        Veiculo veiculoFind = _context.Veiculo.FindAsync(veiculo.Id).Result;

        if (veiculoFind != null)
        {
            veiculoFind.Anofabricacao = veiculo.Anofabricacao;
            veiculoFind.AnoModelo = veiculo.AnoModelo;
            veiculoFind.MarcaID = veiculo.MarcaID;
            veiculoFind.Modelo = veiculo.Modelo;
            veiculoFind.ProprietarioID = veiculo.ProprietarioID;
            veiculoFind.Quilometragem = veiculo.Quilometragem;
            veiculoFind.Renavam = veiculo.Renavam;
            veiculoFind.StatusVeiculo = veiculo.StatusVeiculo;
            veiculoFind.Valor = veiculo.Valor;

            _context.SaveChanges();
        }

        return Task.FromResult(veiculoFind);
    }

    public Task<Veiculo> RenavamIsUnique(Veiculo veiculo)
    {
        return _context.Veiculo.Where(x => x.Renavam == veiculo.Renavam && x.Id != veiculo.Id).FirstOrDefaultAsync();
    }


    public Task<Veiculo> find(int id)
    {
        Veiculo veiculo = _context.Veiculo.FindAsync(id).Result;

        return Task.FromResult(veiculo);
    }

}
}