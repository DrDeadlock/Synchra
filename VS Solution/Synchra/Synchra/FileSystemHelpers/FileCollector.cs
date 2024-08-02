using System;
using System.IO;
using System.Linq;

namespace Synchra.FileSystemHelpers
{
    public static class FileCollector
    {
        public static string[] GetAllFilesFrom(string path)
        {
            if (Directory.Exists(path))
                try
                {
                    return Directory.GetFiles(path).OrderBy(p => p).ToArray();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            else throw new Exception("Path does not exist!" + path);
        }

        public static string[] GetAllFilesRecursivelyFrom(string path)
        {
            return Directory.GetFiles(path, "*", SearchOption.AllDirectories)
                         .OrderBy(p => p).ToArray();
        }
    }
}
