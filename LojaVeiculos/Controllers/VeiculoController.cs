using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LojaVeiculos.Models;
using LojaVeiculos.Data;
using Microsoft.EntityFrameworkCore;
using LojaVeiculos.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using LojaVeiculos.EnumModel;
using System.Text;
using RabbitMQ.Client;

namespace LojaVeiculos.Controllers
{
    public class VeiculoController : Controller
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IProprietarioRepository _proprietarioRepository;
        private readonly IMarcaRepository _marcaRepository;
        private readonly IRabbitMq _rabbitMq;

        public VeiculoController(IVeiculoRepository veiculoRepository, IProprietarioRepository proprietarioRepository, IMarcaRepository marcaRepository, IRabbitMq rabbitMq)
        {
            _veiculoRepository = veiculoRepository;
            _proprietarioRepository = proprietarioRepository;
            _marcaRepository = marcaRepository;
            _rabbitMq = rabbitMq;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _veiculoRepository.GetAll());
        }

        public async Task<IActionResult> Cadastrar()
        {
            ViewBag.Marca = await Marca();
            ViewBag.Proprietarios = await Proprietarios();
            ViewBag.StatusVeiculo = Status();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Veiculo veiculo)
        {
            try
            {
                ViewBag.Marca = await Marca();
                ViewBag.Proprietarios = await Proprietarios();
                ViewBag.StatusVeiculo = Status();
                veiculo.Proprietario = await _proprietarioRepository.find(veiculo.ProprietarioID);
                veiculo.Marca = await _marcaRepository.find(veiculo.MarcaID);


                if (await _veiculoRepository.RenavamIsUnique(veiculo) == null)
                {
                    await _veiculoRepository.Create(veiculo);
                    @ViewBag.Validation = "Cadastrado com sucesso";


                    var factory = _rabbitMq.conection();

                    using (var connection = factory.CreateConnection())
                    {
                        using (var channel = connection.CreateModel())
                        {
                            channel.QueueDeclare(queue: "enviarEmailCadastroVeiculo", durable: false, exclusive: false, autoDelete: false, arguments: null);

                            var body = Encoding.UTF8.GetBytes(veiculo.Proprietario.Email);

                            channel.BasicPublish(exchange: "", routingKey: "enviarEmailCadastroVeiculo", basicProperties: null, body: body);
                        }
                    }
                    return View();
                };
                @ViewBag.Validation = "Renavam do veiculo existente";


                return View();
            }
            catch (Exception err)
            {
                @ViewBag.Validation = "Erro inesperado" + err;
                return View();
            }
        }

        public List<SelectListItem> Status(StatusVeiculo? status = null, StatusVeiculo? statusRemovido = null)
        {
            List<SelectListItem> StatusVeiculo = new List<SelectListItem>();

            foreach (StatusVeiculo eVal in Enum.GetValues(typeof(StatusVeiculo)))
            {
                if (statusRemovido != null)
                {
                    if (statusRemovido != eVal)
                    {
                        StatusVeiculo.Add(new SelectListItem { Text = Enum.GetName(typeof(StatusVeiculo), eVal), Value = ((int)eVal).ToString(), Selected = status == eVal ? true : false });

                    }
                }
                else
                {
                    StatusVeiculo.Add(new SelectListItem { Text = Enum.GetName(typeof(StatusVeiculo), eVal), Value = ((int)eVal).ToString(), Selected = status == eVal ? true : false });
                }
            }

            return StatusVeiculo;
        }




        public async Task<List<SelectListItem>> Proprietarios()
        {
            List<SelectListItem> proprietarios = new List<SelectListItem>();

            List<Proprietario> proprietariosAtivos = await _proprietarioRepository.findActives();

            proprietariosAtivos.ForEach(x => proprietarios.Add(new SelectListItem { Text = x.Nome, Value = x.Id.ToString() }));

            return proprietarios;
        }

        public async Task<List<SelectListItem>> Marca()
        {
            List<SelectListItem> marca = new List<SelectListItem>();

            List<Marca> marcaAtivos = await _marcaRepository.findActives();

            marcaAtivos.ForEach(x => marca.Add(new SelectListItem { Text = x.Nome, Value = x.Id.ToString() }));

            return marca;
        }
        public async Task<IActionResult> Update(int id)
        {

            Veiculo veiculo = await _veiculoRepository.find(id);
            ViewBag.Marca = await Marca();
            ViewBag.Proprietarios = await Proprietarios();
            ViewBag.StatusVeiculo = Status(veiculo.StatusVeiculo, StatusVeiculo.DISPONIVEL);


            return View(veiculo);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Veiculo veiculo)
        {
            try
            {
                ViewBag.Marca = await Marca();
                ViewBag.Proprietarios = await Proprietarios();
                ViewBag.StatusVeiculo = Status();
                ViewBag.StatusVeiculo = Status(veiculo.StatusVeiculo, StatusVeiculo.DISPONIVEL);

                if (veiculo.StatusVeiculo == StatusVeiculo.DISPONIVEL)
                {
                    @ViewBag.Validation = "Status Indisponivel";
                    return View(veiculo);
                };

                if (await _veiculoRepository.RenavamIsUnique(veiculo) == null)
                {
                    veiculo = await _veiculoRepository.Update(veiculo);
                    @ViewBag.Validation = "Atualizado com sucesso";
                    return View(veiculo);
                };
                @ViewBag.Validation = "Documento do veiculo existente";


                return View(veiculo);
            }
            catch (Exception err)
            {
                @ViewBag.Validation = "Erro inesperado" + err;
                return View(veiculo);
            }
        }

    }
}