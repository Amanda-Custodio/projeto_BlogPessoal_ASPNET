using BlogAPI.Src.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogAPI.Src.Repositorios
{
    public interface ITemas
    {
        Task<List<Tema>> PegarTodosTemasAsync();
        Task<Tema> PegarTemaPeloIdAsync(int id);
        Task NovoTemaAsync(Tema tema);
        Task AtualizarTemaAsync(Tema tema);
        Task DeletarTemaAsync(int id);

    }
}
