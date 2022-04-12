using LojaVeiculos.Controllers;
using LojaVeiculos.IRepository;
using LojaVeiculos.Models;
using LojaVeiculoTeste.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LojaVeiculoTeste.Controllers
{
    public class statusFake
    {        
        public bool Disabled { get; set; }
        public bool  Selected { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }

    }
    public class MarcaControllerTest : Controller
    {
        MarcaController _controller;
        private readonly IMarcaRepository _marcaRepository;
        public MarcaControllerTest()
        {
            _marcaRepository = new MarcaRepositoryFake();
            _controller = new MarcaController(_marcaRepository);

        }

        public string convertICollectionObjectToJson(ICollection<object?> value)
        {
            var json = JsonConvert.SerializeObject(value);
            json = json.Replace("]", "").Replace("[", "");
            return "[" + json + "]";
        }

        [Fact]
        public void IndexTest()
        {
            // Act
            var okResult = _controller.Index();
            // Assert
            var ViewResult = Assert.IsType<ViewResult>(okResult.Result);
            List<Marca> marcas = Assert.IsType<List<Marca>>(ViewResult.Model);
            Assert.Equal(4, marcas.Count);
        }

        [Fact]
        public void CadastrarGetTest()
        {
            // Act
            var okResult = _controller.Cadastrar();

            // Assert
            var ViewResult = Assert.IsType<ViewResult>(okResult);
            Assert.Equal("StatusMarca",ViewResult.ViewData.Keys.First());            
                        
            //verificar se lista possui as duas posições
            var result = JsonConvert.DeserializeObject<List<statusFake>>(convertICollectionObjectToJson(ViewResult.ViewData.Values));        
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void CadastrarPostTest()
        {
            //Arrange
            Marca marca = new Marca() { Id = 15, Nome = "Volkswagen", Status = LojaVeiculos.EnumModel.Status.ATIVO };

            // Act  
            var okResult = _controller.Cadastrar(marca);
    
            // Assert
            var ViewResult = Assert.IsType<ViewResult>(okResult.Result);
            Assert.Equal(true, ViewResult.ViewData.Values.Contains("Cadastrado com sucesso"));

            var list = _controller.Index();
            var ViewResultList = Assert.IsType<ViewResult>(list.Result);
            List<Marca> marcas = Assert.IsType<List<Marca>>(ViewResultList.Model);
            //verificar se lista possui as ultimo carro cadastrado
            Assert.Equal(true, marcas.Contains(marca));
        }

        [Fact]
        public void CadastrarPostTestUnitario()
        {
            //Arrange
            Marca marca = new Marca() { Id = 15, Nome = "Volkswagen", Status = LojaVeiculos.EnumModel.Status.ATIVO };

            // Act  
            var okResult = _controller.Cadastrar(marca);

           // Assert
            var ViewResult = Assert.IsType<ViewResult>(okResult.Result);
            Assert.Equal(true, ViewResult.ViewData.Values.Contains("Cadastrado com sucesso"));        
        }


        [Fact]
        public void status()
        {
            //Arrange

            // Act  
            var okResult = _controller.status();

            // Assert
            var ViewResult = Assert.IsType<List<SelectListItem>>(okResult);
            Assert.Equal(2, ViewResult.Count);
        }


        [Fact]
        public void Update()
        {
            int key = 1;
            //Arrange
            Marca marcaFake = _marcaRepository.find(key).Result;
            // Act  
            var okResult = _controller.Update(key);

            // Assert
            var ViewResult = Assert.IsType<ViewResult>(okResult.Result);
            var Marca = Assert.IsType<Marca>(ViewResult.Model);
            var obj1Str = JsonConvert.SerializeObject(marcaFake);
            var obj2Str = JsonConvert.SerializeObject(Marca);
            Assert.Equal(obj1Str, obj2Str);
        }

        [Fact]
        public void UpdatePost()
        {
            //Arrange
            Marca? marcaFake = new Marca() { Id = 2, Nome = "BMW", Status = LojaVeiculos.EnumModel.Status.CANCELADO };
            // Act  
            var okResult = _controller.Update(marcaFake);

            // Assert
            var ViewResult = Assert.IsType<ViewResult>(okResult.Result);
            Assert.Equal(true, ViewResult.ViewData.Values.Contains("Atualizado com sucesso"));

            Assert.IsType<Marca>(ViewResult.Model);
        }

    }
}
