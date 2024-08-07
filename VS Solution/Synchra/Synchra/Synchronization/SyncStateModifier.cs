using System;
using System.IO;
using Synchra.Logging;
using Synchra.Logging.Wrappers;

namespace Synchra.Synchronization
{
    public static class SyncStateModifier
    {
        public static void CopyFile(string from, string to)
        {
            var comm = SynchronizationCommunicator.Instance;

            try
            {
                File.Copy(from, to);
                SynchronizationCommunicator.Instance
                    .InfoFileCreated(to);
            }
            catch (UnauthorizedAccessException ex)
            {
                comm.Error(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch(PathTooLongException ex)
            {
                throw ex;
            }
            catch (DirectoryNotFoundException ex)
            {
                comm.Error(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                comm.Error(ex.Message);
            }
            catch (IOException ex)
            {
                comm.Error(ex.Message);
            }
        }

        public static void UpdateFile(string from, string to)
        {
            var comm = SynchronizationCommunicator.Instance;

            try
            {
                File.Delete(to);
                File.Copy(from, to);
                SynchronizationCommunicator.Instance
                    .InfoFileUpdated(to);

            }
            catch (UnauthorizedAccessException ex)
            {
                comm.Error(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (PathTooLongException ex)
            {
                throw ex;
            }
            catch (DirectoryNotFoundException ex)
            {
                comm.Error(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                comm.Error(ex.Message);
            }
            catch (IOException ex)
            {
                comm.Error(ex.Message);
            }
        }

        public static void DeleteFile(string at)
        {
            var comm = SynchronizationCommunicator.Instance;

            try
            {
                File.Delete(at);
                SynchronizationCommunicator.Instance
                    .InfoFileDeleted(at);
            }
            catch (UnauthorizedAccessException ex)
            {
                comm.Error(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (PathTooLongException ex)
            {
                throw ex;
            }
            catch (DirectoryNotFoundException ex)
            {
                comm.Error(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                comm.Error(ex.Message);
            }
            catch (IOException ex)
            {
                comm.Error(ex.Message);
            }
        }

        public static void CreateDirectory(string at)
        {
            var comm = SynchronizationCommunicator.Instance;

            try
            {
                Directory.CreateDirectory(at);
                SynchronizationCommunicator.Instance
                    .InfoDirectoryCreated(at);
            }
            catch (UnauthorizedAccessException ex)
            {
                comm.Error(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (PathTooLongException ex)
            {
                throw ex;
            }
            catch (DirectoryNotFoundException ex)
            {
                comm.Error(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                comm.Error(ex.Message);
            }
            catch (IOException ex)
            {
                comm.Error(ex.Message);
            }
        }

        public static void DeleteDirectory(string at)
        {
            var comm = SynchronizationCommunicator.Instance;

            try
            {
                Directory.Delete(at);
                SynchronizationCommunicator.Instance
                    .InfoDirectoryDeleted(at);
            }
            catch (UnauthorizedAccessException ex)
            {
                comm.Error(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (PathTooLongException ex)
            {
                throw ex;
            }
            catch (DirectoryNotFoundException ex)
            {
                comm.Error(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                comm.Error(ex.Message);
            }
            catch (IOException ex)
            {
                comm.Error(ex.Message);
            }
        }
    }
}
