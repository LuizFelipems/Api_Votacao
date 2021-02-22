using Votacao.Dominio.Interfaces.Commands;

namespace Votacao.Dominio.Commands.Filme.Outputs
{
    public class FilmeCommandResult : ICommandResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public FilmeCommandResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
