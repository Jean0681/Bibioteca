namespace Biblioteca.Models
{
    public class Usuario
    {
        public static int ADIMIN = 0;
        public static int PADRAO = 1;

        public int id {get; set;}
        public string nome {get; set;}
        public string login {get; set;}
        public string senha {get; set;}
        public int tipo {get; set;} // Faz referencia a o "ADIMIN" e ao "PADRAO", o "int" aqui é pq ambos são representados por número, e dependendo do numero será adimin ou usuario padrão 
    }
}