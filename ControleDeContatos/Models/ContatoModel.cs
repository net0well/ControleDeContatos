using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Favor inserir o nome do contato.")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Favor inserir o e-mail do contato.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }
        [Phone(ErrorMessage = "Celular inválido.")]
        [Required(ErrorMessage = "Favor inserir o celular do contato.")]
        public string Celular { get; set; }
    }
}