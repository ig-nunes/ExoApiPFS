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


        public Projeto BuscarPorId(int id)
        {
            return _context.Projetos.Find(id);
        }


        public void CadastrarProjeto(Projeto projeto)
        {
            _context.Projetos.Add(projeto);

            _context.SaveChanges();
        }


        public void DeletarProjeto(int id)
        {
            Projeto projeto = _context.Projetos.Find(id);

            if (projeto != null)
            {
                _context.Projetos.Remove(projeto);

                _context.SaveChanges();
            }
        }


        public void AtualizarProjeto(int id, Projeto projeto)
        {
            Projeto projetoBuscado = _context.Projetos.Find(id);

            if (projetoBuscado != null)
            {
              
                projetoBuscado.Titulo = projeto.Titulo;
                projetoBuscado.Situacao = projeto.Situacao;
                projetoBuscado.DataInicio = projeto.DataInicio;
                projetoBuscado.Descricao = projeto.Descricao;

            }

            _context.Projetos.Update(projetoBuscado);

            _context.SaveChanges();
        }

    }
}
