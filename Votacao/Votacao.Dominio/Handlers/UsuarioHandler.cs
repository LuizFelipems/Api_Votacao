using Flunt.Notifications;
using System;
using System.Threading.Tasks;
using Votacao.Dominio.Autenticacao;
using Votacao.Dominio.Commands.Usuario.Inputs;
using Votacao.Dominio.Commands.Usuario.Outputs;
using Votacao.Dominio.Entidades;
using Votacao.Dominio.Interfaces.Commands;
using Votacao.Dominio.Interfaces.Repositories;

namespace Votacao.Dominio.Handlers
{
    public class UsuarioHandler : Notifiable, ICommandHandler<AdicionarUsuarioCommand>,
                                                ICommandHandler<AtualizarUsuarioCommand>,
                                                ICommandHandler<ApagarUsuarioCommand>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly TokenService _tokenService;

        public UsuarioHandler(IUsuarioRepository usuarioRepository,
                                TokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        public async Task<ICommandResult> HandlerAsync(AdicionarUsuarioCommand command)
        {
            try
            {
                if (!command.ValidarCommand())
                    return new UsuarioCommandResult(false, Avisos.Por_favor_corrija_as_inconsistências_abaixo, command.Notifications);

                if (await _usuarioRepository.CheckLoginAsync(command.Login))
                    AddNotification("Login", Avisos.Login_ja_existente_Por_favor_tente_um_login_diferente);

                if (Invalid)
                    return new UsuarioCommandResult(false, Avisos.Por_favor_corrija_as_inconsistências_abaixo, Notifications);

                Usuario usuario = new Usuario(command.Nome, command.Login, command.Senha, command.Role);

                await _usuarioRepository.InserirAsync(usuario);

                return new UsuarioCommandResult(true, Avisos.Usuario_Gravado_com_sucesso,
                    new { usuario.Id, usuario.Nome, usuario.Login, Role = usuario.Role, Senha = "******" });
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<ICommandResult> HandlerAsync(AtualizarUsuarioCommand command)
        {
            try
            {
                if (!command.ValidarCommand())
                    return new UsuarioCommandResult(false, Avisos.Por_favor_corrija_as_inconsistências_abaixo, command.Notifications);

                if (! await _usuarioRepository.CheckIdAsync(command.Id))
                    AddNotification("Id", Avisos.Id_invalido_Este_Id_nao_esta_cadastrado);

                if (Invalid)
                    return new UsuarioCommandResult(false, Avisos.Por_favor_corrija_as_inconsistências_abaixo, Notifications);

                Usuario usuario = new Usuario(command.Id, command.Nome, command.Login, command.Senha, command.Role);

                await _usuarioRepository.AlterarAsync(usuario);

                return new UsuarioCommandResult(true, Avisos.Usuario_Atualizado_com_sucesso,
                    new { usuario.Id, usuario.Nome, usuario.Login, Role = usuario.Role, Senha = "******" });
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<ICommandResult> HandlerAsync(ApagarUsuarioCommand command)
        {
            try
            {
                if (!command.ValidarCommand())
                    return new UsuarioCommandResult(false, Avisos.Por_favor_corrija_as_inconsistências_abaixo, command.Notifications);

                if (! await _usuarioRepository.CheckIdAsync(command.Id))
                    AddNotification("Id", Avisos.Id_invalido_Este_Id_nao_esta_cadastrado);

                if (Invalid)
                    return new UsuarioCommandResult(false, Avisos.Por_favor_corrija_as_inconsistências_abaixo, Notifications);

                await _usuarioRepository.DeletarAsync(command.Id);

                return new UsuarioCommandResult(true, Avisos.Usuario_Apagado_com_sucesso,
                    new { command.Id });
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<ICommandResult> HandlerAsync(AutenticarUsuarioCommand command)
        {
            try
            {
                if (!command.ValidarCommand())
                    return new UsuarioCommandResult(false, Avisos.Por_favor_corrija_as_inconsistências_abaixo, command.Notifications);

                if (! await _usuarioRepository.CheckAutenticacaoAsync(command.Login, command.Senha))
                    AddNotification("Autenticação", Avisos.Login_ou_Senha_invalidos);

                if (Invalid)
                    return new UsuarioCommandResult(false, Avisos.Por_favor_corrija_as_inconsistências_abaixo, Notifications);

                var usuarioResult = await _usuarioRepository.ObterPorLoginAsync(command.Login);

                var token = _tokenService.GenerateToken(usuarioResult.Login, usuarioResult.Role);

                return new UsuarioCommandResult(true, Avisos.Usuario_Autenticado_com_sucesso,
                    new { usuarioResult.Login, usuarioResult.Role, Token = token });
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
