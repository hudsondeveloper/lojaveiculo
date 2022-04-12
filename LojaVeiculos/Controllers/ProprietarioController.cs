using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LojaVeiculos.Models;
using LojaVeiculos.Data;
using Microsoft.EntityFrameworkCore;
using LojaVeiculos.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using LojaVeiculos.EnumModel;

namespace LojaVeiculos.Controllers
{
    public class ProprietarioController : Controller
    {
        private readonly IProprietarioRepository _proprietarioRepository;
        public ProprietarioController(IProprietarioRepository proprietarioRepository)
        {
            _proprietarioRepository = proprietarioRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _proprietarioRepository.GetAll());
        }

        public IActionResult Cadastrar()
        {
            ViewBag.StatusProprietario = status();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Proprietario proprietario)
        {
            try
            {
                ViewBag.StatusProprietario = status();
                if (!ModelState.IsValid)
                {
                    @ViewBag.Validation = "Proprietario Invalido";
                    return View();
                }

                if (await _proprietarioRepository.DocumentIsUnique(proprietario) == null)
                {
                    await _proprietarioRepository.Create(proprietario);
                    @ViewBag.Validation = "Cadastrado com sucesso";
                    return View();
                };
                @ViewBag.Validation = "Documento do proprietario existente";


                return View();
            }
            catch (Exception err)
            {
                @ViewBag.Validation = "Erro inesperado" + err;
                return View();
            }
        }

        public List<SelectListItem> status(Status? status = null)
        {
            List<SelectListItem> StatusProprietario = new List<SelectListItem>();

            foreach (Status eVal in Enum.GetValues(typeof(Status)))
            {
                StatusProprietario.Add(new SelectListItem { Text = Enum.GetName(typeof(Status), eVal), Value = ((int)eVal).ToString(), Selected = status == eVal ? true : false });
            }

            return StatusProprietario;
        }
        public async Task<IActionResult> Update(int id)
        {
            Proprietario proprietario = await _proprietarioRepository.find(id);

            ViewBag.StatusProprietario = status(proprietario.Status);

            return View(proprietario);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Proprietario proprietario)
        {
            try
            {
                ViewBag.StatusProprietario = status(proprietario.Status);
                if (!ModelState.IsValid)
                {
                    @ViewBag.Validation = "proprietario Invalido";
                    return View(proprietario);
                }


                if (await _proprietarioRepository.DocumentIsUnique(proprietario) == null)
                {
                    proprietario = await _proprietarioRepository.Update(proprietario);
                    @ViewBag.Validation = "Atualizado com sucesso";
                    return View(proprietario);
                };
                @ViewBag.Validation = "Documento do proprietario existente";


                return View(proprietario);
            }
            catch (Exception err)
            {
                @ViewBag.Validation = "Erro inesperado" + err;
                return View(proprietario);
            }
        }

    }
}