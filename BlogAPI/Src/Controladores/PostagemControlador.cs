using BlogAPI.Src.Modelos;
using BlogAPI.Src.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogAPI.Src.Controladores
{
    [ApiController]
    [Route("api/Postagens")]
    [Produces("application/json")]
    public class PostagemControlador : ControllerBase
    {
        #region Atributos
        private readonly IPostagem _repositorio;
        #endregion
        #region Construtores
        public PostagemControlador(IPostagem repositorio)
        {
            _repositorio = repositorio;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Pegar todas as postagens
        /// </summary>
        /// <response code="200">Retorna lista com todas as postagens</response>
        /// <response code="204">Nenhuma postagem encontrada</response>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> PegarTodasPostagensAsync()
        {
            var lista = await _repositorio.PegarTodasPostagensAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

        /// <summary>
        /// Pegar postagem pelo Id
        /// </summary>
        /// <param name="idPostagem">Id da postagem</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna a postagem</response>
        /// <response code="204">Postagem não existente</response>
        [HttpGet("id/{idPostagem}")]
        [Authorize]
        public async Task<ActionResult> PegarPostagemPeloIdAsync([FromRoute] int idPostagem)
        {
            try
            {
                return Ok(await _repositorio.PegarPostagemPeloIdAsync(idPostagem));
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// /// Criar nova postagem
        /// </summary>
        /// <param name="postagem">Construtor para criar postagem</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// POST api/Postagens
        /// {
        /// "título": "",
        /// "descricao": "",
        /// "foto": "URL"
        /// }
        /// </remarks>
        /// <response code="201">Retorna postagem criada</response>
        /// <response code="400">Postagem inválida</response>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> NovaPostagemAsync([FromBody] Postagem postagem)
        {
            try
            {
                await _repositorio.NovaPostagemAsync(postagem);
                return Created($"api/Postagens", postagem);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// /// Atualizar postagem
        /// </summary>
        /// <param name="postagem">Atualizar postagem já existente</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// PUT /api/Postagem
        /// {
        /// "título": "",
        /// "descricao": "",
        /// "foto": "URL"
        /// }
        /// </remarks>
        /// <response code="200">Retorna postagem atualizada</response>
        /// <response code="400">Postagem não atualizada</response>
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> AtualizarPostagemAsync([FromBody] Postagem postagem)
        {
            try
            {
                await _repositorio.AtualizarPostagemAsync(postagem);
                return Ok(postagem);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// /// Deletar postagem
        /// </summary>
        /// <param name="idPostagem">Deletar postagem existente</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// DELETE /api/Temas/deletar/{idPostagem}
        /// </remarks>
        /// <response code="204">Retorna postagem deletada</response>
        /// <response code="404">Postagem não encontrada</response>
        [HttpDelete("deletar/{idPostagem}")]
        [Authorize]
        public async Task<ActionResult> DeletarPostagem([FromRoute] int idPostagem)
        {
            try
            {
                await _repositorio.DeletarPostagemAsync(idPostagem);
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
