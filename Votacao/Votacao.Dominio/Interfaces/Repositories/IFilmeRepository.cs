using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Votacao.Dominio.Entidades;
using Votacao.Dominio.Queries;

namespace Votacao.Dominio.Interfaces.Repositories
{
    public interface IFilmeRepository
    {
        Task InserirAsync(Filme filme);
        Task AlterarAsync(Filme filme);
        Task DeletarAsync(Guid id);
        Task<List<FilmeQueryResult>> ListarAsync();
        Task<FilmeQueryResult> ObterPorIdAsync(Guid id);
        Task<bool> CheckIdAsync(Guid id);
        Task VotarAsync(Voto voto);
        Task<List<FilmeQueryResult>> ListarMaisVotadosAsync(string nome, string diretor, string genero, string atores);
    }
}
