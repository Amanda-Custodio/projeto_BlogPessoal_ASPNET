using System.Threading.Tasks;
using BlogAPI.Src.Modelos;
namespace BlogAPI.Src.Servicos
{
    /// <summary>
    /// <para>Resumo: Interface Responsavel por representar ações de autenticação</para>
    /// </summary>
public interface IAutenticacao
    {
        string CodificarSenha(string senha);
        Task CriarUsuarioSemDuplicarAsync(Usuario usuario);
        string GerarToken(Usuario usuario);
    }
}

