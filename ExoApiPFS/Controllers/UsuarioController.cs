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


        [HttpPost]
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
