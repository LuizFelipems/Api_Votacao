using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Votacao.Dominio.Entidades;
using Votacao.Dominio.Queries;

namespace Votacao.Dominio.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task InserirAsync(Usuario usuario);
        Task AlterarAsync(Usuario usuario);
        Task DeletarAsync(Guid id);
        Task<List<UsuarioQueryResult>> ListarAsync();
        Task<UsuarioQueryResult> ObterPorIdAsync(Guid id);
        Task<UsuarioQueryResult> ObterPorLoginAsync(string login);
        Task<bool> CheckIdAsync(Guid id);
        Task<bool> CheckAutenticacaoAsync(string login, string senha);
        Task<bool> CheckLoginAsync(string login);
        Task<bool> CheckUsuarioVotouAsync(Guid id);
    }
}
