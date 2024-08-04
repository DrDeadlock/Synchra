using System;
using System.IO;
using System.Text;

namespace TestSynchra.FileSystemHelpers
{
    public static class FileSystemTestSetup
    {
        public static void CreateTestStructure(string subdir)
        {
            FilesAndDirs.SUBDIR = subdir;

            CreateRootTxtSameContentInBoth();
            CreateRootTxtDifferentContentInBoth();
            CreateRootMissingTxtsInBoth();

            CreateRootEmptyDirectoryEqualInBoth();
            CreateRootEmptyDirectoryDifferentInBoth();
            CreateRootEmptyDirectoryMissingInBoth();
        }

        private static void CreateRootTxtSameContentInBoth()
        {
            CreateSameContentTxt
                (FilesAndDirs.RootTxtEqualContentInBoth(Direction.Source));
            CreateSameContentTxt
                (FilesAndDirs.RootTxtEqualContentInBoth(Direction.Destination));
        }


        public static void CreateSameContentTxt(string path)
        {
            SpawnDirectory(path);
            string fileContent = "This Content is equal in Src and Dest";

            using var fs = File.Create(path);            
            fs.Write(Encoding.ASCII.GetBytes(fileContent));
        }

        public static void CreateDifferentContentTxt(string path, string content)
        {
            SpawnDirectory(path);

            using var fs = File.Create(path);
            fs.Write(Encoding.ASCII.GetBytes(content));
        }

        private static DirectoryInfo SpawnDirectory(string path)
        {
            string pathToDirectory = FilesAndDirs.GetPathToFile(path);

            if (!Directory.Exists(pathToDirectory))
                return Directory.CreateDirectory(pathToDirectory);

            return null;
        }

        private static void SpawnDirectory(string path, DateTime pLastWriteTime)
        {
            DirectoryInfo info = SpawnDirectory(path);            

            if (info != null)
                info.LastWriteTime = pLastWriteTime;
        }

        private static void CreateRootTxtDifferentContentInBoth()
        {
            CreateDifferentContentTxt
                (FilesAndDirs.RootTxtDifferentContentInBoth(Direction.Source),
                "Source Directory Content");

            CreateDifferentContentTxt
                (FilesAndDirs.RootTxtDifferentContentInBoth(Direction.Destination),
                "Destination Directory Content");
        }

        private static void CreateRootMissingTxtsInBoth()
        {
            CreateDifferentContentTxt
                (FilesAndDirs.RootTxtMissingInSrc(Direction.Destination),
                "This file is not contained in Root Src.");

            CreateDifferentContentTxt
                (FilesAndDirs.RootTxtMissingInDest(Direction.Source),
                "This file is not contained in Root Dest.");
        }

        private static void CreateRootEmptyDirectoryEqualInBoth()
        {
            SpawnDirectory
                (FilesAndDirs.RootEmptyDirectoryEqualInBoth(Direction.Source));

            SpawnDirectory
                (FilesAndDirs.RootEmptyDirectoryEqualInBoth(Direction.Destination));
        }

        private static void CreateRootEmptyDirectoryDifferentInBoth()
        {
            SpawnDirectory
                (FilesAndDirs.RootEmptyDirectoryDifferentInBoth(Direction.Source),
                DateTime.Now);

            SpawnDirectory
                (FilesAndDirs.RootEmptyDirectoryDifferentInBoth(Direction.Destination),
                DateTime.MinValue);
        }

        private static void CreateRootEmptyDirectoryMissingInBoth()
        {
            SpawnDirectory
                (FilesAndDirs.RootEmptyDirectoryMissingInSrc());

            SpawnDirectory
                (FilesAndDirs.RootEmptyDirectoryMissingInDest());
        }

        private static void ClearDirectories(string pSrcDir, string pDestDir)
        {
            Directory.Delete(pSrcDir);
            Directory.Delete(pDestDir);
        }
    }
}
