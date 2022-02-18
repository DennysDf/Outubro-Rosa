
using outubroRosa.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace outubroRosa.Service.Class
{
    public class Notificacao : INotificacao
    {
        public Notificacao()
        {

        }

        public void Mensagem()
        {
            Servicos.Notificacoes.Push("teste");
        }

    }
}
