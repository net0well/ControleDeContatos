using System;
using System.ComponentModel.DataAnnotations;
using ControleDeContatos.Enums;

namespace ControleDeContatos.Models
{
    public class UsuarioSemSenhaModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Favor inserir o Nome do usuário.")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Favor inserir o Login do usuário.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Favor inserir o e-mail do usuário.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Favor informar o Perfil do usuário.")]
        public PerfilEnum? Perfil { get; set; }
        
       
    }
}