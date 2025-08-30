using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloML.Trainer
{
    class DebugHelper
    {
        // Append current path to relative path
        public static string GetAbsolutePath(string relativePath)
        {
            var assemblyFolderPath = new FileInfo(typeof(Program).Assembly.Location).DirectoryName;
            if (assemblyFolderPath == null)
            {
                return "";
            }

            var basePath = Path.GetFullPath(Path.Combine(assemblyFolderPath, @"..\..\..\"));
            return Path.Combine(basePath, relativePath);
        }

    }
}
