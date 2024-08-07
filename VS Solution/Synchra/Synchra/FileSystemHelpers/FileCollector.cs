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
            comm = SynchronizationCommunicator.Instance;

            try
            {
                return Directory.GetFiles(pPath).OrderBy(p => p).ToArray();
            }
            catch (UnauthorizedAccessException ex)
            {
                comm.Error(ex.Message);
                return new string[0];
            }
            catch (PathTooLongException ex)
            {
                comm.Error(ex.Message);
                return new string[0];
            }
            catch (DirectoryNotFoundException)
            {
                if ((pPath.SequenceEqual(CLAContext.Instance.SrcPath))
                || (pPath.SequenceEqual(CLAContext.Instance.DestPath)))
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
            if (Directory.Exists(pPath))
                return Directory.GetFiles(pPath, "*", SearchOption.AllDirectories)
                         .OrderBy(p => p).ToArray();

            return new string[0];
        }

        public static string[] GetSubDirectories(string pPath)
        {
            if (Directory.Exists(pPath))
                return Directory.EnumerateDirectories(pPath)
                    .OrderBy(p => p).ToArray();

            return new string[0];
        }                
    }
}
