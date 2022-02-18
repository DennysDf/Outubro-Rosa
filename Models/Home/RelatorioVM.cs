using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace outubroRosa.Models.Home
{
    public class RelatorioVM
    {
        public int Id { get; set; }
        public int Num { get; set; }
        public string Nome { get; set; }
        public string TipoProva { get; set; }
        public string Camiseta { get; set; }
        public string Telefone { get; set; }
        public string Elite { get; set; }
        public string DataNasc { get; set; }

        public bool Homem { get; set; }
        public bool Mulher { get; set; }

        public bool Caminhada { get; set; }
        public bool Corrida { get; set; }
        public bool IsElite { get; set; }
        public bool IsAmador { get; set; }
        public string Email { get; internal set; }
        public string CPF { get; internal set; }
        public string Sexo { get; internal set; }
    }
}
