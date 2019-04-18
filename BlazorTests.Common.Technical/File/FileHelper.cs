using System.Collections.Generic;
using System.IO;

namespace BlazorTests.Common.Technical.File
{
    public static class FileHelper
    {
        public static bool IsImage(string filePath)
        {
            IList<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
            return ImageExtensions.Contains(Path.GetExtension(filePath).ToUpperInvariant());
        }

        public static void Copy(string sourceFilePath, string destinationFilePath)
        {
            // On copie le fichier
            System.IO.File.Copy(sourceFilePath, destinationFilePath);

            // On redéfinit les valeurs ayant été écrasé lors de la copie
            System.IO.File.SetCreationTime(destinationFilePath, System.IO.File.GetCreationTime(sourceFilePath));
            System.IO.File.SetLastWriteTime(destinationFilePath, System.IO.File.GetLastWriteTime(sourceFilePath));
            System.IO.File.SetLastAccessTime(destinationFilePath, System.IO.File.GetLastAccessTime(sourceFilePath));
        }
    }
}
