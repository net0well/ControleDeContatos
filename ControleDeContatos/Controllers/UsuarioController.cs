using System;
using System.Collections.Generic;
using ControleDeContatos.Filters;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    [PaginaRestritaSomenteAdmin]
    public class UsuarioController : Controller
    {
        
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        
        // GET
        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
            return View(usuarios);
        }
        
        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
            
        }
        
        public IActionResult Atualizar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            
            return View(usuario);
        }
        
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = 
                        "Usuário apagado com sucesso.";
                }
                else
                {
                    TempData["MensagemErro"] =
                        $"Ops, não conseguimos apagar o usuário, tente novamente.";
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] =
                    $"Ops, não conseguimos apagar o usuário, tente novamente. Erro: {e.Message}";
                return RedirectToAction("Index");
            }
        }
        
        
        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = 
                        "Contato cadastrado com sucesso.";
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] =
                    $"Ops, não conseguimos cadastrar o seu usuário, tente novamente. Erro: {e.Message}";
                return RedirectToAction("Index");
            }
            
            
        }
        
        [HttpPost]
        public IActionResult Atualizar(UsuarioSemSenhaModel usuarioSemSenhaModel)
        {
            try
            {
                UsuarioModel usuario = null;
                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        Id = usuarioSemSenhaModel.Id,
                        Nome = usuarioSemSenhaModel.Nome,
                        Login = usuarioSemSenhaModel.Login,
                        Email = usuarioSemSenhaModel.Email,
                        Perfil = usuarioSemSenhaModel.Perfil
                    };
                    
                   usuario =  _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] =
                        "Usuário alterado com sucesso.";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] =
                    $"Ops, não conseguimos atualizar o usuário, tente novamente. Erro: {e.Message}";
                return RedirectToAction("Index");
            }
            
        }
    }
}