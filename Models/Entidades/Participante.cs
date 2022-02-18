using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace outubroRosa.Models.Entidades
{
    public class Participante
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Endereco { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string DataNasc { get; set; }
        [Required]
        public string Sexo { get; set; }
        [Required]
        public string TipoProva { get; set; }        
        public bool IsElite { get; set; }
        public bool IsAtivo { get; set; }
        [Required]
        public string Camiseta { get; set; }
        [Required]
        public string Cidade { get; set; }
        public DateTime DataCad { get; set; }
        [Required]
        public int Numero { get; set; }
    }
}
