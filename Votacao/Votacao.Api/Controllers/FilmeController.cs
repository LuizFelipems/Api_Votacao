using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Votacao.Dominio.Commands.Filme.Inputs;
using Votacao.Dominio.Handlers;
using Votacao.Dominio.Interfaces.Commands;
using Votacao.Dominio.Interfaces.Repositories;
using Votacao.Dominio.Queries;

namespace Votacao.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly FilmeHandler _handler;

        public FilmeController(IFilmeRepository filmeRepository, FilmeHandler handler)
        {
            _filmeRepository = filmeRepository;
            _handler = handler;
        }

        /// <summary>
        /// Filmes
        /// </summary>                
        /// <remarks><h2><b>Lista os Filmes por osrdem de mais votado.</b></h2></remarks>
        /// <response code="200">OK Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/filmes")]
        [AllowAnonymous]
        public async Task<IEnumerable<FilmeQueryResult>> FilmesAsync(string nome, string diretor, string genero, string atores)
        {
            return await _filmeRepository.ListarMaisVotadosAsync(nome, diretor, genero, atores);
        }

        /// <summary>
        /// Filmes
        /// </summary>                
        /// <remarks><h2><b>Consulta o Filme.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido id do Filme</param>
        /// <response code="200">OK Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/filmes/{id}")]
        [AllowAnonymous]
        public async Task<FilmeQueryResult> FilmeAsync(Guid id)
        {
            return await _filmeRepository.ObterPorIdAsync(id);
        }

        /// <summary>
        /// Incluir Filme 
        /// </summary>                
        /// <remarks><h2><b>Administrador pode Incluir novo Filme.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Insert</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("v1/filmes")]
        [Authorize(Roles = "Administrador")]
        public async Task<ICommandResult> FilmeInserirAsync([FromBody] AdicionarFilmeCommand command)
        {
            return await _handler.HandlerAsync(command);
        }

        /// <summary>
        /// Alterar Filme
        /// </summary>        
        /// <remarks><h2><b>Alterar Filme.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido id do Filme</param>        
        /// <param name="command">Parâmetro requerido command de Update</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("v1/filmes/{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ICommandResult> FilmeAlterarAsync(Guid id, [FromBody] AtualizarFilmeCommand command)
        {
            command.Id = id;
            return await _handler.HandlerAsync(command);
        }

        /// <summary>
        /// Excluir Filme
        /// </summary>                
        /// <remarks><h2><b>Excluir Filme.</b></h2></remarks>
        /// <param name="id">Parâmetro requerido id do Filme</param>        
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("v1/filmes/{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ICommandResult> FilmeApagarAsync(Guid id)
        {
            ApagarFilmeCommand command = new ApagarFilmeCommand() { Id = id };
            return await _handler.HandlerAsync(command);
        }

        /// <summary>
        /// Votar
        /// </summary>                
        /// <remarks><h2><b>Usuário pode Votar no filme escolhido.</b></h2></remarks>
        /// <param name="command">Parâmetro requerido command de Insert</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("v1/votar")]
        [Authorize(Roles = "Visitante")]
        public async Task<ICommandResult> VotarAsync([FromBody] VotarCommand command)
        {
            return await _handler.HandlerAsync(command);
        }
    }
}
