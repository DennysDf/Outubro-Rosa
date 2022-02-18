using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using outubroRosa.Models.Context;
using outubroRosa.Models.Entidades;
using outubroRosa.Models.Home;
using System.Net;
using Newtonsoft.Json;
using outubroRosa.Service.Interface;
using System.Threading;

namespace outubroRosa.Controllers
{
    public class HomeController : Controller
    {
        private readonly BDRosaContext _context;
        private readonly INotificacao _notify;

        public HomeController(BDRosaContext context, INotificacao notify)
        {
            _context = context;
            _notify = notify;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        [Route("Outubro-Rosa")]
        public IActionResult Outubro(int id = 0)
        {
            ViewData["Ativo"] = id;
            return View();
        }

        public IActionResult Cadastrar(ParticiparVM model)
        {

            if (!(_context.Participantes.Any(c => c.CPF.Equals(model.CPF))))
            {
                int num;

                model.IsElite = model.TipoProva.Equals("1") ? model.IsElite : false;

                var a = new List<int>();

                var aaa = _context.Participantes.Where(c => c.IsElite == model.IsElite).Select(c => c.Numero).ToList();

                var a1 = _context.Participantes.Where(c => c.IsElite == model.IsElite).Select(c => new { c.Numero }).OrderByDescending(c => c.Numero).First().Numero;

                var qtd = model.IsElite ? 1 : 100;

                for (int i = qtd; i <= a1; i++)
                {
                    if (!(aaa.Contains(i)))
                    {
                        a.Add(i);
                    }
                }



                num = a.Any() ? a.First() : a1 + 1;


                var part = new Participante() { Camiseta = model.Camiseta, Cidade = model.Cidade, CPF = model.CPF, DataNasc = model.DataNasc, Email = model.Email, Endereco = model.Endereco, IsElite = model.TipoProva.Equals("1") ? model.IsElite : false, Nome = model.Nome, Sexo = model.Sexo, Telefone = model.Telefone, TipoProva = model.TipoProva, Numero = num };
                _context.Add(part);
                _context.SaveChanges();
                TempData["Feito"] = part.Numero;
                Email(part.Id);
            }

            return RedirectToAction("Outubro");
        }

        public async void Email(int id)
        {
            var part = _context.Participantes.First(c => c.Id == id);

            var smtp = new SmtpClient("mail.devroyale.com", 8889);
            smtp.EnableSsl = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("postmaster@devroyale.com", "Deusemais@100");
            var msg = new MailMessage();
            msg.To.Add(part.Email);
            msg.From = new MailAddress("postmaster@devroyale.com", "Corrida Outubro Rosa");

            msg.Subject = "Confirmação de cadastro para corrida.";

            var sexo = part.Sexo == "F" ? "Feminino" : "Masculino";
            var tipo = part.TipoProva == "1" ? "Corrida - 5KM" : "Caminhada - 3KM";
            var isElite = part.IsElite ? "Sim" : "Não";

            msg.Body = "<!DOCTYPE html> <html lang=\"pt - br\"> <head> <meta charset=\"UTF - 8\"> <meta name=\"viewport\" content=\"width = device - width, initial - scale = 1.0\"> <meta http-equiv=\"X - UA - Compatible\" content=\"ie = edge\"> <title> tabelas</title> <style> div{ font-family: arial, sans-serif; font-size: 15px; } table{ border: 1px solid gray ;width: 100%;font-family: arial, sans-serif; font-size: 15px; } #tdNumero{ border-bottom: solid 1px gray; } span{ font-display: arial, sans-serif ; color: black; font-size: 25px; } .bordar-direita{ width: 50%; border-right: solid 1px gray; } .prova{ border-top: solid 1px gray;border-bottom: solid 1px gray; padding-top: 10px;padding-bottom: 10px; } .camiseta{ border-top: solid 1px gray;text-align: left; } a{ text-decoration: none; font-family: arial, sans-serif; color: blue; } </style> </head> <body> <div style=\"font - family:arial, sans - serif; font - size:25px; \" > <b>" + part.Nome + "</b>, agora você é um(a) participante da <b> Corrida Outubro Rosa de Alto Horizonte</b>, o evento acontecerá no Clube Municipal da cidade no dia 27 de Outubro a partir das 6 horas da manhã, muito obrigado por apoiar essa causa. <br></div> <div style=\"font - family:arial, sans - serif; font - size:25px; \" >Abaixo estão seus dados que foram informados no momento do cadastro. O número em destaque será usado como sua identificação na corrida ou caminhada, com este mesmo número você irá receber seu Kit Corrida, o local de retirada é no <a href=\"https://www.google.com/maps/place/14%C2%B011'41.9%22S+49%C2%B020'19.1%22W/@-14.1949567,-49.3391812,19z/data=!3m1!4b1!4m6!3m5!1s0x0:0x0!7e2!8m2!3d-14.1949585!4d-49.3386337\"> ESF 1</a>, nos dias 25 e 26 de Outubro das 7h às 17h, somente o participante poderá retirar seu Kit, leve sua ficha de matrícula impressa e 1Kg de alimento não perecível <small>(exceto: sal, fubá e farinhas em geral)</small>. </div> <a href=\"https://devroyale.com/Imprimir?cpf=" + part.CPF + "&id=" + part.Id + " \" target=\"_blank\" style=\"text-decoration:none;font-family:arial, sans-serif;color:blue;\" >Clique aqui para imprimir seu comprovante de Matrícula . </a> <table style=\"border - width:1px; border - style:solid; border - color:gray; width: 100 %; font - family:arial, sans - serif; font - size:25px; \" > <td id=\"tdNumero\" colspan=\"6\" style=\"border - bottom - width:1px; border - bottom - style:solid; border - bottom - color:gray; \" > <span style=\"font - display:arial, sans - serif; color: black; font - size:80px; \" ><b>N°</b> " + part.Numero + "</span> </td> <tr> <td class=\"bordar - direita\" style=\"width: 50 %; border - right - width:1px; border - right - style:solid; border - right - color:gray; \" > <b> NOME:</b> " + part.Nome + " </td> <td style=\"width: 50 %; \" > <b> CPF:</b> " + part.CPF + " </td> </tr> <tr> <td class=\"bordar - direita\" style=\"width: 50 %; border - right - width:1px; border - right - style:solid; border - right - color:gray; \" > <b> ENDEREÇO COMPLETO: </b> " + part.Endereco + "</td> <td style=\"width: 50 %; \" > <b> EMAIL:</b> " + part.Email + "</td> </tr> <tr> <td class=\"bordar - direita\" style=\"width: 50 %; border - right - width:1px; border - right - style:solid; border - right - color:gray; \" > <b> TELEFONE:</b> " + part.Telefone + "</td> <td style=\"width: 50 %; \" > <b> CIDADE:</b> " + part.Cidade + " </td> </tr> <tr> <td class=\"bordar - direita\" style=\"width: 50 %; border - right - width:1px; border - right - style:solid; border - right - color:gray; \" > <b> DATA NASCIMENTO:</b> " + part.DataNasc + "</td> <td style=\"width: 50 %; \" > <b> SEXO:</b> " + sexo + " </td> </tr> <th class=\"prova\" colspan=\"2\" style=\"border - top - width:1px; border - top - style:solid; border - top - color:gray; border - bottom - width:1px; border - bottom - style:solid; border - bottom - color:gray; padding - top:10px; padding - bottom:10px; \" >INFORMAÇÕES DA CORRIDA</th> <tr> <td class=\"bordar - direita\" style=\"width: 50 %; border - right - width:1px; border - right - style:solid; border - right - color:gray; \" > <b> TIPO DE PROVA:</b> " + tipo + " </td> <td style=\"width: 50 %; \" > <b> ELITE?</b> " + isElite + "</td> </tr><tr> <td class=\"camiseta\" colspan=\"2\" style=\"border - top - width:1px; border - top - style:solid; border - top - color:gray; text - align:left; \" > <b> TAMANHO CAMISETA: </b> " + part.Camiseta + "</td></tr> </table><a href='https://devroyale.com/Cancelar?id=" + part.Id + "' >Caso exista alguma informação incorreta no cadastro clique aqui para cancelar e efetua-lo novamente.</a> <br><br> <b>Atenciosamente</b> <br>- Dennys Fonseca de Souza <br>Programador Web  <br>+5562981479264  </body> </html>";

            msg.IsBodyHtml = true;
            smtp.Send(msg);
        }

        public IActionResult EmailSend(int id)
        {
            Email(id);
            TempData["Email"] = id;
            return RedirectToAction("Outubro");
        }

        public bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        public string VerificaCPF(string cpf)
        {
            var Id = 0;

            return _context.Participantes.Any(c => c.CPF.Equals(cpf)) ? JsonConvert.SerializeObject(_context.Participantes.FirstOrDefault(c => c.CPF.Equals(cpf))) : JsonConvert.SerializeObject(Id);
        }

        [Route("Imprimir")]
        public IActionResult Imprimir(string cpf = "", int id = 0)
        {
            var part = _context.Participantes.Where(c => c.CPF.Equals(cpf) && c.Id == id).First();
            return View(part);
        }

        public string Informacoes(int id)
        {
            var part = _context.Participantes.Where(c => c.Id == id)
                .Select(c => new
                {
                    c.Id,
                    c.Camiseta,
                    Sexo = c.Sexo.Equals("M") ? "Masculino" : "Feminino",
                    Elite = c.IsElite ? "Sim" : "Não",
                    Tipo = c.TipoProva.Equals("1") ? "Corrida - 5KM" : "Caminhada - 3KM",
                    DataHora = $"{c.DataCad.ToShortDateString().ToDateFull()} às {c.DataCad.AddHours(1).ToShortTimeString()}",
                    c.Nome,
                    c.CPF,
                    c.DataNasc,
                    c.Email,
                    c.Endereco,
                    c.Cidade,
                    c.Telefone,
                    c.Numero
                })
                .First();

            return JsonConvert.SerializeObject(part);

        }

        public IActionResult Info(string cpf = "", int id = 0)
        {
            var part = _context.Participantes.First();


            return View(part);
        }

        [Route("Cancelar")]
        public IActionResult Cancelar(int id)
        {
            _context.Remove(_context.Participantes.FirstOrDefault(c => c.Id == id));
            _context.SaveChanges();
            _notify.Mensagem();

            return RedirectToAction("Outubro");
        }
        
        public bool SendEmails(int id)
        {

            var emails = _context.Participantes.Where(c => c.Id == id).First();


            var smtp = new SmtpClient("mail.devroyale.com", 8889);
            smtp.EnableSsl = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("postmaster@devroyale.com", "Deusemais@100");
            var msg = new MailMessage();
            msg.To.Add(emails.Email);
            msg.From = new MailAddress("postmaster@devroyale.com", "Corrida Outubro Rosa");
            
            msg.Subject = "Novo local para retirar seu Kit Corrida";



            msg.Body = "<!DOCTYPE html> <html lang=\"pt - br\"> <head> <meta charset=\"UTF - 8\"> <meta name=\"viewport\" content=\"width = device - width, initial - scale = 1.0\"> <meta http-equiv=\"X - UA - Compatible\" content=\"ie = edge\"> <title>FORM</title> <style> div{ font-size: 25px; font-family: Arial, Helvetica, sans-serif; } </style> </head> <body> <div style=\"font - size:25px; font - family:Arial, Helvetica, sans - serif; \" > Olá "+ emails.Nome +", a organização do evento <b>Corrida Outubro Rosa de Alto Horizonte</b> vem até você lhe informar que o local de entrega foi redefinido para o <a href=\"https://www.google.com/maps/place/14%C2%B011'41.9%22S+49%C2%B020'19.1%22W/@-14.1949567,-49.3391812,19z/data=!3m1!4b1!4m6!3m5!1s0x0:0x0!7e2!8m2!3d-14.1949585!4d-49.3386337\"> ESF 1</a>, nos dias 25 e 26 de Outubro das 7h às 17h, somente o participante poderá retirar seu Kit, leve sua ficha de matrícula impressa e 1Kg de alimento não perecível <small>(exceto: sal, fubá e farinhas em geral)</small>. </div> <br> <br> <br> <br><small> <b>Atenciosamente</b> <br> - Dennys Fonseca de Souza <br> Programador Web <br >+5562981479264 <small></body>";

            msg.IsBodyHtml = true;
            var aaa = false;
            try
            {
                smtp.Send(msg);
                aaa = true;
            }
            catch (Exception)
            {
                aaa = false;
            }

            return aaa;
        }

        public bool sadasd(int id)
        {

            var emails = _context.Participantes.Where(c => c.Id == id).First();

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("outubro.rosa.ah@gmail.com", "Deusemais100");

            var msg = new MailMessage();
            msg.To.Add(emails.Email);
            msg.From = new MailAddress("postmaster@devroyale.com", "Corrida Outubro Rosa");

            msg.Subject = "Novo local para retirar seu Kit Corrida";



            msg.Body = "<!DOCTYPE html> <html lang=\"pt - br\"> <head> <meta charset=\"UTF - 8\"> <meta name=\"viewport\" content=\"width = device - width, initial - scale = 1.0\"> <meta http-equiv=\"X - UA - Compatible\" content=\"ie = edge\"> <title>FORM</title> <style> div{ font-size: 25px; font-family: Arial, Helvetica, sans-serif; } </style> </head> <body> <div style=\"font - size:25px; font - family:Arial, Helvetica, sans - serif; \" > Olá " + emails.Nome + ", a organização do evento <b>Corrida Outubro Rosa de Alto Horizonte</b> vem até você lhe informar que o local de entrega foi redefinido para o <a href=\"https://www.google.com/maps/place/14%C2%B011'41.9%22S+49%C2%B020'19.1%22W/@-14.1949567,-49.3391812,19z/data=!3m1!4b1!4m6!3m5!1s0x0:0x0!7e2!8m2!3d-14.1949585!4d-49.3386337\"> ESF 1</a>, nos dias 25 e 26 de Outubro das 7h às 17h, somente o participante poderá retirar seu Kit, leve sua ficha de matrícula impressa e 1Kg de alimento não perecível <small>(exceto: sal, fubá e farinhas em geral)</small>. </div> <br> <br> <br> <br><small> <b>Atenciosamente</b> <br> - Dennys Fonseca de Souza <br> Programador Web <br >+5562981479264 <small></body>";

            msg.IsBodyHtml = true;
            var aaa = false;
            try
            {
                client.Send(msg);
                aaa = true;
            }
            catch (Exception)
            {
                aaa = false;
            }

            return aaa;            
        }
    }
}
