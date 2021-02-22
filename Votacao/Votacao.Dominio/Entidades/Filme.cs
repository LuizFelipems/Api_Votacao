using System;
using System.Collections.Generic;

namespace Votacao.Dominio.Entidades
{
    public class Filme : Entidade
    {
        public string Nome { get; private set; }
        public string Diretor { get; private set; }
        public string Genero { get; private set; }
        public List<string> Atores { get; private set; }

        public Filme(string nome, string diretor, string genero, List<string> atores)
        {
            Nome = nome;
            Diretor = diretor;
            Genero = genero;
            Atores = atores;
        }

        public Filme(Guid id, string nome, string diretor, string genero, List<string> atores) : base(id)
        {
            Nome = nome;
            Diretor = diretor;
            Genero = genero;
            Atores = atores;
        }
    }
}
