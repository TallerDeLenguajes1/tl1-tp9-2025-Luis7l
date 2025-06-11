using System.IO;
    
internal class Program
{

    private static void Main(string[] args)
    {
        string? path;
        bool Estexto = false;

        do

        {
            Console.WriteLine("Ingrese el path de  el archivo ");
            path = Console.ReadLine();
            if (Directory.Exists(path))
            {
                Estexto = true;
            }
            else
            {
                Console.WriteLine("Ingrese un Path valido");
                path = Console.ReadLine();
            }
        } while (!Estexto);
        
    }
}