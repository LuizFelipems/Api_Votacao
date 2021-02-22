using Flunt.Notifications;
using System;
using Votacao.Dominio.Interfaces.Commands;

namespace Votacao.Dominio.Commands.Voto.Inputs
{
    public class VotarCommand : Notifiable, ICommandPadrao
    {
        public Guid IdUsuario { get; set; }
        public Guid IdFilme { get; set; }
        public int Pontuacao { get; set; }

        public bool ValidarCommand()
        {
            try
            {
                if (IdUsuario == Guid.Empty)
                    AddNotification("IdUsuario", Avisos.Campo_obrigatorio);

                if (IdFilme == Guid.Empty)
                    AddNotification("IdFilme", Avisos.Campo_obrigatorio);

                return Valid;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
