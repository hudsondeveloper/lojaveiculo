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
    public class MarcaController : Controller
    {
        private readonly IMarcaRepository _marcaRepository;
        public MarcaController(IMarcaRepository marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _marcaRepository.GetAll());
        }

        public IActionResult Cadastrar()
        {
            ViewBag.StatusMarca = status();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Marca marca)
        {
            try
            {
                ViewBag.StatusMarca = status();
                if (!ModelState.IsValid)
                {
                    @ViewBag.Validation = "Marca Invalida";
                    return View();
                }

                if (await _marcaRepository.NameIsUnique(marca) == null)
                {
                    await _marcaRepository.Create(marca);
                    @ViewBag.Validation = "Cadastrado com sucesso";
                    return View();
                };
                @ViewBag.Validation = "Nome da marca existente";

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
            List<SelectListItem> StatusMarca = new List<SelectListItem>();

            foreach (Status eVal in Enum.GetValues(typeof(Status)))
            {
                StatusMarca.Add(new SelectListItem { Text = Enum.GetName(typeof(Status), eVal), Value = ((int)eVal).ToString(), Selected = status == eVal ? true : false });
            }

            return StatusMarca;
        }
        public async Task<IActionResult> Update(int id)
        {
            Marca marca = await _marcaRepository.find(id);

            ViewBag.StatusMarca = status(marca.Status);

            return View(marca);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Marca marca)
        {
            try
            {
                ViewBag.StatusMarca = status(marca.Status);
                if (!ModelState.IsValid)
                {
                    @ViewBag.Validation = "Marca Invalida";
                    return View(marca);
                }


                marca = await _marcaRepository.Update(marca);
                @ViewBag.Validation = "Atualizado com sucesso";

                return View(marca);
            }
            catch (Exception err)
            {
                @ViewBag.Validation = "Erro inesperado" + err;
                return View(marca);
            }
        }

    }
}