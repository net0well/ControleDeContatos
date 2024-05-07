﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControleDeContatos.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ControleDeContatos.Models;

namespace ControleDeContatos.Controllers
{
    [PaginaParaUsuarioLogado]
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}