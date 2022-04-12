using LojaVeiculos.IRepository;
using LojaVeiculos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaVeiculoTeste.Repository
{
    class MarcaRepositoryFake : IMarcaRepository
    {

        private readonly List<Marca> _marca;
        public MarcaRepositoryFake()
        {
            _marca = new List<Marca>()
            {
                new Marca() { Id=1,Nome="BMW",Status=LojaVeiculos.EnumModel.Status.ATIVO},
                new Marca() { Id=2,Nome="Hyundai",Status=LojaVeiculos.EnumModel.Status.CANCELADO},
                new Marca() { Id=3,Nome="Renault",Status=LojaVeiculos.EnumModel.Status.ATIVO},
                new Marca() { Id=4,Nome="Mercedes Benz",Status=LojaVeiculos.EnumModel.Status.CANCELADO},
            };
        }
        public Task<int> Create(Marca marca)
        {
            _marca.Add(marca);
            return Task.FromResult(marca.Id);
        }

        public Task<Marca> find(int id)
        {
            Marca marca = _marca.Where(x=>x.Id == id).FirstOrDefault();

            return Task.FromResult(marca);
        }

        public Task<List<Marca>> findActives()
        {
           
            return Task.FromResult(_marca.Where(x => x.Status == LojaVeiculos.EnumModel.Status.ATIVO).ToList());
        }

        public Task<List<Marca>> GetAll()
        {
            return Task.FromResult(_marca.ToList());
        }

        public Task<Marca> NameIsUnique(Marca marca)
        {
            return Task.FromResult(_marca.Where(x => x.Nome == marca.Nome && x.Id != marca.Id).FirstOrDefault());
        }

        public Task<Marca> Update(Marca marca)
        {
            Marca marcaFind = _marca.Where(x => x.Id == marca.Id).FirstOrDefault();
            if(marcaFind != null)
            {
                marcaFind.Nome = marca.Nome;
                marcaFind.Status = marca.Status;
            }
            _marca.Remove(marca);
            _marca.Add(marcaFind);

            return Task.FromResult(marcaFind);
        }
    }
}
