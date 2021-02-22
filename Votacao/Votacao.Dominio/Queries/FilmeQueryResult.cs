using System;
using System.Collections.Generic;

namespace Votacao.Dominio.Queries
{
    public class FilmeQueryResult
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Diretor { get; set; }
        public string Genero { get; set; }
        public dynamic Atores { get; set; }
        public int QuantidadeVotos { get; set; }
    }
}
