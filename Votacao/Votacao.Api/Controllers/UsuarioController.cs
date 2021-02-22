using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Votacao.Dominio.Commands.Usuario.Inputs;
using Votacao.Dominio.Handlers;
using Votacao.Dominio.Interfaces.Commands;
using Votacao.Dominio.Interfaces.Repositories;
using Votacao.Dominio.Queries;

namespace Votacao.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly UsuarioHandler _handler;

        public UsuarioController(IUsuarioRepository usuarioRepository, UsuarioHandler handler)
        {
            _usuarioRepository = usuarioRepository;
            _handler = handler;
        }

        [HttpPost]
        [Route("v1/login")]
        [AllowAnonymous]
        public async Task<ICommandResult> LoginAsync([FromBody] AutenticarUsuarioCommand command)
        {
            return await _handler.HandlerAsync(command);
        }

        /// <summary>
        /// Usuarios
        /// </summary>                
        /// <remarks><h2><b>Lista todos os Usuarios não administradores ativos.</b></h2></remarks>
        /// <response code="200">OK Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/usuarios")]
        [Authorize(Roles = "Visitante,Administrador")]
        public async Task<IEnumerable<UsuarioQueryResult>> UsuariosAsync()
        {
            return await _usuarioRepository.ListarAsync();
        }

        /// <summary>
        /// Usuarios
        /// </summary>                
        /// <remarks><h2><b>Consulta o Usuario.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido id do Usuario</param>
        /// <response code="200">OK Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/usuarios/{id}")]
        [Authorize(Roles = "Visitante,Administrador")]
        public async Task<UsuarioQueryResult> UsuarioAsync(Guid id)
        {
            return await _usuarioRepository.ObterPorIdAsync(id);
        }

        /// <summary>
        /// Incluir Usuario 
        /// </summary>                
        /// <remarks><h2><b>Incluir novo Usuario. Roles: Visitante, Administrador</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Insert</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("v1/usuarios")]
        [AllowAnonymous]
        public async Task<ICommandResult> UsuarioInserirAsync([FromBody] AdicionarUsuarioCommand command)
        {
            return await _handler.HandlerAsync(command);
        }

        /// <summary>
        /// Alterar Usuario
        /// </summary>        
        /// <remarks><h2><b>Alterar Usuario. Roles: Visitante, Administrador</b></h2></remarks>
        /// <param name="id">Parâmetro requerido id do Usuario</param>        
        /// <param name="command">Parâmetro requerido command de Update</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("v1/usuarios/{id}")]
        [Authorize(Roles = "Visitante,Administrador")]
        public async Task<ICommandResult> UsuarioAlterarAsync(Guid id, [FromBody] AtualizarUsuarioCommand command)
        {
            command.Id = id;
            return await _handler.HandlerAsync(command);
        }

        /// <summary>
        /// Excluir Usuario
        /// </summary>                
        /// <remarks><h2><b>Excluir Usuario.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido id do Usuario</param>        
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("v1/usuarios/{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ICommandResult> UsuarioApagarAsync(Guid id)
        {
            ApagarUsuarioCommand command = new ApagarUsuarioCommand() { Id = id };
            return await _handler.HandlerAsync(command);
        }
    }
}
