using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Models
{
    public class EmprestimoService 
    {
        public void Inserir(Emprestimo e)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Emprestimos.Add(e);
                bc.SaveChanges();
            }
        }

        public void Atualizar(Emprestimo e)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Emprestimo emprestimo = bc.Emprestimos.Find(e.Id);
                emprestimo.NomeUsuario = e.NomeUsuario;
                emprestimo.Telefone = e.Telefone;
                emprestimo.LivroId = e.LivroId;
                emprestimo.DataEmprestimo = e.DataEmprestimo;
                emprestimo.DataDevolucao = e.DataDevolucao;
                emprestimo.Devolvido = e.Devolvido;

                bc.SaveChanges();
            }
        }

        public ICollection<Emprestimo> ListarTodos(FiltrosEmprestimos filtro = null)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                IQueryable<Emprestimo> Query;
                if (filtro != null)
                {
                    switch (filtro.TipoFiltro)
                    {
                        case "Usuario":
                            Query = bc.Emprestimos.Include(e => e.Livro).Where(e => e.NomeUsuario.Contains(filtro.Filtro,System.StringComparison.OrdinalIgnoreCase)); // "System.StringComparison.OrdinalIgnoreCase" utilizado para não haver diferença na letra maiscula ou minuscula utilizada.
                        break;    

                        case "Livro":
                            Query = bc.Emprestimos.Include(e => e.Livro).Where(e => e.Livro.Titulo.Contains(filtro.Filtro,System.StringComparison.OrdinalIgnoreCase)); // Lembrar de testar sem o "Include" que era a forma que estava antes, para ver oque acontece.
                        break;

                        default:
                            // Query = bc.Emprestimos;
                            Query = bc.Emprestimos.Include(e => e.Livro);
                        break;
                    }
                }else{
                    Query = bc.Emprestimos;
                }

                return Query.OrderByDescending(e => e.DataDevolucao).ToList();
                // return Query.Include(e => e.Livro).ToList().OrderByDescending(e => e.DataDevolucao).ToList();
            }
        }

        public Emprestimo ObterPorId(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Emprestimos.Find(id);
            }
        }
    }
}