using System.ComponentModel.DataAnnotations;

namespace Projeto_Gamer_MVC.Models
{
    public class Equipe
    {
        [Key] // Chave Primaria
        public int IdEquipe { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }

        public ICollection<Jogador> Jogador {get; set;}
    }
}