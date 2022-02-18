using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace outubroRosa.Models.Home
{
    public class ParticiparVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Seu Nome, por favor.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Seu CPF, por favor.")]
        [Remote(action: "IsCpf", controller: "Home", ErrorMessage = "CPF inválido.")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "Seu Endereço, por favor.")]
        [MinLength(7, ErrorMessage = "Por favor, escreva seu endereço completo.")]
        public string Endereco { get; set; }
        [Required(ErrorMessage = "Seu Telefone, por favor.")]
        [MinLength(16,ErrorMessage ="Número de telefone inválido")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "Seu Email, por favor.")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Sua Data de Nascimento, por favor.")]
        [MinLength(10, ErrorMessage = "Data de Nascimento inválida.")]
        public string DataNasc { get; set; }
        [Required(ErrorMessage = "Seu Sexo, por favor.")]
        public string Sexo { get; set; }
        [Required(ErrorMessage = "Escolha o tipo de prova, por favor.")]
        public string TipoProva { get; set; }        
        public bool IsElite { get; set; }
        [Required(ErrorMessage = "Escolha o tamanho da camiseta, por favor.")]
        public string Camiseta { get; set; }
        [Required(ErrorMessage = "Sua Cidade, por favor.")]
        public string Cidade { get; set; }
    }
}
