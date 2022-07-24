using ExoApiPFS.Contexts;
using ExoApiPFS.Models;

namespace ExoApiPFS.Repositories
{
    public class ProjetoRepository
    {
        private readonly ExoApiContext _context;

        public ProjetoRepository(ExoApiContext context)
        {
            _context = context;
        }

        public List<Projeto> ListarProjetos()
        {
            return _context.Projetos.ToList();
        }

    }
}
