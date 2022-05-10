using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using UseCases.Interfaces;
using UseCases.UserStories;
using WEB.Models;
using WEB.ViewsModels.PersonagemViewModel;

namespace WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BuscarPersonagens _buscarPersonagens;
        private readonly DetalharPersonagem _detalharPersonagem;
        private readonly FavoritarPersonagem _favoritarPersonagem;
        private readonly DesfavoritarPersonagem _desfavoritarPersonagem;

        public HomeController(
            ILogger<HomeController> logger,
            IPersonagemServices personagemServices,
            IPersonagemFavoritoJsonServices personagemFavoritoJsonServices)
        {
            _logger = logger;
            _buscarPersonagens = new BuscarPersonagens(personagemServices);
            _detalharPersonagem = new DetalharPersonagem(personagemServices);
            _favoritarPersonagem = new FavoritarPersonagem(personagemFavoritoJsonServices);
            _desfavoritarPersonagem = new DesfavoritarPersonagem(personagemFavoritoJsonServices);
        }
        
        public async Task<IActionResult> Index(int pagina = 1, string parametroDeBusca = "")
        {
            ViewData["parametroDeBusca"] = parametroDeBusca;
            try
            {
                var result = await _buscarPersonagens.Executar(pagina, parametroDeBusca);
                if (result != null)
                    return View(new ListarPersonagemViewModel(
                        pagina, 
                        result.data.total, 
                        result.data.limit,
                        result.data.results));
                
            }
            catch (Exception erro)
            {
                ViewData["erro"] = erro.Message;
            }
            return View();
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            try
            {
                var personagem = await _detalharPersonagem.Executar(id);
                if (personagem != null)
                    return View(new DetalhesDoPersonagemViewModel(
                        personagem.id.Value, 
                        personagem.name, 
                        personagem.description,
                        personagem.thumbnail.path,
                        personagem.thumbnail.extension
                    ));
                return View();
            }
            catch (Exception erro)
            {
                TempData["erro"] = erro.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public IActionResult Favoritar(int id)
        {
            try
            {
                _favoritarPersonagem.Executar(id);
            }
            catch (Exception erro)
            {
                TempData["erro"] = erro.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Desfavoritar(int id)
        {
            try
            {
                _desfavoritarPersonagem.Executar(id);
            }
            catch (Exception erro)
            {
                TempData["erro"] = erro.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}