using System.IO;
using System.Text;
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

            }
        } while (!Estexto);
        if (path != null)
        {
            Console.WriteLine("\nCarpetas");
            string[] directorios = Directory.GetDirectories(path);
            foreach (string dir in directorios)
            {
                string carpetaNombre = Path.GetFileName(dir);
                Console.WriteLine($"-{carpetaNombre}");
            }

            Console.WriteLine("\nArchivos");
            string[] archivos = Directory.GetFiles(path);
            foreach (string archivo in archivos)
            {
                string archivoNombre = Path.GetFileName(archivo);
                 double tamKb = (double)new FileInfo(archivo).Length / 1024;

                Console.WriteLine($"- {archivoNombre} tamaño: ({tamKb} KB)");
            }
     }
    }
}