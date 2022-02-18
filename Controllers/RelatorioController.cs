using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using outubroRosa.Models.Context;
using outubroRosa.Models.Home;

namespace outubroRosa.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly BDRosaContext _context;

        public RelatorioController(BDRosaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["Qtd"] = _context.Participantes.Count();

            return View();
        }
    
        [HttpGet]
        public IActionResult Ajax(string Mulher, string Homem, string Caminhada, string Corrida, string IsElite, string IsAmador)
        {
            
            var ajax = _context.Participantes.Where(c => c.Id > 0);

            if (Mulher == null || Homem == null)
                ajax = ajax.Where(c => Mulher != null ? c.Sexo.Equals("F") : true || Homem != null ? c.Sexo.Equals("M") : true);
                


            if (Corrida == null || Caminhada == null)
                ajax = ajax.Where(c => Corrida != null ? c.TipoProva.Equals("1") : true || Caminhada != null ? c.TipoProva.Equals("2") : true);
                


            if (IsElite == null || IsAmador == null)
                ajax = ajax.Where(c => IsElite != null ? c.IsElite : true || IsAmador != null ? !c.IsElite : true);
                
            

            var ajax2 = ajax.Select(c => new RelatorioVM
            {
                Id = c.Id,
                Nome = c.Nome,
                TipoProva = c.TipoProva == "1" ? "Corrida - 5KM" : "Caminhada - 3KM",
                Camiseta = c.Camiseta,
                Telefone = c.Telefone,
                Elite = c.IsElite ? "Sim":"Não",
                Num = c.Numero,
                DataNasc = c.DataNasc,
                Email = c.Email,
                CPF = c.CPF,
                Sexo = c.Sexo.Equals("M") ? "Masculico" : "Feminino"

            })
            .OrderBy(c => c.Nome)
            .ToList();

            ViewData["Camiseta"] = ajax.Select(c => new
            {
                c.Camiseta,
                Tamanho = c.Camiseta
            })
            .GroupBy(c => new { c.Camiseta })
            .Select(c =>new
            {
                c.Key.Camiseta,
                Qtd = c.Count()
            })
            .OrderBy(c => c.Camiseta)
            .Select(c => c.Qtd)
            .ToArray();

            ViewData["Sexo"] = ajax.Select(c => new
            {
                c.Sexo,
                Qtd = c.Sexo
            })
           .GroupBy(c => new { c.Sexo })
           .Select(c => new
           {
               c.Key.Sexo,
               Qtd = c.Count()
           })
           .OrderByDescending(c => c.Sexo)
           .Select(c => c.Qtd)
           .ToArray();


            ViewData["TipoProva"] = ajax.Select(c => new
            {
                c.TipoProva,
                Qtd = c.TipoProva
            })
           .GroupBy(c => new { c.TipoProva })
           .Select(c => new
           {
               c.Key.TipoProva,
               Qtd = c.Count()
           })
           .OrderBy(c => c.TipoProva)
           .Select(c => c.Qtd)
           .ToArray();


            return View(ajax2);
        }
    }
}