using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Biblioteca.Models;
using System.Linq;

namespace Biblioteca.Controllers
{
    public class Autenticacao
    {
        public static void CheckLogin(Controller controller)
        {   
            if(string.IsNullOrEmpty(controller.HttpContext.Session.GetString("user")))
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }
        }

        public static bool Verifica_login_senha(string Login, string Senha, Controller controller)
        {
            using (BibliotecaContext context = new BibliotecaContext())
            {
                Verifica_usuario_adimin_existe(context);

                Senha = Criptografia.Texto_criptografado(Senha);

                IQueryable<Usuario> Usuario_encontrado = context.Usuarios.Where(u => u.login==Login && u.senha==Senha);

                List<Usuario> Lista_usuario_encontrado = Usuario_encontrado.ToList();

                if (Lista_usuario_encontrado.Count==0)
                {
                    return false;
                }else{
                    controller.HttpContext.Session.SetString("login", Lista_usuario_encontrado[0].login);
                    controller.HttpContext.Session.SetString("senha", Lista_usuario_encontrado[0].senha);
                    controller.HttpContext.Session.SetInt32("tipo", Lista_usuario_encontrado[0].tipo);

                    return true;
                }
            }
        }

        public static void Verifica_usuario_adimin_existe(BibliotecaContext bc)
        {
            IQueryable<Usuario> usuario_encontrado = bc.Usuarios.Where(u => u.login == "admin");
            if (usuario_encontrado.ToList().Count==0)
            {
                Usuario admin = new Usuario();
                admin.login = "admin";
                admin.senha = Criptografia.Texto_criptografado("admin");
                admin.tipo = Usuario.ADIMIN;
                admin.nome = "Administrador";

                bc.Usuarios.Add(admin);
                bc.SaveChanges();
            }
        }

        public static void verfica_se_usuario_e_admin(Controller controller)
        {
            if (!(controller.HttpContext.Session.GetInt32("tipo")==Usuario.ADIMIN))
            {
                controller.Request.HttpContext.Response.Redirect("/Usuario/Cadastro_Realizado");
            }
        }
    }
}