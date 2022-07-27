using ExoApiPFS.Models;
using ExoApiPFS.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExoApiPFS.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetoController : ControllerBase
    {
        private readonly ProjetoRepository _projetoRepository;

        public ProjetoController(ProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }


        [HttpGet]
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


        [HttpGet("{id}")]
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


        [HttpPost]
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


        [HttpDelete("{id}")]
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


        [HttpPut("{id}")]
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
