using Flunt.Notifications;
using System;
using System.Linq;
using System.Threading.Tasks;
using Votacao.Dominio.Commands.Voto.Inputs;
using Votacao.Dominio.Commands.Voto.Outputs;
using Votacao.Dominio.Entidades;
using Votacao.Dominio.Interfaces.Commands;
using Votacao.Dominio.Interfaces.Repositories;

namespace Votacao.Dominio.Handlers
{
    public class VotoHandler : Notifiable, ICommandHandler<VotarCommand>
    {
        private readonly IVotoRepository _votoRepository;
        private readonly IFilmeRepository _filmeRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public VotoHandler(IVotoRepository votoRepository, IFilmeRepository filmeRepository, IUsuarioRepository usuarioRepository)
        {
            _votoRepository = votoRepository;
            _filmeRepository = filmeRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<ICommandResult> HandlerAsync(VotarCommand command)
        {
            try
            {
                if (!command.ValidarCommand())
                    return new VotarCommandResult(false, Avisos.Por_favor_corrija_as_inconsistências_abaixo, command.Notifications);

                if (! await _usuarioRepository.CheckIdAsync(command.IdUsuario))
                    AddNotification("IdUsuario", Avisos.Id_invalido_Este_Id_nao_esta_cadastrado);

                if (! await _filmeRepository.CheckIdAsync(command.IdFilme))
                    AddNotification("IdFilme", Avisos.Id_invalido_Este_Id_nao_esta_cadastrado);

                if (await _votoRepository.CheckUsuarioVotouAsync(command.IdUsuario))
                    AddNotification("IdUsuario", Avisos.Esse_usuario_ja_votou);

                if (Invalid)
                    return new VotarCommandResult(false, Avisos.Por_favor_corrija_as_inconsistências_abaixo, Notifications);

                Voto voto = new Voto(command.IdUsuario, command.IdFilme, command.Pontuacao);

                await _votoRepository.InserirAsync(voto);

                return new VotarCommandResult(true, Avisos.Voto_realizado_com_sucesso, 
                    new { voto.Id, voto.IdFilme, voto.IdUsuario, voto.Pontuacao });
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
