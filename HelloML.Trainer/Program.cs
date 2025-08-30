using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace HelloML.Trainer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to HelloML");
            EnsureOutputDirectory();

            // Binary Classification
            ModelBuilder.Execute();

            Console.WriteLine("\n\nDONE. Press any key.");
            Console.ReadKey();
        }

        // Ensure the output directory where the model will be
        // saved exists
        private static void EnsureOutputDirectory()
        {
            var assemblyFolderPath = new FileInfo(typeof(Program).Assembly.Location).DirectoryName;
            if (assemblyFolderPath == null)
            {
                return;
            }

            var dir = Path.GetFullPath(Path.Combine(assemblyFolderPath, @"..\..\..\Output"));
            if (!Directory.Exists(dir)) 
            {
                Directory.CreateDirectory(dir);
            }
        }
    }
}
