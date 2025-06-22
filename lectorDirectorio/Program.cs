using System;
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
            if (!Directory.Exists(path))
            {
                Console.WriteLine("La ruta no es válida.");
                return;
            }
            string rutaCsv = Path.Combine(path, "reporte_archivos.csv");

            using (StreamWriter writer = new StreamWriter(rutaCsv, false, Encoding.UTF8))
            {
                // Escribir encabezado
                writer.WriteLine("Nombre del Archivo,Tamaño (KB),Fecha de Última Modificación");

                // Escribir cada archivo
                foreach (string archivo in archivos)
                {
                    FileInfo info = new FileInfo(archivo);
                    string nombreArchivo = info.Name;
                    double tamKB = Math.Round(info.Length / 1024.0, 2);
                    string fechaMod = info.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");

                    // Escribir línea en el CSV
                    writer.WriteLine($"{nombreArchivo},{tamKB},{fechaMod}");
                }
            }

            Console.WriteLine($"\n✅ Reporte generado correctamente en: {rutaCsv}");
        }
    }


}
