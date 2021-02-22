using Flunt.Notifications;
using System;
using Votacao.Dominio.Interfaces.Commands;

namespace Votacao.Dominio.Commands.Usuario.Inputs
{
    public class AtualizarUsuarioCommand : Notifiable, ICommandPadrao
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }

        public bool ValidarCommand()
        {
            try
            {
                if (Id == Guid.Empty)
                    AddNotification("Id", Avisos.Campo_obrigatorio);

                if (string.IsNullOrEmpty(Nome))
                    AddNotification("Nome", Avisos.Campo_obrigatorio);
                else if (Nome.Length > 50)
                    AddNotification("Nome", Avisos.Campo_maior_que_o_esperado);

                if (string.IsNullOrEmpty(Login))
                    AddNotification("Login", Avisos.Campo_obrigatorio);
                else if (Login.Length > 50)
                    AddNotification("Login", Avisos.Campo_maior_que_o_esperado);

                if (string.IsNullOrEmpty(Senha))
                    AddNotification("Senha", Avisos.Campo_obrigatorio);
                else if (!(Senha.Length >= 3 && Senha.Length <= 6))
                    AddNotification("Senha", Avisos.Campo_maior_que_o_esperado);

                if (string.IsNullOrEmpty(Role))
                    AddNotification("Role", Avisos.Campo_obrigatorio);

                return Valid;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
