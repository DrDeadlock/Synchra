
using System.Runtime.CompilerServices;
using System.Threading;
using Synchra.FileSystemHelpers;

[assembly: InternalsVisibleTo("TestSynchra")]
namespace Synchra.Synchronization
{
    public static class SyncPerformer
    {
        private static void Execute(string pSrc, string pDest, int waitForSeconds)
        {
            if (SyncStateChecker.DirectoryOutOfSync(pSrc, pDest))
            {
                ClearExcessFilesInDestRecursively(pSrc, pDest, waitForSeconds);
                ClearExcessDirsInDestRecursively(pSrc, pDest, waitForSeconds);
                CreateAndUpdateFiles(pSrc, pDest, waitForSeconds);
                CreateDirectories(pSrc, pDest, waitForSeconds);
                IterateSubDirectories(pSrc, pDest, waitForSeconds);                
            }
        }

        /// <summary>
        /// Performs the One Way Synchronization from Source to Destination.
        /// </summary>
        /// <param name="pSrc"></param>
        /// <param name="pDest"></param>
        public static void Execute(string pSrc, string pDest)
        {
            Execute(pSrc, pDest, 0);
        }

        /// <summary>
        /// Don't use this method in productive code!
        /// It's only for Testing purposes.
        /// </summary>
        /// <param name="pSrc"></param>
        /// <param name="pDest"></param>
        /// <param name="timeIntervalSeconds"></param>
        public static void TestExecute(string pSrc, string pDest, int timeIntervalSeconds)
        {
            Execute(pSrc, pDest, timeIntervalSeconds);
        }

        internal static void WaitFor(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

        internal static void ClearExcessFilesInDest(string pSrc, string pDest, int seconds)
        {
            string[] filesInDest = FileCollector.GetAllFilesFrom(pDest);
            foreach (var file in filesInDest)
            {
                if (seconds > 0)
                    WaitFor(seconds);

                string fileLocalPath = PathConversion.MakePathLocal
                    (file, pDest);

                if (!SyncStateChecker.BothContainFile(
                        pSrc, pDest, fileLocalPath))
                {
                    SyncStateModifier.DeleteFile(file);
                }
            }
        }

        internal static void ClearExcessFilesInDestRecursively
            (string pSrc, string pDest, int seconds)
        {
            string[] directories = FileCollector.GetSubDirectories(pDest);
            foreach (var dir in directories)
            {
                string dirLocalPath = PathConversion.MakePathLocal
                    (dir, pDest);

                ClearExcessFilesInDestRecursively(
                    pSrc + dirLocalPath, pDest + dirLocalPath, seconds);
            }

            //Reaching this point means, no SubDir was found 
            ClearExcessFilesInDest(pSrc, pDest, 0);
        }

        internal static void ClearExcessDirsInDest(string pSrc, string pDest, int seconds)
        {
            string[] dirsInDest = FileCollector.GetSubDirectories(pDest);
            foreach (var dir in dirsInDest)
            {
                if (seconds > 0)
                    WaitFor(seconds);

                string dirLocalPath = PathConversion.MakePathLocal
                    (dir, pDest);

                if (!SyncStateChecker.BothContainDirectory(
                    pSrc, pDest, dirLocalPath))
                {
                    //TODO: We have to iterate through this directory
                    //And clear sub dirs and files as well...
                    SyncStateModifier.DeleteDirectory(dir);
                }
            }
        }

        internal static void ClearExcessDirsInDestRecursively
            (string pSrc, string pDest, int seconds)
        {
            string[] directories = FileCollector.GetSubDirectories(pDest);
            foreach (var dir in directories)
            {
                string dirLocalPath = PathConversion.MakePathLocal
                    (dir, pDest);

                ClearExcessDirsInDestRecursively(
                    pSrc + dirLocalPath, pDest + dirLocalPath, seconds);
            }

            //Reaching this point means, no SubDir was found 
            ClearExcessDirsInDest(pSrc, pDest, 0);
        }

        internal static void CreateAndUpdateFiles(string pSrc, string pDest, int seconds)
        {
            string[] filesInSrc = FileCollector.GetAllFilesFrom(pSrc);
            foreach (var file in filesInSrc)
            {
                if (seconds > 0)
                    WaitFor(seconds);

                string fileLocalPath = PathConversion.MakePathLocal
                    (file, pSrc);
                string fileInSrc = pSrc + fileLocalPath;
                string fileInDest = pDest + fileLocalPath;

                if (!SyncStateChecker.BothContainFile
                    (pSrc, pDest, fileLocalPath))
                {
                    SyncStateModifier.CreateFile(
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

        internal static void CreateDirectories(string pSrc, string pDest, int seconds)
        {
            string[] subDirsInSrc = FileCollector.GetSubDirectories(pSrc);
            foreach (var subDir in subDirsInSrc)
            {
                if (seconds > 0)
                    WaitFor(seconds);

                string subDirLocalPath = PathConversion.MakePathLocal
                    (subDir, pSrc);
                string subDirInSrc = pSrc + subDirLocalPath;
                string subDirInDest = pDest + subDirLocalPath;

                if (!SyncStateChecker.BothContainDirectory(subDirInSrc, subDirInDest))
                {
                    SyncStateModifier.CreateDirectory(subDirInDest);
                }
            }
        }

        private static void IterateSubDirectories(string pSrc, string pDest, int seconds)
        {
            string[] subDirsInSrc = FileCollector.GetSubDirectories(pSrc);
            foreach (var subDir in subDirsInSrc)
            {
                if (seconds > 0)
                    WaitFor(seconds);

                string subDirLocalPath = PathConversion.MakePathLocal
                    (subDir, pSrc);
                string subDirInSrc = pSrc + subDirLocalPath;
                string subDirInDest = pDest + subDirLocalPath;

                if (SyncStateChecker.DirectoryOutOfSync(subDirInSrc, subDirInDest))
                {
                    Execute(subDirInSrc, subDirInDest, seconds);
                }
            }
        }
    }
}
