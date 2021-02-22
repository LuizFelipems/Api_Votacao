using Votacao.Dominio.Interfaces.Commands;

namespace Votacao.Dominio.Commands.Usuario.Outputs
{
    public class UsuarioCommandResult : ICommandResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public UsuarioCommandResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
