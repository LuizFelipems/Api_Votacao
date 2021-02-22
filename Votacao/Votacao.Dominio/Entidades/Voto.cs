using System;

namespace Votacao.Dominio.Entidades
{
    public class Voto : Entidade
    {
        public Guid IdUsuario { get; private set; }
        public Guid IdFilme { get; private set; }
        public int Pontuacao { get; private set; }

        public Voto(Guid idUsuario, Guid idFilme, int pontuacao)
        {
            IdUsuario = idUsuario;
            IdFilme = idFilme;
            Pontuacao = pontuacao;
        }

        public Voto(Guid id, Guid idUsuario, Guid idFilme, int pontuacao) : base(id)
        {
            IdUsuario = idUsuario;
            IdFilme = idFilme;
            Pontuacao = pontuacao;
        }
    }
}
