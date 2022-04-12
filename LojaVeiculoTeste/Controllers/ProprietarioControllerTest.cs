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
    public class ProprietarioControllerTest : Controller
    {
        ProprietarioController _controller;
        private readonly IProprietarioRepository _proprietarioRepository;
        public ProprietarioControllerTest()
        {
            _proprietarioRepository = new ProprietarioRepositoryFake();
            _controller = new ProprietarioController(_proprietarioRepository);

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
            List<Proprietario> proprietario = Assert.IsType<List<Proprietario>>(ViewResult.Model);
            Assert.Equal(3, proprietario.Count);
        }

        [Fact]
        public void CadastrarGetTest()
        {
            // Act
            var okResult = _controller.Cadastrar();

            // Assert
            var ViewResult = Assert.IsType<ViewResult>(okResult);
            Assert.Equal("StatusProprietario", ViewResult.ViewData.Keys.First());

            //verificar se lista possui as duas posições
            var result = JsonConvert.DeserializeObject<List<statusFake>>(convertICollectionObjectToJson(ViewResult.ViewData.Values));
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void CadastrarPostTest()
        {
            //Arrange
            Proprietario proprietario = new Proprietario() { Id = 15, Nome = "Hudson Luis da Silva Costa", Status = LojaVeiculos.EnumModel.Status.ATIVO, Cep = "41320010", Documento = "05970003522", Email = "hudsonluis.costa@gmail.com", Endereco = "Cep: 41320172, Estado: BA, Cidade: Salvador, Bairro: Castelo Branco, Rua: Via Castelo Branco" };
;

            // Act  
            var okResult = _controller.Cadastrar(proprietario);

            // Assert
            var ViewResult = Assert.IsType<ViewResult>(okResult.Result);
            Assert.Equal(true, ViewResult.ViewData.Values.Contains("Documento do proprietario existente"));

            var list = _controller.Index();
            var ViewResultList = Assert.IsType<ViewResult>(list.Result);
            List<Proprietario> proprietarios = Assert.IsType<List<Proprietario>>(ViewResultList.Model);
            //verificar se lista não possui as ultimo carro cadastrado
            Assert.Equal(false, proprietarios.Contains(proprietario));
        }

        [Fact]
        public void CadastrarPostTestUnitario()
        {
            //Arrange
            Proprietario proprietario = new Proprietario() { Id = 15, Nome = "Hudson Luis da Silva Costa", Status = LojaVeiculos.EnumModel.Status.ATIVO, Cep = "41320010", Documento = "05970003544", Email = "hudsonluis.costa@gmail.com", Endereco = "Cep: 41320172, Estado: BA, Cidade: Salvador, Bairro: Castelo Branco, Rua: Via Castelo Branco" };

            // Act  
            var okResult = _controller.Cadastrar(proprietario);

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
            Proprietario proprietarioFake = _proprietarioRepository.find(key).Result;
            // Act  
            var okResult = _controller.Update(key);

            // Assert
            var ViewResult = Assert.IsType<ViewResult>(okResult.Result);
            var proprietario = Assert.IsType<Proprietario>(ViewResult.Model);
            var obj1Str = JsonConvert.SerializeObject(proprietarioFake);
            var obj2Str = JsonConvert.SerializeObject(proprietario);
            Assert.Equal(obj1Str, obj2Str);
        }

        [Fact]
        public void UpdatePost()
        {
            //Arrange
            Proprietario proprietarioFake = new Proprietario() { Id = 2, Nome = "Hudson Luis da Silva Costa", Status = LojaVeiculos.EnumModel.Status.ATIVO, Cep = "41320010", Documento = "05970003522", Email = "hudsonluis.costa@gmail.com", Endereco = "Cep: 41320172, Estado: BA, Cidade: Salvador, Bairro: Castelo Branco, Rua: Via Castelo Branco" };
            // Act  
            var okResult = _controller.Update(proprietarioFake);

            // Assert
            var ViewResult = Assert.IsType<ViewResult>(okResult.Result);
            Assert.Equal(true, ViewResult.ViewData.Values.Contains("Documento do proprietario existente"));

            Assert.IsType<Proprietario>(ViewResult.Model);
        }

    }
}
