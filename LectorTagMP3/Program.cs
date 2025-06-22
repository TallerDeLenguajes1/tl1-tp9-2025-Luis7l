using System;
using System.IO;
using System.Text;

namespace LectorTagMP3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Ingrese la ruta del archivo MP3: ");
            string? ruta = Console.ReadLine();

            if (ruta == null || !File.Exists(ruta))
            {
                Console.WriteLine("El archivo no existe.");
                return;
            }

            // Leer los últimos 128 bytes del archivo MP3
            using FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.Read);
            if (fs.Length < 128)
            {
                Console.WriteLine("El archivo es demasiado pequeño para contener un tag ID3v1.");
                return;
            }

            fs.Seek(-128, SeekOrigin.End); // posicionarse al final del archivo
            byte[] tagBytes = new byte[128];
            fs.Read(tagBytes, 0, 128);

            // Verificar si comienza con "TAG"
            string cabecera = Encoding.ASCII.GetString(tagBytes, 0, 3);
            if (cabecera != "TAG")
            {
                Console.WriteLine("  El archivo no contiene un tag ID3v1.");
                return;
            }

            // Crear objeto ID3v1 y cargar datos
            Id3v1Tag tag = new Id3v1Tag
            {
                Titulo = LeerTexto(tagBytes, 3, 30),
                Artista = LeerTexto(tagBytes, 33, 30),
                Album = LeerTexto(tagBytes, 63, 30),
                Anio = LeerTexto(tagBytes, 93, 4)
            };

            // Mostrar resultados
            Console.WriteLine("\n🎵 Información de la canción:");
            Console.WriteLine($"Título : {tag.Titulo}");
            Console.WriteLine($"Artista: {tag.Artista}");
            Console.WriteLine($"Álbum  : {tag.Album}");
            Console.WriteLine($"Año    : {tag.Anio}");
        }

        // Método para extraer texto limpio desde los bytes
        static string LeerTexto(byte[] datos, int inicio, int longitud)
        {
            return Encoding.GetEncoding("latin1")
                           .GetString(datos, inicio, longitud)
                           .TrimEnd('\0', ' ');
        }
    }

    class Id3v1Tag
    {
        public string Titulo { get; set; } = "";
        public string Artista { get; set; } = "";
        public string Album { get; set; } = "";
        public string Anio { get; set; } = "";
    }
}
