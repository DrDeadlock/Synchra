using System;
using System.IO;
using System.Linq;
using Synchra.CLAValidation;
using Synchra.Logging.Wrappers;

namespace Synchra.FileSystemHelpers
{
    public static class FileCollector
    {
        private static SynchronizationCommunicator comm;

        public static string[] GetAllFilesFrom(string pPath)
        {
            if (comm == null) comm = new SynchronizationCommunicator();

            try
            {
                return Directory.GetFiles(pPath).OrderBy(p => p).ToArray();
            }
            catch (UnauthorizedAccessException)
            {
                SynchronizationCommunicator comm = new SynchronizationCommunicator();
                comm.ErrorPermissionMissing(pPath);
                return new string[0];
            }
            catch (PathTooLongException)
            {
                comm.ErrorPathTooLong(pPath);
                return new string[0];
            }
            catch (DirectoryNotFoundException)
            {
                if ((pPath.SequenceEqual(CLAContext.Instance.srcPath))
                || (pPath.SequenceEqual(CLAContext.Instance.destPath)))
                {
                    Directory.CreateDirectory(pPath);
                    comm.WarnSrcOrDestNewCreated();
                }
                return new string[0];
            }
            catch (IOException ex)
            {
                throw ex;
            }
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
