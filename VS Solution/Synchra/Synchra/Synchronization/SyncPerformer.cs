
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using Synchra.FileSystemHelpers;
using Synchra.Logging.Wrappers;

[assembly: InternalsVisibleTo("TestSynchra")]
namespace Synchra.Synchronization
{
    public static class SyncPerformer
    {
        private static void Execute(string srcPath, string destPath, int waitForSeconds)
        {
            SynchronizationCommunicator comm
                = SynchronizationCommunicator.Instance;

            comm.InfoText("Execute started!");
            if (SyncStateChecker.DirectoryOutOfSyncRecursively(srcPath, destPath))
            {
                comm.InfoText("Directories out of sync.");
                ClearExcessFilesInDestRecursively(srcPath, destPath, waitForSeconds);
                ClearExcessDirsInDestRecursively(srcPath, destPath, waitForSeconds);
                CreateAndUpdateFilesRecursively(srcPath, destPath, waitForSeconds);
                CreateDirectoriesRecursively(srcPath, destPath, waitForSeconds);
                return;
            }
            comm.InfoText("Execute completed!");
        }

        /// <summary>
        /// Performs the One Way Synchronization from Source to Destination.
        /// </summary>
        /// <param name="srcPath"></param>
        /// <param name="destPath"></param>
        public static void Execute(string srcPath, string destPath)
        {
            Execute(srcPath, destPath, 0);
        }

