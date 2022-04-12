using LojaVeiculos.Controllers;
using LojaVeiculos.EnumModel;
using LojaVeiculos.IRepository;
using LojaVeiculos.Models;
using LojaVeiculoTeste.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LojaVeiculoTeste.Controllers
{
    public class VeiculoControllerTest : Controller
    {
        VeiculoController _controller;
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IProprietarioRepository _proprietarioRepository;
        private readonly IMarcaRepository _marcaRepository;
        private readonly IRabbitMq _rabbitMq;

        public VeiculoControllerTest()
        {
            _veiculoRepository = new VeiculoRepositoryFake();
            _proprietarioRepository = new ProprietarioRepositoryFake();
            _marcaRepository = new MarcaRepositoryFake();
            _rabbitMq = new RabbitMqFake();
            _controller = new VeiculoController(_veiculoRepository, _proprietarioRepository, _marcaRepository, _rabbitMq);

        }

        public string convertICollectionObjectToJson(ICollection<object?> value)
        {
            var json = JsonConvert.SerializeObject(value);
            json = json.Replace("]", "").Replace("[", "");
            return "[" + json + "]";
        }
        private string convertICollectionObjectToJson(object? value)
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
            List<Veiculo> veiculo = Assert.IsType<List<Veiculo>>(ViewResult.Model);
            Assert.Equal(1, veiculo.Count);
        }

        [Fact]
        public void CadastrarGetTest()
        {
            // Act
            var okResult = _controller.Cadastrar();

            // Assert
            var ViewResult = Assert.IsType<ViewResult>(okResult.Result);
            Assert.Equal(true, ViewResult.ViewData.Keys.Contains("Marca"));
            Assert.Equal(true, ViewResult.ViewData.Keys.Contains("Proprietarios"));
            Assert.Equal(true, ViewResult.ViewData.Keys.Contains("StatusVeiculo"));

            //verificar se lista possui as três posições
            var result = JsonConvert.DeserializeObject<List<statusFake>>(convertICollectionObjectToJson(ViewResult.ViewData.Values.ToArray()[1]));
            Assert.Equal(3, result.Count);
        }


        [Fact]
        public void CadastrarPostTest()
        {
            //Arrange
            Veiculo veiculo = new Veiculo() { Id = 24, ProprietarioID = 1, Anofabricacao = 21, AnoModelo = 13, MarcaID = 1, Modelo = "Ford", Quilometragem = 10.5, Renavam = "12345", Valor = 40000.00 };

            // Act  
            var okResult = _controller.Cadastrar(veiculo);

            // Assert
            var ViewResult = Assert.IsType<ViewResult>(okResult.Result);
            Assert.Equal(true, ViewResult.ViewData.Values.Contains("Cadastrado com sucesso"));

            var list = _controller.Index();
            var ViewResultList = Assert.IsType<ViewResult>(list.Result);
            List<Veiculo> veiculos = Assert.IsType<List<Veiculo>>(ViewResultList.Model);
            //verificar se lista não possui as ultimo carro cadastrado
            Assert.Equal(true, veiculos.Contains(veiculo));
        }


        [Fact]
        public void CadastrarPostTestUnitario()
        {
            //Arrange
            Veiculo veiculo = new Veiculo() { Id = 24, ProprietarioID = 1, Anofabricacao = 21, AnoModelo = 13, MarcaID = 1, Modelo = "Ford", Quilometragem = 10.5, Renavam = "12345", Valor = 40000.00 };

            // Act  
            var okResult = _controller.Cadastrar(veiculo);

            // Assert
            var ViewResult = Assert.IsType<ViewResult>(okResult.Result);
            Assert.Equal(true, ViewResult.ViewData.Values.Contains("Cadastrado com sucesso"));
        }

        [Fact]
        public void status()
        {
            //Arrange

            // Act  
            var okResult = _controller.Status(StatusVeiculo.INDISPONIVEL, StatusVeiculo.DISPONIVEL);

            // Assert
            var ViewResult = Assert.IsType<List<SelectListItem>>(okResult);
            Assert.Equal(2, ViewResult.Count);

            //verificar se DISPONIVEL foi removido
            Assert.Equal(false, ViewResult.Select(x => x.Text).Contains(StatusVeiculo.DISPONIVEL.ToString()));

        }

        [Fact]
        public void statusTesttwo()
        {
            //Arrange

            // Act  
            var okResult = _controller.Status(StatusVeiculo.INDISPONIVEL);

            // Assert
            var ViewResult = Assert.IsType<List<SelectListItem>>(okResult);

            Assert.Equal(3, ViewResult.Count);
        }

        [Fact]
        public void Proprietarios()
        {
            //Arrange

            // Act  
            var okResult = _controller.Proprietarios();

            // Assert
            var ViewResult = Assert.IsType<List<SelectListItem>>(okResult.Result);
            Assert.Equal(3, ViewResult.Count);
        }

        [Fact]
        public void Marca()
        {
            //Arrange

            // Act  
            var okResult = _controller.Marca();

            // Assert
            var ViewResult = Assert.IsType<List<SelectListItem>>(okResult.Result);
            Assert.Equal(2, ViewResult.Count);
        }

        [Fact]
        public void Update()
        {
            int key = 1;
            //Arrange
            Veiculo veiculoFake = _veiculoRepository.find(key).Result;
            // Act  
            var okResult = _controller.Update(key);

            // Assert
            var ViewResult = Assert.IsType<ViewResult>(okResult.Result);
            var veiculo = Assert.IsType<Veiculo>(ViewResult.Model);
            var obj1Str = JsonConvert.SerializeObject(veiculoFake);
            var obj2Str = JsonConvert.SerializeObject(veiculo);
            Assert.Equal(obj1Str, obj2Str);
        }

        [Fact]
        public void UpdatePost()
        {
            //Arrange
            Veiculo veiculoFake = new Veiculo() { Id = 24, ProprietarioID = 1, Anofabricacao = 21, AnoModelo = 13, MarcaID = 1, Modelo = "Ford", Quilometragem = 10.5, Renavam = "12345", Valor = 40000.00 };
            // Act  
            var okResult = _controller.Update(veiculoFake);

            // Assert
            var ViewResult = Assert.IsType<ViewResult>(okResult.Result);
            Assert.Equal(true, ViewResult.ViewData.Values.Contains("Status Indisponivel"));

            Assert.IsType<Veiculo>(ViewResult.Model);
        }

    }
}
