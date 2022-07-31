using ExoApiPFS.Models;
using ExoApiPFS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExoApiPFS.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class ProjetoController : ControllerBase
    {
        private readonly ProjetoRepository _projetoRepository;

        public ProjetoController(ProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }

        /// <summary>
        /// Listar todos os projetos
        /// </summary>
        /// <returns>Todos os projetos cadastrados</returns>
        /// <response code="200">código retornado junto da lista de projetos buscado, caso a pesquisa tenha sido completada com sucesso</response>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ListarProjetos()
        {
            try
            {
                return Ok(_projetoRepository.ListarProjetos());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Buscar projeto por ID
        /// </summary>
        /// <param name="id">ID do projeto-alvo</param>
        /// <returns></returns>
        /// <response code="200">código retornado junto dos dados do projeto buscado, caso a pesquisa tenha sido completada com sucesso</response>
        /// <response code="404">Código apresentado caso o projeto não seja encontrado</response>
        /// <exception cref="Exception"></exception>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                Projeto projetoBuscado = _projetoRepository.BuscarPorId(id);

                if (projetoBuscado == null)
                {
                    return NotFound();
                }
                
                return Ok(projetoBuscado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Cadastrar novo projeto
        /// </summary>
        /// <param name="projeto">Dados do novo projeto</param>
        /// <returns></returns>
        /// <remarks>
        /// Exemplo de Body:
        /// 
        ///     {
        ///         "titulo": "Título exemplo",
        ///         "situacao": "Status exemplo",
        ///         "dataInicio": "01/02/2022",
        ///         "descricao": "Descrção exemplo"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Código retornado caso o projeto tenha sido cadastrado com sucesso</response>
        /// <exception cref="Exception"></exception>
        [Authorize(Roles = "1")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CadastrarProjeto(Projeto projeto)
        {
            try
            {
                _projetoRepository.CadastrarProjeto(projeto);

                return StatusCode(201);

                //return Ok("Livro Cadastrado Com Sucesso");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Deletar projeto cadastrado
        /// </summary>
        /// <param name="id">ID do projeto a ser deletado</param>
        /// <returns></returns>
        /// <response code="200">Código retornado caso o projeto tenha sido deletado com sucesso. Retorna também uma frase de confirmação</response>
        /// <response code="404">Código retornado caso o projeto a ser deletado não tenha sido encontrado</response>
        /// <exception cref="Exception"></exception>
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeletarProjeto(int id)
        {
            try
            {
                Projeto projetoBuscado = _projetoRepository.BuscarPorId(id);

                if (projetoBuscado == null)
                {
                    return NotFound();
                }
                else
                {
                    _projetoRepository.DeletarProjeto(id);
                }

                return Ok("O Projeto foi removido com sucesso!");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Atualizar projeto cadastrado
        /// </summary>
        /// <param name="id">ID do projeto</param>
        /// <param name="projeto">Novos dados do projeto a ser atualizado</param>
        /// <returns></returns>
        /// <remarks>
        /// Exemplo de Body:
        /// 
        ///     {
        ///         "titulo": "Novo Título exemplo",
        ///         "situacao": "Novo Status exemplo",
        ///         "dataInicio": "01/02/2022",
        ///         "descricao": "Nova Descrção exemplo"
        ///     }
        ///
        /// </remarks>
        /// <response code="204">Código retornado caso o projeto tenha sido atualizado com sucesso</response>
        /// <response code="404">Código retornado caso o projeto tenha sido deletado com sucesso</response>
        /// <exception cref="Exception"></exception>
        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult AlterarProjeto(int id, Projeto projeto)
        {
            try
            {
                Projeto projetoBuscado = _projetoRepository.BuscarPorId(id);

                if (projetoBuscado == null)
                {
                    return NotFound();
                }
                else
                {
                    _projetoRepository.AtualizarProjeto(id, projeto);
                }

                return StatusCode(204);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
