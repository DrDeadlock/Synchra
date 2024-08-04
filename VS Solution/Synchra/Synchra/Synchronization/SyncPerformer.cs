
using System.Threading;
using Synchra.FileSystemHelpers;

namespace Synchra.Synchronization
{
    public static class SyncPerformer
    {
        private static void Execute(string pSrc, string pDest, int waitForSeconds)
        {
            if (SyncStateChecker.DirectoryOutOfSync(pSrc, pDest))
            {
                ClearExcessFilesInDest(pSrc, pDest, waitForSeconds);
                ClearExcessDirsInDest(pSrc, pDest, waitForSeconds);
                CreateAndUpdateFiles(pSrc, pDest, waitForSeconds);
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

        private static void WaitFor(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

        private static void ClearExcessFilesInDest(string pSrc, string pDest, int seconds)
        {
            string[] filesInDest = FileCollector.GetAllFilesFrom(pDest);
            foreach (var file in filesInDest)
            {
                if (seconds > 0)
                    WaitFor(seconds);

                string fileLocalPath = PathConversion.MakePathLocal
                    (file, pDest);

                if (!SyncStateChecker.BothContain(
                        pSrc, pDest, fileLocalPath))
                {
                    SyncStateModifier.DeleteFile(file);
                }
            }
        }

        private static void ClearExcessDirsInDest(string pSrc, string pDest, int seconds)
        {
            string[] dirsInDest = FileCollector.GetSubDirectories(pDest);
            foreach (var dir in dirsInDest)
            {
                if (seconds > 0)
                    WaitFor(seconds);

                string dirLocalPath = PathConversion.MakePathLocal
                    (dir, pDest);
                if (!SyncStateChecker.BothContain(
                    pSrc, pDest, dirLocalPath))
                {
                    SyncStateModifier.DeleteDirectory(dir);
                }
            }
        }

        private static void CreateAndUpdateFiles(string pSrc, string pDest, int seconds)
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

                if (!SyncStateChecker.BothContain
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