        internal static void WaitFor(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

        internal static void ClearExcessFilesInDest
            (string srcPath, string destPath, int seconds = 0)
        {
            string[] filesInDest = FileCollector.GetAllFilesFrom(destPath);
            foreach (var file in filesInDest)
            {
                if (seconds > 0)
                    WaitFor(seconds);

                string fileLocalPath = PathConversion.MakePathLocal
                    (file, destPath);

                if (!SyncStateChecker.BothContainFile(
                        srcPath, destPath, fileLocalPath))
                {
                    SyncStateModifier.DeleteFile(file);
                }
            }
        }

        internal static void ClearExcessFilesInDestRecursively
            (string srcPath, string destPath, int seconds = 0)
        {
            string[] directories = FileCollector.GetSubDirectories(destPath);
            foreach (var dir in directories)
            {
                string dirLocalPath = PathConversion.MakePathLocal
                    (dir, destPath);

                ClearExcessFilesInDestRecursively(
                    srcPath + dirLocalPath, destPath + dirLocalPath, seconds);
            }

            //Reaching this point means, no SubDir was found 
            ClearExcessFilesInDest(srcPath, destPath, 0);
        }

        internal static void ClearExcessDirsInDest
            (string srcPath, string destPath, int seconds = 0)
        {
            string[] dirsInDest = FileCollector.GetSubDirectories(destPath);
            foreach (var dir in dirsInDest)
            {
                if (seconds > 0)
                    WaitFor(seconds);

                string dirLocalPath = PathConversion.MakePathLocal
                    (dir, destPath);

                if (!SyncStateChecker.BothContainDirectory(
                    srcPath, destPath, dirLocalPath))
                {
                    //TODO: We have to iterate through this directory
                    //And clear sub dirs and files as well...
                    SyncStateModifier.DeleteDirectory(dir);
                }
            }
        }

        internal static void ClearExcessDirsInDestRecursively
            (string srcPath, string destPath, int seconds = 0)
        {
            string[] directories = FileCollector.GetSubDirectories(destPath);
            foreach (var dir in directories)
            {
                string dirLocalPath = PathConversion.MakePathLocal
                    (dir, destPath);

                ClearExcessDirsInDestRecursively(
                    srcPath + dirLocalPath, destPath + dirLocalPath, seconds);
            }

            //Reaching this point means, no SubDir was found 
            ClearExcessDirsInDest(srcPath, destPath, 0);
        }

        internal static void CreateAndUpdateFiles
            (string srcPath, string destPath, int seconds = 0)
        {
            string[] filesInSrc = FileCollector.GetAllFilesFrom(srcPath);
            foreach (var file in filesInSrc)
            {
                if (seconds > 0)
                    WaitFor(seconds);

                string fileLocalPath = PathConversion.MakePathLocal
                    (file, srcPath);
                string fileInSrc = srcPath + fileLocalPath;
                string fileInDest = destPath + fileLocalPath;

                if (!SyncStateChecker.BothContainFile
                    (srcPath, destPath, fileLocalPath))
                {
                    SyncStateModifier.CopyFile(
                        fileInSrc, fileInDest);
                }
                else
                {
                    if (SyncStateChecker.FileOutOfSync
                        (fileInSrc, fileInDest))
                    {
                        SyncStateModifier.UpdateFile
                            (fileInSrc, fileInDest);
                    }
                }
            }
        }

        internal static void CreateAndUpdateFilesRecursively
            (string srcPath, string destPath, int seconds = 0)
        {
            string[] directories = FileCollector.GetSubDirectories(srcPath);
            foreach (var dir in directories)
            {
                string dirLocalPath = PathConversion.MakePathLocal
                    (dir, srcPath);

                CreateAndUpdateFilesRecursively(
                    srcPath + dirLocalPath, destPath + dirLocalPath, seconds);
            }

            //Reaching this point means, no SubDir was found 
            CreateAndUpdateFiles(srcPath, destPath, 0);
        }

        internal static void CreateDirectories
            (string srcPath, string destPath, int seconds = 0)
        {
            string[] subDirsInSrc = FileCollector.GetSubDirectories(srcPath);
            foreach (var subDir in subDirsInSrc)
            {
                if (seconds > 0)
                    WaitFor(seconds);

                string subDirLocalPath = PathConversion.MakePathLocal
                    (subDir, srcPath);
                string subDirInSrc = srcPath + subDirLocalPath;
                string subDirInDest = destPath + subDirLocalPath;

                if (!SyncStateChecker.BothDirectoriesExist(subDirInSrc, subDirInDest))
                {
                    SyncStateModifier.CreateDirectory(subDirInDest);
                }
            }
        }

        internal static void CreateDirectoriesRecursively
            (string srcPath, string destPath, int seconds = 0)
        {
            string[] directories = FileCollector.GetSubDirectories(srcPath);
            foreach (var dir in directories)
            {
                string dirLocalPath = PathConversion.MakePathLocal
                    (dir, srcPath);

                CreateDirectoriesRecursively(
                    srcPath + dirLocalPath, destPath + dirLocalPath, seconds);
            }

            //Reaching this point means, no SubDir was found 
            CreateDirectories(srcPath, destPath, 0);
        }



        private static void Execute(string srcPath, string destPath, int waitForSeconds, bool useAlternative)
        {
            //Alternative Method to spare code. 
            if (SyncStateChecker.DirectoryOutOfSync(srcPath, destPath))
            {
                Action<string, string, int> clearFilesOnDest = ClearExcessFilesInDest;
                PerformRecurisvelyOn(srcPath, destPath, waitForSeconds, clearFilesOnDest, false);
                Action<string, string, int> clearDirsOnDest = ClearExcessDirsInDest;
                PerformRecurisvelyOn(srcPath, destPath, waitForSeconds, clearDirsOnDest, false);
                Action<string, string, int> createAndUpdateFiles = CreateAndUpdateFiles;
                PerformRecurisvelyOn(srcPath, destPath, waitForSeconds, clearDirsOnDest, false);
                Action<string, string, int> createDirectories = CreateDirectories;
                PerformRecurisvelyOn(srcPath, destPath, waitForSeconds, clearDirsOnDest, false);
                return;
            }
        }

        //This method is a conjunction of all seperate (DoStuff)Recursively Methods. 
        internal static void PerformRecurisvelyOn
                (string srcPath, string destPath, int seconds,
                Action<string, string, int> perform, bool performOnSource)
        {
            string[] directories;
            if (performOnSource)
                directories = FileCollector.GetSubDirectories(srcPath);
            else
                directories = FileCollector.GetSubDirectories(destPath);

            foreach (var dir in directories)
            {
                string dirLocalPath = PathConversion.MakePathLocal
                    (dir, srcPath);

                PerformRecurisvelyOn(
                    srcPath + dirLocalPath,
                    destPath + dirLocalPath,
                    seconds,
                    perform,
                    performOnSource);
            }

            perform.Invoke(srcPath, destPath, seconds);
        }
    }
}
