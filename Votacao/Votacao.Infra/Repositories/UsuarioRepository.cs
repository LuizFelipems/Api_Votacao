using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Votacao.Dominio.Entidades;
using Votacao.Dominio.Interfaces.Repositories;
using Votacao.Dominio.Queries;
using Votacao.Infra.DataContexts;

namespace Votacao.Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DynamicParameters _parametros = new DynamicParameters();
        private readonly DataContext _dataContext;

        public UsuarioRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task InserirAsync(Usuario usuario)
        {
            try
            {
                _parametros.Add("Id", usuario.Id, DbType.Guid);
                _parametros.Add("Nome", usuario.Nome, DbType.String);
                _parametros.Add("Login", usuario.Login, DbType.String);
                _parametros.Add("Senha", usuario.Senha, DbType.String);
                _parametros.Add("Role", usuario.Role, DbType.String);
                _parametros.Add("Ativo", usuario.Ativo, DbType.String);

                var sql = @"INSERT INTO Usuario (Id, Nome, Login, Senha, Role, Ativo) VALUES 
                            (@Id, @Nome, @Login, @Senha, @Role, @Ativo); SELECT SCOPE_IDENTITY();";

                await _dataContext.SQLConnection.ExecuteScalarAsync<long>(sql, _parametros);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task AlterarAsync(Usuario usuario)
        {
            try
            {
                _parametros.Add("Id", usuario.Id, DbType.Guid);
                _parametros.Add("Nome", usuario.Nome, DbType.String);
                _parametros.Add("Login", usuario.Login, DbType.String);
                _parametros.Add("Senha", usuario.Senha, DbType.String);
                _parametros.Add("Role", usuario.Role, DbType.String);

                var sql = @"UPDATE Usuario SET Id=@Id, Nome=@Nome, Login=@Login, Senha=@Senha, Role=@Role WHERE Id=@Id;";

                await _dataContext.SQLConnection.ExecuteAsync(sql, _parametros);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task DeletarAsync(Guid id)
        {
            try
            {
                var sql = @"UPDATE Usuario SET Ativo=0 WHERE Id=@Id;";

                await _dataContext.SQLConnection.ExecuteAsync(sql, _parametros);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<List<UsuarioQueryResult>> ListarAsync()
        {
            try
            {
                var sql = @"SELECT * FROM Usuario
                            WHERE Role = 'Visitante'
                            AND Ativo = 1
                            ORDER BY Nome;";

                var result = await _dataContext.SQLConnection.QueryAsync<UsuarioQueryResult>(sql);

                return result.ToList();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<UsuarioQueryResult> ObterPorIdAsync(Guid id)
        {
            try
            {
                _parametros.Add("Id", id, DbType.Guid);

                var sql = @"SELECT * FROM Usuario WHERE Id=@Id;";

                var result = await _dataContext.SQLConnection.QueryAsync<UsuarioQueryResult>(sql, _parametros);
                
                return result.FirstOrDefault();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<UsuarioQueryResult> ObterPorLoginAsync(string login)
        {
            try
            {
                _parametros.Add("Login", login, DbType.String);

                var sql = @"SELECT * FROM Usuario WHERE Login=@Login;";

                var result = await _dataContext.SQLConnection.QueryAsync<UsuarioQueryResult>(sql, _parametros);

                return result.FirstOrDefault();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<bool> CheckIdAsync(Guid id)
        {
            try
            {
                _parametros.Add("Id", id, DbType.Guid);

                var sql = @"SELECT * FROM Usuario WHERE Id=@Id AND Ativo = 1;";

                var result = await _dataContext.SQLConnection.QueryAsync<UsuarioQueryResult>(sql, _parametros);

                if (result.Count() > 0)
                    return true;

                return false;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<bool> CheckAutenticacaoAsync(string login, string senha)
        {
            try
            {
                _parametros.Add("Login", login, DbType.String);
                _parametros.Add("Senha", senha, DbType.String);

                var sql = @"SELECT * FROM Usuario 
                            WHERE Login=@Login AND Senha=@Senha;";

                var result = await _dataContext.SQLConnection.QueryAsync<UsuarioQueryResult>(sql, _parametros);

                if (result.Count() > 0)
                    return true;

                return false;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<bool> CheckLoginAsync(string login)
        {
            try
            {
                _parametros.Add("Login", login, DbType.String);

                var sql = @"SELECT * FROM Usuario WHERE Login=@Login;";

                var result = await _dataContext.SQLConnection.QueryAsync<UsuarioQueryResult>(sql, _parametros);

                if (result.Count() > 0)
                    return true;

                return false;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<bool> CheckUsuarioVotouAsync(Guid id)
        {
            try
            {
                _parametros.Add("IdUsuario", id, DbType.Guid);

                var sql = @"SELECT * FROM Usuario u
                            INNER JOIN Voto v ON v.IdUsuario = u.Id
                            WHERE v.IdUsuario=@IdUsuario;";

                var result = await _dataContext.SQLConnection.QueryAsync<UsuarioQueryResult>(sql, _parametros);

                if (result.Count() > 0)
                    return true;

                return false;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
