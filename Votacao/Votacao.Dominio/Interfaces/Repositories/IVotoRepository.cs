using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Votacao.Dominio.Entidades;
using Votacao.Dominio.Queries;

namespace Votacao.Dominio.Interfaces.Repositories
{
    public interface IVotoRepository
    {
        Task InserirAsync(Voto voto);
        Task<List<VotoQueryResult>> ListarVotosAsync();
        Task<bool> CheckUsuarioVotouAsync(Guid idUsuario);
        Task<VotoQueryResult> ObterVotoAsync(Guid id);
    }
}
