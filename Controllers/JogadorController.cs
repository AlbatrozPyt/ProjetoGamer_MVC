using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Projeto_Gamer_MVC.Infra;
using Projeto_Gamer_MVC.Models;

namespace Projeto_Gamer_MVC.Controllers
{
    [Route("[controller]")]
    public class JogadorController : Controller
    {
        private readonly ILogger<JogadorController> _logger;

        public JogadorController(ILogger<JogadorController> logger)
        {
            _logger = logger;
        }
    
        Context c = new Context(); // Instancia do objeto

        // ***METODO LISTAR*** //
        [Route("Listar")]
        public IActionResult Index()
        {
            ViewBag.Jogador = c.Jogador.ToList(); // Guarda a Lista de Jogadores
            ViewBag.Equipe = c.Equipe.ToList(); // Guarda a Lista de Equipes
            return View(); // Retorna a View Jogadores
        }

        // ***METODO CADASTRAR*** //
        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            Jogador novoJogador = new Jogador();
            novoJogador.Nome = form["Nome"].ToString();
            novoJogador.Email = form["Email"].ToString();
            novoJogador.Senha = form["Senha"].ToString();
            novoJogador.Equipe.Nome = form["Equipe"].ToString();
            c.Jogador.Add(novoJogador);
            c.SaveChanges();
            
            return LocalRedirect("~/Jogador/Listar");

        }

        // ***METODO EXCLUIR*** //
        [Route("Excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            Jogador j = c.Jogador.First(j => j.IdJogador == id);
            c.Jogador.Remove(j);
            c.SaveChanges();
            return LocalRedirect("~/Jogador/Listar");
        }

        // ***METODO EDITAR*** //
        [Route("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            Jogador j = c.Jogador.First(j => j.IdJogador == id);
            ViewBag.Jogador = j;
            return View("Edit");
        }

        // ***METODO ATUALIZAR*** //
        [Route("Atualizar")]
        public IActionResult Atualizar(IFormCollection form, Jogador j)
        {
            Jogador novoJogador = new Jogador();
            novoJogador.Nome = j.Nome;
            novoJogador.Email = j.Email;
            novoJogador.Senha = j.Senha;

            Jogador jogador = c.Jogador.First(x => x.IdJogador == j.IdJogador);
            jogador.Nome = novoJogador.Nome;
            jogador.Email = novoJogador.Email;
            jogador.Senha = novoJogador.Senha;

            c.Jogador.Update(jogador);
            c.SaveChanges();

            return LocalRedirect("~/Jogador/Atualizar");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}