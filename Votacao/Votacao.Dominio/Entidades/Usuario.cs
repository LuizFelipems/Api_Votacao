using System;

namespace Votacao.Dominio.Entidades
{
    public class Usuario : Entidade
    {
        public string Nome { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public string Role { get; private set; }
        public bool Ativo { get; private set; }

        public Usuario(string nome, string login, string senha, string role, bool ativo = true)
        {
            Nome = nome;
            Login = login;
            Senha = senha;
            Role = role;
            Ativo = ativo;
        }

        public Usuario(Guid id, string nome, string login, string senha, string role, bool ativo = true) : base(id)
        {
            Nome = nome;
            Login = login;
            Senha = senha;
            Role = role;
            Ativo = ativo;
        }
    }
}
