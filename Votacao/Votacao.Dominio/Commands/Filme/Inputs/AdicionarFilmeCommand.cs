using Flunt.Notifications;
using System;
using System.Collections.Generic;
using Votacao.Dominio.Interfaces.Commands;

namespace Votacao.Dominio.Commands.Filme.Inputs
{
    public class AdicionarFilmeCommand : Notifiable, ICommandPadrao
    {
        public string Nome { get; set; }
        public string Diretor { get; set; }
        public string Genero { get; set; }
        public List<string> Atores { get; set; }

        public bool ValidarCommand()
        {
            try
            {
                if (string.IsNullOrEmpty(Nome))
                    AddNotification("Nome", Avisos.Campo_obrigatorio);
                else if (Nome.Length > 50)
                    AddNotification("Nome", Avisos.Campo_maior_que_o_esperado);

                if (string.IsNullOrEmpty(Diretor))
                    AddNotification("Diretor", Avisos.Campo_obrigatorio);
                else if (Diretor.Length > 50)
                    AddNotification("Diretor", Avisos.Campo_maior_que_o_esperado);

                if (string.IsNullOrEmpty(Genero))
                    AddNotification("Genero", Avisos.Campo_obrigatorio);
                else if (Genero.Length > 50)
                    AddNotification("Genero", Avisos.Campo_maior_que_o_esperado);

                if (Atores.Count <= 0)
                    AddNotification("Atores", Avisos.Campo_obrigatorio);

                return Valid;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
