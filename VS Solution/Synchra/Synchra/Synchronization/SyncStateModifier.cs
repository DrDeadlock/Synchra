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
            try
            {
                File.Copy(from, to);
                SynchronizationCommunicator.Instance
                    .InfoFileCreated(to);
            }
            catch (UnauthorizedAccessException ex)
            {
                SynchronizationCommunicator.Instance
                    .Error(ex.Message);
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
                SynchronizationCommunicator.Instance
                    .Error(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                SynchronizationCommunicator.Instance
                    .Error(ex.Message);
            }
            catch (IOException ex)
            {
                SynchronizationCommunicator.Instance
                    .Error(ex.Message);
            }
        }

        public static void UpdateFile(string from, string to)
        {
            try
            {
                File.Delete(to);
                File.Copy(from, to);
                SynchronizationCommunicator.Instance
                    .InfoFileUpdated(to);

            }
            catch (UnauthorizedAccessException ex)
            {
                SynchronizationCommunicator.Instance
                    .Error(ex.Message);
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
                SynchronizationCommunicator.Instance
                    .Error(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                SynchronizationCommunicator.Instance
                    .Error(ex.Message);
            }
            catch (IOException ex)
            {
                SynchronizationCommunicator.Instance
                    .Error(ex.Message);
            }
        }

        public static void DeleteFile(string at)
        {
            try
            {
                File.Delete(at);
                SynchronizationCommunicator.Instance
                    .InfoFileDeleted(at);
            }
            catch (UnauthorizedAccessException ex)
            {
                SynchronizationCommunicator.Instance
                    .Error(ex.Message);
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
                SynchronizationCommunicator.Instance
                    .Error(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                SynchronizationCommunicator.Instance
                    .Error(ex.Message);
            }
            catch (IOException ex)
            {
                SynchronizationCommunicator.Instance
                    .Error(ex.Message);
            }
        }

        public static void CreateDirectory(string at)
        {
            try
            {
                Directory.CreateDirectory(at);
                SynchronizationCommunicator.Instance
                    .InfoDirectoryCreated(at);
            }
            catch (UnauthorizedAccessException ex)
            {
                SynchronizationCommunicator.Instance
                    .Error(ex.Message);
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
                SynchronizationCommunicator.Instance
                    .Error(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                SynchronizationCommunicator.Instance
                    .Error(ex.Message);
            }
            catch (IOException ex)
            {
                SynchronizationCommunicator.Instance
                    .Error(ex.Message);
            }
        }

        public static void DeleteDirectory(string at)
        {
            try
            {
                Directory.Delete(at);
                SynchronizationCommunicator.Instance
                    .InfoDirectoryDeleted(at);
            }
            catch (UnauthorizedAccessException ex)
            {
                SynchronizationCommunicator.Instance
                    .Error(ex.Message);
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
                SynchronizationCommunicator.Instance
                    .Error(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                SynchronizationCommunicator.Instance
                    .Error(ex.Message);
            }
            catch (IOException ex)
            {
                SynchronizationCommunicator.Instance
                    .Error(ex.Message);
            }
        }
    }
}
