
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Synchra.CLAValidation;
using Synchra.FileSystemHelpers;
using Synchra.Logging.Wrappers;

namespace Synchra.Synchronization
{
    public static class SyncStateChecker
    {
        private static SynchronizationCommunicator comm;

        /// <summary>
        /// Check if two files represented by their full qualified path
        /// match per Checksum. 
        /// </summary>
        /// <param name="pSrcPath"></param>
        /// <param name="pDestPath"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Check if the directories by themselves and files contained in directories
        /// match per Checksum. 
        /// </summary>
        /// <param name="pSrcPath"></param>
        /// <param name="pDestPath"></param>
        /// <returns></returns>
        public static bool DirectoryOutOfSync(string pSrcPath, string pDestPath)
        {
            byte[] srcHash = new byte[0];
            byte[] destHash = new byte[0];
            try
            {
                if (!(Directory.Exists(pSrcPath) && Directory.Exists(pDestPath)))
                {
                    return true; 
                }
            }
            catch (DirectoryNotFoundException)
            {
                return true;
            }

            srcHash = GetHashOfFilesIn(pSrcPath);
            destHash = GetHashOfFilesIn(pDestPath);
            
            if (srcHash.Length != destHash.Length)
                return true;
            else
            {
                return !Enumerable.SequenceEqual(srcHash, destHash);
            }
        }

        private static byte[] GetHashOfFilesIn(string pPath)
        {
            if (comm == null) comm = new SynchronizationCommunicator();

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

                try
                {
                    byte[] contentBytes = File.ReadAllBytes(file);
                    if (i == files.Length - 1)
                        md5.TransformFinalBlock(contentBytes, 0, contentBytes.Length);
                    else
                        md5.TransformBlock(contentBytes, 0, contentBytes.Length, contentBytes, 0);
                }
                catch (UnauthorizedAccessException)
                {
                    comm.ErrorPermissionMissing(pPath);
                    return new byte[0];
                }
                catch (PathTooLongException)
                {
                    comm.ErrorPathTooLong(pPath);
                    return new byte[0];
                }
                catch (DirectoryNotFoundException)
                {
                    return new byte[0];
                }
                catch (IOException ex)
                {
                    throw ex;
                }
                
            }

            if (md5.Hash != null)
                return md5.Hash;
            else
                return new byte[0];
        }

        private static string GetLocalPath(string pPath, string pReferencePath)
        {
            int index = pPath.IndexOf(pReferencePath);
            string relativePath = (index < 0)
                ? pPath
                : pPath.Remove(index, pReferencePath.Length);

            return relativePath;

            //byte[] pathBytes = Encoding.UTF8.GetBytes(relativePath.ToLower());
            //using var md5 = MD5.Create();
            //md5.TransformBlock(pathBytes, 0, pathBytes.Length, pathBytes, 0);
            //Console.WriteLine("The PathBytes are: " + Encoding.Default.GetString(pathBytes));
            //return pathBytes;
        }

        /// <summary>
        /// Check if a File is contained in both directories.
        /// </summary>
        /// <param name="pPath1"></param>
        /// <param name="pPath2"></param>
        /// <param name="pFileName"></param>
        /// <returns></returns>
        public static bool BothContainFile(string pPath1, string pPath2, string pFileName)
        {
            return File.Exists(pPath1 + pFileName) && File.Exists(pPath2 + pFileName);
        }

        /// <summary>
        /// Check if a File is contained in both directories.
        /// </summary>
        /// <param name="pPath1"></param>
        /// <param name="pPathToFile2"></param>
        /// <param name="pFileName"></param>
        /// <returns></returns>
        public static bool BothContainFile(string pPathToFile1, string pPathToFile2)
        {
            return File.Exists(pPathToFile1) && File.Exists(pPathToFile2);
        }

        /// <summary>
        /// Check if a File is missing in both directories.
        /// </summary>
        /// <param name="pPath1"></param>
        /// <param name="pPath2"></param>
        /// <param name="pFileName"></param>
        /// <returns></returns>
        public static bool BothMissFile(string pPath1, string pPath2)
        {
            return !File.Exists(pPath1) && !File.Exists(pPath2);
        }

        /// <summary>
        /// Check if the directory of both paths exist.
        /// </summary>
        /// <param name="pPath1"></param>
        /// <param name="pPath2"></param>
        /// <param name="pFileName"></param>
        /// <returns></returns>
        public static bool BothContainDirectory(string pPath1, string pPath2, string localDir)
        {
            return Directory.Exists(pPath1 + localDir) && Directory.Exists(pPath2 + localDir);
        }

        /// <summary>
        /// Check if the directory of both paths exist.
        /// </summary>
        /// <param name="pPath1"></param>
        /// <param name="pPathToDir2"></param>
        /// <param name="pFileName"></param>
        /// <returns></returns>
        public static bool BothContainDirectory(string pPathToDir1, string pPathToDir2)
        {
            return Directory.Exists(pPathToDir1) && Directory.Exists(pPathToDir2);
        }

        /// <summary>
        /// Check if the directory of both paths is missing.
        /// </summary>
        /// <param name="pPath1"></param>
        /// <param name="pPathToDir2"></param>
        /// <param name="pFileName"></param>
        /// <returns></returns>
        public static bool BothMissDirectory(string pPathToDir1, string pPathToDir2)
        {
            return !Directory.Exists(pPathToDir1) && !Directory.Exists(pPathToDir2);
        }
    }
}
