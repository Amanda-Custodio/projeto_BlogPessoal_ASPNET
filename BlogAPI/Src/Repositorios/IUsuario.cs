﻿using BlogAPI.Src.Modelos;
using System.Threading.Tasks;

namespace BlogAPI.Src.Repositorios
{
    /// <summary>
    /// <para>Resumo: Responsável por representar ações de CRUD de usuário</para>
    /// <para>Criado por: Amanda</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 18/08/2022</para>
    /// </summary>
    public interface IUsuario
    {
        Task<Usuario> PegarUsuarioPeloEmailAsync(string email);
        Task NovoUsuarioAsync(Usuario usuario);

    }
}
