using System;
using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _Sessao;
        private readonly IEmail _Email;
        
        
        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _Sessao = sessao;
            _Email = email;

        }
        // GET
        public IActionResult Index()
        {
            // Se o usuário estiver logado vai redirecionar para home

            if (_Sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");
            
            return View();
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        public IActionResult Sair()
        {
            _Sessao.RemoverSessaoDoUsuario();

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);
                    
                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            _Sessao.CriarSessaoDoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] =
                            $"Senha inválida, tente novamente.";
                    }
                    TempData["MensagemErro"] =
                        $"Usuario/Senha inválido(s), tente novamente.";
                }

                return View("Index");
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] =
                    $"Ops, não conseguimos realizar o seu login, tente novamente. Erro: {e.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario =
                        _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);
                    
                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                  
                        string menssagem = $"Sua nova senha é: {novaSenha}";

                        bool emailEnviado = _Email.Enviar(usuario.Email, "Sistema de Contato - Nova Senha", menssagem);

                            if (emailEnviado)
                            {
                                _usuarioRepositorio.Atualizar(usuario);
                                
                                TempData["MensagemSucesso"] =
                                    $"Enviamos para seu e-mail cadastrado uma nova senha.";
                                
                            }
                            else
                            {
                                TempData["MensagemErro"] =
                                    $"Não foi possível enviar o e-mail, por favor, tente novamente.";
                            }
                        
                        return RedirectToAction("Index", "Login");
                    }
                    TempData["MensagemErro"] =
                        $"Não foi possível redefinir sua senha, por favor, verifique os dados informados.";
                }

                return View("Index");
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] =
                    $"Ops, não conseguimos redefinir sua senha, tente novamente. Erro: {e.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}