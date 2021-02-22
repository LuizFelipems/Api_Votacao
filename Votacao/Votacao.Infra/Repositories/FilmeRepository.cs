using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Votacao.Dominio.Entidades;
using Votacao.Dominio.Interfaces.Repositories;
using Votacao.Dominio.Queries;
using Votacao.Infra.DataContexts;

namespace Votacao.Infra.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly DynamicParameters _parametros = new DynamicParameters();
        private readonly DataContext _dataContext;

        public FilmeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task InserirAsync(Filme filme)
        {
            try
            {
                _parametros.Add("Nome", filme.Nome, DbType.String);
                _parametros.Add("Diretor", filme.Diretor, DbType.String);
                _parametros.Add("Genero", filme.Genero, DbType.String);
                _parametros.Add("Atores", JsonSerializer.Serialize(filme.Atores), DbType.String);

                var sql = @"INSERT INTO Filme (Nome, Diretor, Genero, Atores) 
                            VALUES (@Nome, @Diretor, @Genero, @Atores); SELECT SCOPE_IDENTITY();";

                await _dataContext.SQLConnection.ExecuteAsync(sql, _parametros);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task AlterarAsync(Filme filme)
        {
            try
            {
                _parametros.Add("Id", filme.Id, DbType.String);
                _parametros.Add("Nome", filme.Nome, DbType.String);
                _parametros.Add("Diretor", filme.Diretor, DbType.String);
                _parametros.Add("Genero", filme.Genero, DbType.String);
                _parametros.Add("Atores", JsonSerializer.Serialize(filme.Atores), DbType.String);

                var sql = @"UPDATE Filme SET Nome=@Nome, Diretor=@Diretor, Genero=@Genero, Atores=@Atores
                            WHERE Id=@Id;";

                await _dataContext.SQLConnection.ExecuteAsync(sql, _parametros);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task DeletarAsync(Guid id)
        {
            try
            {
                _parametros.Add("Id", id, DbType.String);

                var sql = @"DELETE FROM Filme WHERE Id=@Id;";

                await _dataContext.SQLConnection.ExecuteAsync(sql, _parametros);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<List<FilmeQueryResult>> ListarAsync()
        {
            try
            {
                var sql = @"SELECT * FROM Filme ORDER BY Nome;";

                var result = await _dataContext.SQLConnection.QueryAsync<FilmeQueryResult>(sql);

                foreach (var item in result)
                    item.Atores = JsonSerializer.Deserialize<List<string>>(item.Atores);

                return result.ToList();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<FilmeQueryResult> ObterPorIdAsync(Guid id)
        {
            try
            {
                _parametros.Add("Id", id, DbType.String);

                var sql = @"SELECT * FROM Filme WHERE Id=@Id;";

                var result = await _dataContext.SQLConnection.QueryAsync<FilmeQueryResult>(sql, _parametros);

                foreach (var item in result)
                    item.Atores = JsonSerializer.Deserialize<List<string>>(item.Atores);

                return result.FirstOrDefault();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<bool> CheckIdAsync(Guid id)
        {
            try
            {
                _parametros.Add("Id", id, DbType.String);

                var sql = @"SELECT * FROM Filme WHERE Id=@Id;";

                var result = await _dataContext.SQLConnection.QueryAsync<bool>(sql, _parametros);

                return result.FirstOrDefault();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task VotarAsync(Voto voto)
        {
            try
            {
                _parametros.Add("IdUsuario", voto.IdUsuario, DbType.String);
                _parametros.Add("IdFilme", voto.IdFilme, DbType.String);
                _parametros.Add("Pontuacao", voto.Pontuacao, DbType.Int64);

                var sql = @"INSERT INTO Voto (IdUsuario, IdFilme) VALUES (@IdUsuario, @IdFilme); SELECT SCOPE_IDENTITY();";

                await _dataContext.SQLConnection.ExecuteScalarAsync<long>(sql, _parametros);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<List<FilmeQueryResult>> ListarMaisVotadosAsync(string nome, string diretor, string genero, string atores)
        {
            try
            {
                _parametros.Add("Nome", nome, DbType.String);
                _parametros.Add("Diretor", diretor, DbType.String);
                _parametros.Add("Genero", genero, DbType.String);
                _parametros.Add("Atores", atores, DbType.String);

                var sql = @"SELECT MAX(f.Id) as Id,
		                            f.Nome as Nome, 
		                            MAX(f.Diretor) as Diretor,
		                            MAX(f.Genero) as Genero,
		                            MAX(f.Atores) as Atores,
		                            SUM(v.Pontuacao) as QuantidadeVotos from filme f
                            JOIN Voto v on f.Id = v.IdFilme

                            WHERE (@Nome IS NULL 
		                            OR f.Nome like '%@Nome%') AND
	                            (@Diretor IS NULL 
		                            OR f.Diretor like '%@Diretor%') AND
	                            (@Genero IS NULL 
		                            OR f.Genero like '%@Genero%') AND
	                            (@Atores IS NULL 
		                            OR f.Atores like '%@Atores%')
                            GROUP BY f.Nome
                            ORDER BY f.Nome, sum(v.Pontuacao);";

                var result = await _dataContext.SQLConnection.QueryAsync<FilmeQueryResult>(sql, _parametros);

                foreach (var item in result)
                    item.Atores = JsonSerializer.Deserialize<List<string>>(item.Atores);

                return result.ToList();
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
