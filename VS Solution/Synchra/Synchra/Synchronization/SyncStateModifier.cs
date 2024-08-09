using System;
using System.IO;
using Synchra.FileSystemHelpers;
using Synchra.Logging.Wrappers;

namespace Synchra.Synchronization
{
    public enum ModificationType
    {
        CopyFile, UpdateFile, DeleteFile, CreateDirectory, RemoveDirectory
    }

    public static class SyncStateModifier
    {
        private const string COPY_FILE = "Copy File";
        private const string UPDATE_FILE = "Update File";
        private const string DELETE_FILE = "Delete File";
        private const string CREATE_DIRECTORY = "Creation of Directory";
        private const string REMOVE_DIRECTORY = "Remove Directory";

        public static void Modify
            (ModificationType modType, string fromOrAt, string to = "")
        {
            Action<string, string> referenceModification = null;
            Action<string> singleModification = null;
            string currentAction = "";
            switch (modType)
            {
                case ModificationType.CopyFile:
                    referenceModification = CopyFile;
                    currentAction = COPY_FILE;
                    break;
                case ModificationType.UpdateFile:
                    referenceModification = UpdateFile;
                    currentAction = UPDATE_FILE;
                    break;
                case ModificationType.DeleteFile:
                    singleModification = DeleteFile;
                    currentAction = DELETE_FILE;
                    break;
                case ModificationType.CreateDirectory:
                    singleModification = CreateDirectory;
                    currentAction = CREATE_DIRECTORY;
                    break;
                case ModificationType.RemoveDirectory:
                    singleModification = DeleteDirectory;
                    currentAction = REMOVE_DIRECTORY;
                    break;
            }

            var comm = SynchronizationCommunicator.Instance;
            string fileName = PathConversion.GetFileName(fromOrAt);
            try
            {
                if (singleModification != null)
                {
                    singleModification(fromOrAt);
                    comm.InfoModification(currentAction, fileName);
                }

                if (referenceModification != null)
                {
                    referenceModification(fromOrAt, to);
                    comm.InfoModification(currentAction, fileName);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                /* The user changes permissions for the file or destination directory
                 */
                comm.Error(currentAction, ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                //User Deletes file to copy while Synchra contains the file in a
                //foreach collection
                comm.Error(currentAction, ex.Message);
            }
            catch (ArgumentException ex)
            {
                comm.Error(currentAction, ex.Message);
            }
            catch (PathTooLongException ex)
            {
                comm.Error(currentAction, ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                comm.Error(currentAction, ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                comm.Error(currentAction, ex.Message);
            }
            catch (IOException ex)
            {
                comm.Error(currentAction, ex.Message);
            }
        }

        private static void CopyFile(string from, string to)
        {            
            File.Copy(from, to);
        }

        private static void UpdateFile(string from, string to)
        {            
            File.Delete(to);
            File.Copy(from, to);        
        }

        private static void DeleteFile(string at)
        {            
            File.Delete(at);
        }

        private static void CreateDirectory(string at)
        {            
            Directory.CreateDirectory(at);
        }

        private static void DeleteDirectory(string at)
        {            
            Directory.Delete(at);          
        }
    }
}
