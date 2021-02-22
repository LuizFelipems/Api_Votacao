using System;

namespace Votacao.Dominio.Entidades
{
    public class Voto
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
    }
}
