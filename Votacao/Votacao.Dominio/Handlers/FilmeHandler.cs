using Flunt.Notifications;
using System;
using System.Threading.Tasks;
using Votacao.Dominio.Commands.Filme.Inputs;
using Votacao.Dominio.Commands.Filme.Outputs;
using Votacao.Dominio.Entidades;
using Votacao.Dominio.Interfaces.Commands;
using Votacao.Dominio.Interfaces.Repositories;

namespace Votacao.Dominio.Handlers
{
    public class FilmeHandler : Notifiable, ICommandHandler<AdicionarFilmeCommand>,
                                                ICommandHandler<AtualizarFilmeCommand>,
                                                ICommandHandler<ApagarFilmeCommand>
    {
        private readonly IFilmeRepository _filmeRepository;

        public FilmeHandler(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public async Task<ICommandResult> HandlerAsync(AdicionarFilmeCommand command)
        {
            try
            {
                if (!command.ValidarCommand())
                    return new FilmeCommandResult(false, Avisos.Por_favor_corrija_as_inconsistências_abaixo, command.Notifications);

                Filme filme = new Filme(command.Nome, command.Diretor, command.Genero, command.Atores);

                await _filmeRepository.InserirAsync(filme);

                return new FilmeCommandResult(true, Avisos.Filme_Gravado_com_sucesso,
                    new { filme.Id, filme.Nome, filme.Diretor, filme.Atores });
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<ICommandResult> HandlerAsync(AtualizarFilmeCommand command)
        {
            try
            {
                if (!command.ValidarCommand())
                    return new FilmeCommandResult(false, Avisos.Por_favor_corrija_as_inconsistências_abaixo, command.Notifications);

                if (! await _filmeRepository.CheckIdAsync(command.Id))
                    AddNotification("Id", Avisos.Id_invalido_Este_Id_nao_esta_cadastrado);

                if (Invalid)
                    return new FilmeCommandResult(false, Avisos.Por_favor_corrija_as_inconsistências_abaixo, Notifications);

                Filme filme = new Filme(command.Id, command.Nome, command.Diretor, command.Genero, command.Atores);

                await _filmeRepository.AlterarAsync(filme);

                return new FilmeCommandResult(true, Avisos.Filme_Atualizado_com_sucesso,
                    new { filme.Id, filme.Nome, filme.Diretor, filme.Atores });
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<ICommandResult> HandlerAsync(ApagarFilmeCommand command)
        {
            try
            {
                if (!command.ValidarCommand())
                    return new FilmeCommandResult(false, Avisos.Por_favor_corrija_as_inconsistências_abaixo, command.Notifications);

                if (! await _filmeRepository.CheckIdAsync(command.Id))
                    AddNotification("Id", Avisos.Id_invalido_Este_Id_nao_esta_cadastrado);

                if (Invalid)
                    return new FilmeCommandResult(false, Avisos.Por_favor_corrija_as_inconsistências_abaixo, Notifications);

                await _filmeRepository.DeletarAsync(command.Id);

                return new FilmeCommandResult(true, Avisos.Filme_Apagado_com_sucesso,
                    new { command.Id });
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
