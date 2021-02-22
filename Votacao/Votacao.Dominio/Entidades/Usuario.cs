using System;
using System.Collections.Generic;

namespace Votacao.Dominio.Entidades
{
    public class Usuario : Entidade
    {
        public string Nome { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public string Role { get; private set; }

        public Usuario(string nome, string login, string senha, string role)
        {
            Nome = nome;
            Login = login;
            Senha = senha;
            Role = role;
        }

        public Usuario(Guid id, string nome, string login, string senha, string role) : base(id)
        {
            Nome = nome;
            Login = login;
            Senha = senha;
            Role = role;
        }
    }
}
