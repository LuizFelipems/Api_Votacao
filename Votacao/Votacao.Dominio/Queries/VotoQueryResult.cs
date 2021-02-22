using System;

namespace Votacao.Dominio.Queries
{
    public class VotoQueryResult
    {
        public Guid Id { get; set; }
        public UsuarioQueryResult Usuario { get; set; }
        public FilmeQueryResult Filme { get; set; }
        public int Pontuacao { get; set; }
    }
}
