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
                // "Data Source = NOTE07-S15; initial catalog = projetoGamer-tarde; User Id=sa; pwd=Senai@134; TrustServerCertificate = true"
              optionsBuilder.UseSqlServer("Data Source = NOTE07-S15; initial catalog = projetoGamer-tarde; User Id=sa; pwd=Senai@134; TrustServerCertificate = true");  
            }
        }

        public DbSet<Jogador> Jogador {get; set;}
        public DbSet<Equipe> Equipe {get; set;}
    }
}