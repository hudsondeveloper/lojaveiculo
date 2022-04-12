using LojaVeiculos.IRepository;
using LojaVeiculos.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVeiculoTeste.Repository
{
    class VeiculoRepositoryFake : IVeiculoRepository
    {

        private readonly List<Veiculo> _veiculo;
        public VeiculoRepositoryFake()
        {
            _veiculo = new List<Veiculo>()
            {
               new Veiculo() { Id = 1, ProprietarioID = 1, Anofabricacao = 21, AnoModelo = 13, MarcaID = 1, Modelo = "Ford", Quilometragem = 10.5, Renavam = "1234", Valor = 40000.00 }
            };
        }
        public Task<int> Create(Veiculo veiculo)
        {
            _veiculo.Add(veiculo);
            return Task.FromResult(veiculo.Id);
        }

        public Task<Veiculo> RenavamIsUnique(Veiculo veiculo)
        {
            return Task.FromResult(_veiculo.Where(x => x.Renavam == veiculo.Renavam && x.Id != veiculo.Id).FirstOrDefault());
        }

        public Task<Veiculo> find(int id)
        {
            Veiculo veiculo = _veiculo.Where(x => x.Id == id).FirstOrDefault();

            return Task.FromResult(veiculo);
        }

        public Task<List<Veiculo>> GetAll()
        {
            return Task.FromResult(_veiculo.ToList());
        }

        public Task<Veiculo> Update(Veiculo veiculo)
        {
            Veiculo veiculoFind = _veiculo.Where(x => x.Id == veiculo.Id).FirstOrDefault();
            if (veiculoFind != null)
            {

            }
            _veiculo.Remove(veiculo);
            _veiculo.Add(veiculoFind);

            return Task.FromResult(veiculoFind);
        }
    }
}
