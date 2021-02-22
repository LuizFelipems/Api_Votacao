using Flunt.Notifications;
using System;
using Votacao.Dominio.Interfaces.Commands;

namespace Votacao.Dominio.Commands.Filme.Inputs
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

                if (Pontuacao > 4)
                    AddNotification("Pontuacao", Avisos.Pontuacao_Invalida);

                return Valid;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
