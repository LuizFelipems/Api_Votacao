using System;

namespace Votacao.Dominio.Entidades
{
    public class Entidade
    {
        public Guid Id { get; protected set; }

        public Entidade()
        {
            Id = Guid.NewGuid();
        }

        public Entidade(Guid id)
        {
            Id = id;
        }
    }
}
