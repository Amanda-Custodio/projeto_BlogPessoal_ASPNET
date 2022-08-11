using BlogAPI.Src.Contextos;
using BlogAPI.Src.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlogAPI.Src.Repositorios.Implementacoes
{
    public class UsuarioRepositorio : IUsuario
    {
        #region Atributos
        private readonly BlogPessoalContexto _contexto;
        #endregion
        #region Construtores
        public UsuarioRepositorio(BlogPessoalContexto contexto)
        {
            _contexto = contexto;
        }
        #endregion

        #region Métodos
        public async Task<Usuario> PegarUsuarioPeloEmailAsync(string email)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task NovoUsuarioAsync(Usuario usuario)
        {
            await _contexto.Usuarios.AddAsync(
            new Usuario
            {
                Email = usuario.Email,
                Nome = usuario.Nome,
                Senha = usuario.Senha,
                Foto = usuario.Foto
            });
            await _contexto.SaveChangesAsync();
        }


        #endregion

    }
}
