using System;
using System.IO;
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
                /* The user changes permissions for the file or destination directory
                 */
                comm.Error(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                //User Deletes file to copy while Synchra contains the file in a
                //foreach collection
                comm.Error(ex.Message);
            }
            catch (ArgumentException ex)
            {
                comm.Error(ex.Message);
            }
            catch(PathTooLongException ex)
            {
                comm.Error(ex.Message);
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
                comm.Error(ex.Message);
            }
            catch (ArgumentException ex)
            {
                comm.Error(ex.Message);
            }
            catch (PathTooLongException ex)
            {
                comm.Error(ex.Message);
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
                comm.Error(ex.Message);
            }
            catch (ArgumentException ex)
            {
                comm.Error(ex.Message);
            }
            catch (PathTooLongException ex)
            {
                comm.Error(ex.Message);
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
                comm.Error(ex.Message);
            }
            catch (ArgumentException ex)
            {
                comm.Error(ex.Message);
            }
            catch (PathTooLongException ex)
            {
                comm.Error(ex.Message);
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
                comm.Error(ex.Message);
            }
            catch (ArgumentException ex)
            {
                comm.Error(ex.Message);
            }
            catch (PathTooLongException ex)
            {
                comm.Error(ex.Message);
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
