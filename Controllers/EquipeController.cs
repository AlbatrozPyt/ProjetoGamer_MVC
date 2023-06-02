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

        // ***METODO LISTAR*** //
        [Route("Listar")] // http://localhost/Equipe/Listar
        public IActionResult Index()
        {
            ViewBag.Equipe = c.Equipe.ToList(); // "Mochila" que contem a lista de equipes, usada na View Equipe
            return View(); // Retorna a View Equipes
        }

        // ***METODO CADASTRAR*** //
        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            Equipe novaEquipe = new Equipe();

            novaEquipe.Nome = form["Nome"].ToString(); // Recebe nome digitado no formulario

            // ****LOGICA DO UPLOAD DA IMAGEM**** //
            if (form.Files.Count > 0)
            {
                var file = form.Files[0];
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);    
                }

                novaEquipe.Imagem = file.FileName;
            }

            else 
            {
                novaEquipe.Imagem = "padrao.png";
            }
            c.Equipe.Add(novaEquipe); // Adiciona em Equipe
            c.SaveChanges(); // Salva as mudanças

            return LocalRedirect("~/Equipe/Listar"); // Chama o metodo listar
        }

        // ***METODO EXCLUIR*** //
        [Route("Excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            Equipe e = c.Equipe.First(e => e.IdEquipe == id);
            c.Equipe.Remove(e);
            c.SaveChanges();
            return LocalRedirect("~/Equipe/Listar");
        }

        //  ***METODO EDITAR***  //
        [Route("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            Equipe e = c.Equipe.First(e => e.IdEquipe == id);
            ViewBag.Equipe = e;
            return View("Edit");
        }

        // ***METODO ATUALIZAR*** //
        [Route("Atualizar")]
        public IActionResult Atualizar(IFormCollection form, Equipe e)
        {
            Equipe novaEquipe = new Equipe();
            novaEquipe.Nome = e.Nome;

            // Upload da imagem na equipe nova(atualizada)
            if (form.Files.Count > 0)
            {
                var file = form.Files[0];
                var folder = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img/Equipes");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(folder, file.FileName);

                using(var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                novaEquipe.Imagem = file.FileName;
            }
            else 
            {
                novaEquipe.Imagem = "padrao.png";
            }
            
            Equipe equipe = c.Equipe.First(x => x.IdEquipe == e.IdEquipe);

            equipe.Nome = novaEquipe.Nome;
            equipe.Imagem = novaEquipe.Imagem;

            c.Equipe.Update(equipe);
            c.SaveChanges();

            return LocalRedirect("~/Equipe/Listar");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}