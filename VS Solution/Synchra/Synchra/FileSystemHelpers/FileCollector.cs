using System;
using System.IO;
using System.Linq;

namespace Synchra.FileSystemHelpers
{
    public static class FileCollector
    {
        public static string[] GetAllFilesFrom(string pPath)
        {
            if (Directory.Exists(pPath))
                try
                {
                    return Directory.GetFiles(pPath).OrderBy(p => p).ToArray();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            else throw new Exception("Path does not exist!" + pPath);
        }

        public static string[] GetAllFilesRecursivelyFrom(string pPath)
        {
            return Directory.GetFiles(pPath, "*", SearchOption.AllDirectories)
                         .OrderBy(p => p).ToArray();
        }

        public static bool HasSubDirectories(string path)
        {
            return Directory.EnumerateDirectories(path).Count() > 0;
        }

        public static string[] GetSubDirectories(string pPath)
        {
            return Directory.EnumerateDirectories(pPath)
                    .OrderBy(p => p).ToArray();
        }        
    }
}
