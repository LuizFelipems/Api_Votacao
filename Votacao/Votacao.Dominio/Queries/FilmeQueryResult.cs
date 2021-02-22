using System;

namespace Votacao.Dominio.Queries
{
    public class FilmeQueryResult
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Diretor { get; set; }
    }
}
