using System;
using System.Security.Cryptography;
using System.Text;

namespace Biblioteca.Models
{
    public class Criptografia
    {
        public static string Texto_criptografado(string texto_claro)
        {
            MD5 MD5Hasher = MD5.Create();

            byte[] By = Encoding.Default.GetBytes(texto_claro);
            byte[] bytes_criptografado = MD5Hasher.ComputeHash(By);

            StringBuilder sb = new StringBuilder();

            foreach (byte b in bytes_criptografado)
            {
                string DebugB = b.ToString("x2");
                sb.Append(DebugB);
            }

            return sb.ToString();  
        } // Tentar entender depois como isso funciona, pesquisar por "md5".
    }
}