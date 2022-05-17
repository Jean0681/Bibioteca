using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Lista_Usuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verfica_se_usuario_e_admin(this);

            return View(new UsuarioService().Listar_Usuarios());
        }

        public IActionResult Editar_usuario(int id)
        {
            Usuario u = new UsuarioService().Listar(id);
            return View(u);
        }

        [HttpPost]
        public IActionResult Editar_usuario(Usuario usuario_editado)
        {
            UsuarioService us = new UsuarioService();
            us.Editar_Usuario(usuario_editado);
            return RedirectToAction("Lista_Usuarios");
        }

        public IActionResult Registrar_usuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verfica_se_usuario_e_admin(this);

            return View();
        }

        [HttpPost]
        public IActionResult Registrar_Usuarios(Usuario novo_usuario)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verfica_se_usuario_e_admin(this);

            novo_usuario.senha = Criptografia.Texto_criptografado(novo_usuario.senha);

            UsuarioService us = new UsuarioService();
            us.Criar_usuario(novo_usuario);

            return RedirectToAction("Cadastro_Realizado");
        }

        public IActionResult Excluir_usuario(int id)
        {
            return View(new UsuarioService().Listar(id));
        }

        [HttpPost]
        public IActionResult Excluir_usuario(string decisao, int id)
        {
            if (decisao=="EXCLUIR")
            {
                ViewData["Mensagem"]="Exclusão de Usuário " + new UsuarioService().Listar(id).nome + " Realizado co sucesso";
                new UsuarioService().excluir_usuario(id);
                
                return View("Lista_Usuarios", new UsuarioService().Listar_Usuarios());
            }else{
                ViewData["Mensagem"]="Exclusão cancelada";
                return View("Lista_Usuarios", new UsuarioService().Listar_Usuarios());
            }
        }

        public IActionResult Cadastro_Realizado()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verfica_se_usuario_e_admin(this);

            return View();
        }

        public IActionResult Acesso_Negado()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }
    }
}