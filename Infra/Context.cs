using Microsoft.EntityFrameworkCore;
using Projeto_Gamer_MVC.Models;

namespace Projeto_Gamer_MVC.Infra
{
    public class Context : DbContext
    {
        // Conexao do projeto com o banco de dados

        public Context(){}

        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
              optionsBuilder.UseSqlServer("Data Source = NOTE07-S15; initisl catalog = projetoGamer; Integrated Security = true; TrustServerCertificate = true");  
            }
        }

        public DbSet<Jogador> Jogador {get; set;}
        public DbSet<Equipe> Equipe {get; set;}
    }
}