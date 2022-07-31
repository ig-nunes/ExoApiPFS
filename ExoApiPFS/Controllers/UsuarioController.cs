using ExoApiPFS.Interfaces;
using ExoApiPFS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExoApiPFS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _iUsuarioRepository;

        public UsuarioController(IUsuarioRepository iUsuarioRepository)
        {
            _iUsuarioRepository = iUsuarioRepository;
        }


        /// <summary>
        /// Buscar usuários
        /// </summary>
        /// <returns></returns>
        /// <response code="200">código retornado junto da lista de usuários buscado, caso a pesquisa tenha sido completada com sucesso</response>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        public IActionResult ListarUsuarios()
        {
            try
            {
                return Ok(_iUsuarioRepository.Listar());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Buscar usuário por ID
        /// </summary>
        /// <param name="id">ID do projeto-alvo</param>
        /// <returns></returns>
        /// <response code="200">código retornado junto dos dados do usuário buscado, caso a pesquisa tenha sido completada com sucesso</response>
        /// <response code="404">Código apresentado caso o usuário não seja encontrado</response>
        /// <exception cref="Exception"></exception>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                Usuario usuarioEncontrado = _iUsuarioRepository.BuscarPorId(id);

                if (usuarioEncontrado == null)
                {
                    return NotFound();
                }

                return Ok(usuarioEncontrado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Cadastrar Novo usuário
        /// </summary>
        /// <param name="usuario">Dados do novo usuário</param>
        /// <returns></returns>
        /// <remarks>
        /// Exemplo de Body:
        /// 
        ///     {
        ///         "email": "exemplo@email.com",
        ///         "senha": "senhaexemplo",
        ///         "tipo": "1"
        ///     }
        /// 
        /// Obs:. usuários do tipo 1 são comuns e usuários do tipo 2 são administradores
        /// </remarks>
        /// <response code="201">Código retornado caso o usuário tenha sido cadastrado com sucesso</response>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CadastrarUsuario(Usuario usuario)
        {
            try
            {
                _iUsuarioRepository.Cadastrar(usuario);

                return StatusCode(201);
                //return Created("Usuario cadastrado com sucesso!", usuario);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Deletar usuário cadastrado
        /// </summary>
        /// <param name="id">ID do usuário a ser deletado</param>
        /// <returns></returns>
        /// <response code="200">Código retornado caso o usuário tenha sido deletado com sucesso. Retorna também uma frase de confirmação</response>
        /// <response code="404">Código retornado caso o usuário a ser deletado não tenha sido encontrado</response>
        /// <exception cref="Exception"></exception>
        [HttpDelete("{id}")]
        public IActionResult DeletarUsuario(int id)
        {
            try
            {
                Usuario usuarioEncontrado = _iUsuarioRepository.BuscarPorId(id);

                if  (usuarioEncontrado == null)
                {
                    return NotFound();
                }

                _iUsuarioRepository.Deletar(id);

                return Ok("Usuario removido com sucesso");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Atualizar usuário cadastrado
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <param name="usuario">Novos dados do usuário a ser atualizado</param>
        /// <returns></returns>
        /// <remarks>
        /// Exemplo de Body:
        /// 
        ///     {
        ///         "email": "exemploatualizado@email.com",
        ///         "senha": "senhaexemploatualizada",
        ///         "tipo": "1"
        ///     }
        /// 
        /// Obs:. usuários do tipo 1 são comuns e usuários do tipo 2 são administradores
        /// </remarks>
        /// <response code="200">Código retornado caso o usuário tenha sido atualizado com sucesso</response>
        /// <response code="404">Código retornado caso o usuário tenha sido deletado com sucesso</response>
        [HttpPut("{id}")]
        public IActionResult AtualizarUsuario(int id, Usuario usuario)
        {
            Usuario usuarioEncontrado = _iUsuarioRepository.BuscarPorId(id);

            if (usuarioEncontrado == null)
            {
                return NotFound();
            }

            _iUsuarioRepository.Atualizar(id, usuario);

            return Ok("Usuario atualizado com sucesso");
        }

    }
}
