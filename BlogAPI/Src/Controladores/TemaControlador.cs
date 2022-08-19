using BlogAPI.Src.Modelos;
using BlogAPI.Src.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogAPI.Src.Controladores
{
    [ApiController]
    [Route("api/Temas")]
    [Produces("application/json")]
    public class TemaControlador : ControllerBase
    {
        #region Atributos
        private readonly ITemas _repositorio;
        #endregion
        #region Construtores
        public TemaControlador(ITemas repositorio)
        {
            _repositorio = repositorio;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Pegar todos os temas
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna lista de temas</response>
        /// <response code="204">Resultado vazio</response>
        [HttpGet]
        [Authorize]

        public async Task<ActionResult> PegarTodosTemasAsync()
        {
            var lista = await _repositorio.PegarTodosTemasAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

        /// <summary>
        /// Pegar tema pelo Id
        /// </summary>
        /// <param name="idTema">Id do tema</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o tema</response>
        /// <response code="404">Tema não encontrado</response>
        [HttpGet("id/{idTema}")]
        [Authorize]

        public async Task<ActionResult> PegarTemaPeloIdAsync([FromRoute] int idTema)
        {
            try
            {
                return Ok(await _repositorio.PegarTemaPeloIdAsync(idTema));
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// /// Criar novo tema
        /// </summary>
        /// <param name="tema">Construtor para criar tema</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// POST /api/Temas
        /// {
        /// "descricao": "",
        /// </remarks>
        /// <response code="201">Retorna tema criado</response>
        [HttpPost]
        [Authorize]

        public async Task<ActionResult> NovoTemaAsync([FromBody] Tema tema)
        {
            await _repositorio.NovoTemaAsync(tema);
            return Created($"api/Temas", tema);
        }

        /// <summary>
        /// /// Atualizar tema
        /// </summary>
        /// <param name="tema">Atualizar tema já existente</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// PUT /api/Temas
        /// {
        /// "descricao": "Nova descrição",
        /// </remarks>
        /// <response code="200">Retorna tema atualizado</response>
        /// <response code="400">Tema não atualizado</response>
        [HttpPut]
        [Authorize(Roles = "ADMINISTRADOR")]

        public async Task<ActionResult> AtualizarTema([FromBody] Tema tema)
        {
            try
            {
                await _repositorio.AtualizarTemaAsync(tema);
                return Ok(tema);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// /// Deletar tema
        /// </summary>
        /// <param name="idTema">Deletar tema existente</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// DELETE /api/Temas/deletar/{idTema}
        /// </remarks>
        /// <response code="204">Retorna tema deletado</response>
        /// <response code="404">Tema não encontrado</response>
        [HttpDelete("deletar/{idTema}")]
        [Authorize(Roles = "ADMINISTRADOR")]

        public async Task<ActionResult> DeletarTema([FromRoute] int idTema)
        {
            try
            {
                await _repositorio.DeletarTemaAsync(idTema);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }
        #endregion
    }
}
