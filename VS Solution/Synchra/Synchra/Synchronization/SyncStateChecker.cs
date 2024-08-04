
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Synchra.FileSystemHelpers;

namespace Synchra.Synchronization
{
    public static class SyncStateChecker
    {
        public static bool FileOutOfSync(string pSrcPath, string  pDestPath)
        {
            byte[] srcHash;
            byte[] destHash;

            using var md5 = MD5.Create();
            try
            {
                using (var srcFs = File.OpenRead(pSrcPath))
                {
                    srcHash = md5.ComputeHash(srcFs);
                }

                using (var destFs = File.OpenRead(pDestPath))
                {
                    destHash = md5.ComputeHash(destFs);
                }            
            }
            catch(FileNotFoundException)
            {
                return true;
            }

            if (srcHash.Length != destHash.Length)
                return true;
            else
            {
                return !Enumerable.SequenceEqual(srcHash, destHash);
            }
        }

        public static bool DirectoryOutOfSync(string pSrcPath, string pDestPath)
        {
            byte[] srcHash;
            byte[] destHash;
            try
            {
                srcHash = GetHashOfFilesIn(pSrcPath);
                destHash = GetHashOfFilesIn(pDestPath);

            }
            catch (DirectoryNotFoundException)
            {
                return true; 
            }

            if (srcHash.Length != destHash.Length)
                return true;
            else
            {
                return !Enumerable.SequenceEqual(srcHash, destHash);
            }
        }

        private static byte[] GetHashOfFilesIn(string pPath)
        {
            using var md5 = MD5.Create();
            byte[] srcHash = md5.Hash;
            string[] files =
                FileCollector.GetAllFilesRecursivelyFrom
                (
                    pPath
                );

            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];

                // hash path
                string relativePath = file.Substring(pPath.Length + 1);
                byte[] pathBytes = Encoding.UTF8.GetBytes(relativePath.ToLower());
                md5.TransformBlock(pathBytes, 0, pathBytes.Length, pathBytes, 0);

                // hash contents
                byte[] contentBytes = File.ReadAllBytes(file);
                if (i == files.Length - 1)
                    md5.TransformFinalBlock(contentBytes, 0, contentBytes.Length);
                else
                    md5.TransformBlock(contentBytes, 0, contentBytes.Length, contentBytes, 0);
            }

            return md5.Hash;
        }

        public static bool BothContain(string pPath1, string pPath2, string pFileName)
        {
            return File.Exists(pPath1 + pFileName) && File.Exists(pPath2 + pFileName);
        }
    }
}
