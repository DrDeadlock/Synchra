
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
        /// <param name="srcPath"></param>
        /// <param name="destPath"></param>
        /// <returns></returns>
        public static bool FileOutOfSync(string srcPath, string  destPath)
        {
            byte[] srcHash;
            byte[] destHash;

            using var md5 = MD5.Create();
            try
            {
                using (var srcFs = File.OpenRead(srcPath))
                {
                    srcHash = md5.ComputeHash(srcFs);
                }

                using (var destFs = File.OpenRead(destPath))
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
        /// <param name="srcPath"></param>
        /// <param name="destPath"></param>
        /// <returns></returns>
        public static bool DirectoryOutOfSync(string srcPath, string destPath)
        {
            byte[] srcHash = new byte[0];
            byte[] destHash = new byte[0];
            try
            {
                //This behaviour is necessary for testing!
                if (BothMissDirectory(srcPath, destPath))
                    return false;

                if (!(Directory.Exists(srcPath) && Directory.Exists(destPath)))
                {
                    return true; 
                }
            }
            catch (DirectoryNotFoundException)
            {
                return true;
            }

            srcHash = GetHashOfFilesIn(srcPath);
            destHash = GetHashOfFilesIn(destPath);
            
            if (srcHash.Length != destHash.Length)
                return true;
            else
            {
                return !Enumerable.SequenceEqual(srcHash, destHash);
            }
        }

        public static bool DirectoryOutOfSyncRecursively(string srcPath, string destPath)
        {
            bool outOfSync = DirectoryOutOfSync(srcPath, destPath);
            if (outOfSync)
                return true;

            string[] directories
                = FileCollector.GetSubDirectories(srcPath);

            foreach (var subDir in directories)
            {
                string localDirPath =
                    PathConversion.MakePathLocal(subDir, srcPath);

                if (DirectoryOutOfSyncRecursively
                    (srcPath + localDirPath, destPath + localDirPath))
                    return true;
            }

            directories
                = FileCollector.GetSubDirectories(destPath);

            foreach (var subDir in directories)
            {
                string localDirPath =
                    PathConversion.MakePathLocal(subDir, destPath);

                if (DirectoryOutOfSyncRecursively
                    (srcPath + localDirPath, destPath + localDirPath))
                    return true;
            }

            return false; 
        }

        private static byte[] GetHashOfFilesIn(string path)
        {
            if (comm == null) comm = new SynchronizationCommunicator();

            using var md5 = MD5.Create();
            byte[] srcHash = md5.Hash;
            string[] files =
                FileCollector.GetAllFilesRecursivelyFrom
                (
                    path
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
                    comm.ErrorPermissionMissing(path);
                    return new byte[0];
                }
                catch (PathTooLongException)
                {
                    comm.ErrorPathTooLong(path);
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

        /// <summary>
        /// Returns the part of the path which is not contained in reference path.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="referencePath"></param>
        /// <returns></returns>
        private static string GetLocalPath(string path, string referencePath)
        {
            int index = path.IndexOf(referencePath);
            string relativePath = (index < 0)
                ? path
                : path.Remove(index, referencePath.Length);

            return relativePath;
        }

        /// <summary>
        /// Check if a File with a given filename is contained in both directories.
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool BothContainFile(string path1, string path2, string fileName)
        {
            return File.Exists(path1 + fileName) && File.Exists(path2 + fileName);
        }

        /// <summary>
        /// Check if a full qualified Filepath is contained in both directories.
        /// </summary>
        /// <param name="pPath1"></param>
        /// <param name="pathToFile2"></param>
        /// <param name="pFileName"></param>
        /// <returns></returns>
        public static bool BothContainFile(string pathToFile1, string pathToFile2)
        {
            return File.Exists(pathToFile1) && File.Exists(pathToFile2);
        }

        /// <summary>
        /// Check if a full qualified Filepath is missing in both directories.
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <param name="pFileName"></param>
        /// <returns></returns>
        public static bool BothMissFile(string path1, string path2)
        {
            return !File.Exists(path1) && !File.Exists(path2);
        }

        /// <summary>
        /// Check if a directory with a given directory name is contained in both paths.
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <param name="pFileName"></param>
        /// <returns></returns>
        public static bool BothContainDirectory(string path1, string path2, string localDir)
        {
            return Directory.Exists(path1 + localDir) && Directory.Exists(path2 + localDir);
        }

        /// <summary>
        /// Check if both given directories exist.
        /// </summary>
        /// <param name="pPath1"></param>
        /// <param name="pathToDir2"></param>
        /// <param name="pFileName"></param>
        /// <returns></returns>
        public static bool BothDirectoriesExist(string pathToDir1, string pathToDir2)
        {
            return Directory.Exists(pathToDir1) && Directory.Exists(pathToDir2);
        }

        /// <summary>
        /// Check if both given directories are missing.
        /// </summary>
        /// <param name="pPath1"></param>
        /// <param name="pathToDir2"></param>
        /// <param name="pFileName"></param>
        /// <returns></returns>
        public static bool BothMissDirectory(string pathToDir1, string pathToDir2)
        {
            return !Directory.Exists(pathToDir1) && !Directory.Exists(pathToDir2);
        }
    }
}
