using System.Threading.Tasks;

namespace Votacao.Dominio.Interfaces.Commands
{
    public interface ICommandHandler<T>
        where T : ICommandPadrao
    {
        Task<ICommandResult> HandlerAsync(T command);
    }
}
