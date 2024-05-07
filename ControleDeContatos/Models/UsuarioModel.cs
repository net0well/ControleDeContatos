using System;
using System.ComponentModel.DataAnnotations;
using ControleDeContatos.Enums;
using ControleDeContatos.Helper;

namespace ControleDeContatos.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Favor inserir o nome do usuário")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Favor inserir o login do usuário")]
        public string Login { get; set; }
        
        [Required(ErrorMessage = "Favor inserir o e-mail do usuário")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Favor informar o Perfil do usuário.")]
        public PerfilEnum? Perfil { get; set; }
        public string Senha {get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? ModificadoEm { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
        }

        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }

        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
            Senha = novaSenha.GerarHash();
            return novaSenha;
        }
    }
}