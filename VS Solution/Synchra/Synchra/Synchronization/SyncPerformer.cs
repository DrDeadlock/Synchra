
using Synchra.FileSystemHelpers;

namespace Synchra.Synchronization
{
    public static class SyncPerformer
    {
        private static void Execute(string pSrc, string pDest, float waitInterval)
        {
            if (SyncStateChecker.DirectoryOutOfSync(pSrc, pDest))
            {
                string[] filesInDest = FileCollector.GetAllFilesFrom(pDest);


                string[] dirsInDest;
                string[] FilesInSrc;
                string[] dirsInSrc;
            }

            /*
                Get Checksum of pSrc and pDest (Use SyncStateChecker.DirectoryOutOfSync)
                    Are they different? -- Sync Necessary stays true
                    Are they the same? Then we are done. -- Sync Necessary flips to false

            If SyncNecessary is true: 
                Create Lists:
                a) all Files in pDest
                d) all SubDirectories in pDest
                b) all Files in pSrc
                c) all SubDirectories of pSrc                
                

            --> With a)
                Foreach File in pDest:
                    Does it also exist in pSrc?
                    Yes: Continue;
                    No: Delete the file from pDest

            --> With d)
                Foreach Dir in pDest:
                    Does it also exist in pSrc?
                    Yes: Continue;
                    No: Delete the Directory from pDest

            --> With b)
                Foreach File in pSrc
                Does it also exist in pDest?
                    Yes: Continue;
                    No: Create it in pDest with SyncStateModifier.CreateFile()

            --> With c)
                Foreach SubDirectory in pSrc:
                Execute(pSrc/SubDir, pDest/SubDir)                                           
            */
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
        /// <param name="timeInterval"></param>
        public static void TestExecute(string pSrc, string pDest, float timeInterval)
        {
            Execute(pSrc, pDest, timeInterval);
        }
    }
}
