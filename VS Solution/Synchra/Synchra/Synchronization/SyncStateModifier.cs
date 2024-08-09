using System;
using System.IO;
using Synchra.Logging.Wrappers;

namespace Synchra.Synchronization
{
    public static class SyncStateModifier
    {
        private const string COPY_FILE = "Copy File";
        private const string UPDATE_FILE = "Update File";
        private const string DELETE_FILE = "Delete File";
        private const string CREATE_DIRECTORY = "Creation of Directory";
        private const string REMOVE_DIRECTORY = "Remove Directory";

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
                comm.ErrorDuring(COPY_FILE, ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                //User Deletes file to copy while Synchra contains the file in a
                //foreach collection
                comm.ErrorDuring(COPY_FILE, ex.Message);
            }
            catch (ArgumentException ex)
            {
                comm.ErrorDuring(COPY_FILE, ex.Message);
            }
            catch(PathTooLongException ex)
            {
                comm.ErrorDuring(COPY_FILE, ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                comm.ErrorDuring(COPY_FILE, ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                comm.ErrorDuring(COPY_FILE, ex.Message);
            }
            catch (IOException ex)
            {
                comm.ErrorDuring(COPY_FILE, ex.Message);
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
                comm.ErrorDuring(UPDATE_FILE, ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                comm.ErrorDuring(UPDATE_FILE, ex.Message);
            }
            catch (ArgumentException ex)
            {
                comm.ErrorDuring(UPDATE_FILE, ex.Message);
            }
            catch (PathTooLongException ex)
            {
                comm.ErrorDuring(UPDATE_FILE, ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                comm.ErrorDuring(UPDATE_FILE, ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                comm.ErrorDuring(UPDATE_FILE, ex.Message);
            }
            catch (IOException ex)
            {
                comm.ErrorDuring(UPDATE_FILE, ex.Message);
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
                comm.ErrorDuring(DELETE_FILE, ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                comm.ErrorDuring(DELETE_FILE, ex.Message);
            }
            catch (ArgumentException ex)
            {
                comm.ErrorDuring(DELETE_FILE, ex.Message);
            }
            catch (PathTooLongException ex)
            {
                comm.ErrorDuring(DELETE_FILE, ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                comm.ErrorDuring(DELETE_FILE, ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                comm.ErrorDuring(DELETE_FILE, ex.Message);
            }
            catch (IOException ex)
            {
                comm.ErrorDuring(DELETE_FILE, ex.Message);
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
                comm.ErrorDuring(CREATE_DIRECTORY, ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                comm.ErrorDuring(CREATE_DIRECTORY, ex.Message);
            }
            catch (ArgumentException ex)
            {
                comm.ErrorDuring(CREATE_DIRECTORY, ex.Message);
            }
            catch (PathTooLongException ex)
            {
                comm.ErrorDuring(CREATE_DIRECTORY, ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                comm.ErrorDuring(CREATE_DIRECTORY, ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                comm.ErrorDuring(CREATE_DIRECTORY, ex.Message);
            }
            catch (IOException ex)
            {
                comm.ErrorDuring(CREATE_DIRECTORY, ex.Message);
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
                comm.ErrorDuring(REMOVE_DIRECTORY, ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                comm.ErrorDuring(REMOVE_DIRECTORY, ex.Message);
            }
            catch (ArgumentException ex)
            {
                comm.ErrorDuring(REMOVE_DIRECTORY, ex.Message);
            }
            catch (PathTooLongException ex)
            {
                comm.ErrorDuring(REMOVE_DIRECTORY, ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                comm.ErrorDuring(REMOVE_DIRECTORY, ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                comm.ErrorDuring(REMOVE_DIRECTORY, ex.Message);
            }
            catch (IOException ex)
            {
                comm.ErrorDuring(REMOVE_DIRECTORY, ex.Message);
            }
        }
    }
}
