using Flunt.Notifications;
using System;
using Votacao.Dominio.Interfaces.Commands;

namespace Votacao.Dominio.Commands.Filme.Inputs
{
    public class ApagarFilmeCommand : Notifiable, ICommandPadrao
    {
        public Guid Id { get; set; }

        public bool ValidarCommand()
        {
            try
            {
                if (Id == Guid.Empty)
                    AddNotification("Id", Avisos.Campo_obrigatorio);

                return Valid;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
