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
    }
}
