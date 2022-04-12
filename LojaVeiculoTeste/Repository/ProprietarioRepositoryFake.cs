using LojaVeiculos.IRepository;
using LojaVeiculos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaVeiculoTeste.Repository
{
    class ProprietarioRepositoryFake : IProprietarioRepository
    {

        private readonly List<Proprietario> _proprietario;
        public ProprietarioRepositoryFake()
        {
            _proprietario = new List<Proprietario>()
            {
                new Proprietario() { Id=1,Nome="Hudson Luis da Silva Costa",Status=LojaVeiculos.EnumModel.Status.ATIVO,Cep="41320010",Documento="05970003522",Email="hudsonluis.costa@gmail.com",Endereco="Cep: 41320172, Estado: BA, Cidade: Salvador, Bairro: Castelo Branco, Rua: Via Castelo Branco"},
                new Proprietario() { Id=2,Nome="Marcus Alexandre",Status=LojaVeiculos.EnumModel.Status.ATIVO,Cep="41342430",Documento="60880329017",Email="Alexandre.costa@gmail.com",Endereco="Cep: 41342430, Estado: BA, Cidade: Salvador, Bairro: Fazenda Grande II, Rua: Avenida BB"},
                new Proprietario() { Id=2,Nome="Lindinalva Nascimento",Status=LojaVeiculos.EnumModel.Status.ATIVO,Cep="41300600",Documento="48037361039",Email="Lindinalva@gmail.com",Endereco="Cep: 41300600, Estado: BA, Cidade: Salvador, Bairro: Valéria, Rua: Rua da Matriz"},
            };
        }
        public Task<int> Create(Proprietario proprietario)
        {
            _proprietario.Add(proprietario);
            return Task.FromResult(proprietario.Id);
        }

        public Task<Proprietario> DocumentIsUnique(Proprietario proprietario)
        {
            return Task.FromResult(_proprietario.Where(x => x.Documento == proprietario.Documento && x.Id != proprietario.Id).FirstOrDefault());
        }

        public Task<Proprietario> find(int id)
        {
            Proprietario proprietario = _proprietario.Where(x=>x.Id == id).FirstOrDefault();

            return Task.FromResult(proprietario);
        }

        public Task<List<Proprietario>> findActives()
        {
           
            return Task.FromResult(_proprietario.Where(x => x.Status == LojaVeiculos.EnumModel.Status.ATIVO).ToList());
        }

        public Task<List<Proprietario>> GetAll()
        {
            return Task.FromResult(_proprietario.ToList());
        }

        public Task<Proprietario> Update(Proprietario proprietario)
        {
            Proprietario proprietarioFind = _proprietario.Where(x => x.Id == proprietario.Id).FirstOrDefault();
            if(proprietarioFind != null)
            {
                proprietarioFind.Documento = proprietario.Documento;
                proprietarioFind.Email = proprietario.Email;
                proprietarioFind.Endereco = proprietario.Endereco;
                proprietarioFind.Nome = proprietario.Nome;
                proprietarioFind.Status = proprietario.Status;
            }
            _proprietario.Remove(proprietario);
            _proprietario.Add(proprietarioFind);

            return Task.FromResult(proprietarioFind);
        }
    }
}
