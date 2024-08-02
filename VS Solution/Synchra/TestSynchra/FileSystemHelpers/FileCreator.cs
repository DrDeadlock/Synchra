using System;
using System.IO;
using System.Text;

namespace TestSynchra.FileSystemHelpers
{
    public static class FileCreator
    {
        private static void SpawnDirectory(string path)
        {
            Console.WriteLine("Checking directory...");
            //if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static void CreateTxt(string path, string fileName)
        {
            SpawnDirectory(path);
            string fileContent = "Awesome Content of an awesome Author";

            using var fs = File.Create(path + fileName);
            fs.Write(Encoding.ASCII.GetBytes(fileContent));
        }

        public static void CreateTxt(string path, string fileName, string content)
        {
            SpawnDirectory(path);
            using var fs = File.Create(path + fileName);
            fs.Write(Encoding.ASCII.GetBytes(content));
        }
    }
}
