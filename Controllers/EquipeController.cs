using Microsoft.AspNetCore.Mvc;
using Projeto_Gamer_MVC.Infra;
using Projeto_Gamer_MVC.Models;

namespace Projeto_Gamer_MVC.Controllers
{
    [Route("[controller]")]
    public class EquipeController : Controller
    {
        private readonly ILogger<EquipeController> _logger;

        public EquipeController(ILogger<EquipeController> logger)
        {
            _logger = logger;
        }

        Context c = new Context(); // Instancia do objeto da classe Context.cs, acessa o banco de dados

        // controller/action
        [Route("Listar")] // http://localhost/Equipe/Listar
        public IActionResult Index()
        {
            ViewBag.Equipe = c.Equipe.ToList(); // "Mochila" que contem a lista de equipes, usada na View Equipe
            return View(); // Retorna a View Equipes
        }

        public IActionResult Cadastrar(IFormCollection form)
        {
          Equipe novaEquipe = new Equipe();

          novaEquipe.Nome =  form["Nome"].ToString();
          novaEquipe.Imagem =  form["Imagem"].ToString();

          c.Equipe.Add(novaEquipe);

          c.SaveChanges();
          ViewBag.Equipe = c.Equipe.ToList();

          return LocalRedirect("~/Equipe/Listar");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}