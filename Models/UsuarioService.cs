using System.Collections.Generic;
using System.Linq;


namespace Biblioteca.Models
{
    public class UsuarioService
    {
        public void Criar_usuario (Usuario novo_usuario)
        {
            using (BibliotecaContext context = new BibliotecaContext())
            {
                context.Add(novo_usuario);
            }
        }

        public List<Usuario> Listar_Usuarios() // tentar entender a diferença desse coddigo de lista para o da parte de baixo.
        {
            using (BibliotecaContext context = new BibliotecaContext())
            {
                return context.Usuarios.ToList();
            }
        } // Pesquisar pq tem esses dois listar pq n explicaram

        public Usuario Listar(int id)
        {
            using (BibliotecaContext context = new BibliotecaContext())
            {
                return context.Usuarios.Find(id);
            }
        } // Pesquisar pq tem dois listar pq não explicaram

        public void Editar_Usuario(Usuario editando_usuario)
        {
            using (BibliotecaContext context = new BibliotecaContext())
            {
                Usuario u = context.Usuarios.Find(editando_usuario.id);

                u.login = editando_usuario.login;
                u.nome = editando_usuario.nome;
                u.senha = editando_usuario.senha;
                u.tipo = editando_usuario.tipo;

                context.SaveChanges();
            }
        }

        public void excluir_usuario(int Id) 
        {
            using (BibliotecaContext context = new BibliotecaContext())
            {
                context.Usuarios.Remove(context.Usuarios.Find(Id)); // A tradução de "Find" é encontrar, ele basicamente consulta o banco de dados e retorna o valor encontrado.
                context.SaveChanges();
            }
        }
    }
}