using System;

namespace Votacao.Dominio.Queries
{
    public class UsuarioQueryResult
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
    }
}
