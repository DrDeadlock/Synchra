using System;
using System.IO;
using System.Text;

namespace TestSynchra.FileSystemHelpers
{
    public static class FileSystemTestSetup
    {
        public static void CreateTestStructure(string subdir)
        {
            FilesAndDirs.SUBDIR_OF_TESTCLASS = subdir;

            CreateRootTxtSameContentInBoth();
            CreateRootTxtDifferentContentInBoth();
            CreateRootMissingTxtsInBoth();

            CreateRootEmptyDirectoryEqualInBoth();
            CreateRootEmptyDirectoryDifferentInBoth();
            CreateRootEmptyDirectoryMissingInBoth();

            CreateSubEmptyDirectoryEqualInBoth();
            CreateSubEmptyDirectoryMissingInEachOther();

            CreateSubDirectory_ComplexExcessIn(Direction.Destination);
            CreateSubDirectory_ComplexExcessIn(Direction.Source);
            CreateSubDirectory_ComplexMerge();
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
            SpawnDirectoryForFile(path);
            string fileContent = "This Content is equal in Src and Dest";

            using var fs = File.Create(path);            
            fs.Write(Encoding.ASCII.GetBytes(fileContent));
        }

        public static void CreateDifferentContentTxt(string path, string content)
        {
            SpawnDirectoryForFile(path);

            using var fs = File.Create(path);
            fs.Write(Encoding.ASCII.GetBytes(content));
        }

        private static DirectoryInfo SpawnDirectoryForFile(string filePath)
        {
            string pathToDirectory = FilesAndDirs.GetPathToFile(filePath);

            if (!Directory.Exists(pathToDirectory))
                return Directory.CreateDirectory(pathToDirectory);

            return null;
        }

        private static void SpawnNewDirectory(string path, DateTime pLastWriteTime)
        {
            DirectoryInfo info = SpawnNewDirectory(path);

            if (info != null)
                info.LastWriteTime = pLastWriteTime;
        }

        private static DirectoryInfo SpawnNewDirectory(string path)
        {
            return Directory.CreateDirectory(path);
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
            SpawnNewDirectory
                (FilesAndDirs.RootEmptyDirectoryEqualInBoth(Direction.Source));

            SpawnNewDirectory
                (FilesAndDirs.RootEmptyDirectoryEqualInBoth(Direction.Destination));
        }

        private static void CreateRootEmptyDirectoryDifferentInBoth()
        {
            SpawnNewDirectory
                (FilesAndDirs.RootEmptyDirectoryDifferentInBoth(Direction.Source),
                DateTime.Now);

            SpawnNewDirectory
                (FilesAndDirs.RootEmptyDirectoryDifferentInBoth(Direction.Destination),
                DateTime.MinValue);
        }

        private static void CreateRootEmptyDirectoryMissingInBoth()
        {
            SpawnNewDirectory
                (FilesAndDirs.RootEmptyDirectoryMissingInSrc(Direction.Destination));

            SpawnNewDirectory
                (FilesAndDirs.RootEmptyDirectoryMissingInDest(Direction.Source));
        }

        private static void CreateSubEmptyDirectoryEqualInBoth()
        {
            SpawnNewDirectory
                (FilesAndDirs.SubToEmptyDirEqualInBoth(Direction.Source));

            SpawnNewDirectory
                (FilesAndDirs.SubToEmptyDirEqualInBoth(Direction.Destination));

            CreateFiveSubDirsInBothAt(
                FilesAndDirs.SubToEmptyDirEqualInBoth(Direction.Source),
                FilesAndDirs.SubToEmptyDirEqualInBoth(Direction.Destination));
        }

        private static void CreateFiveSubDirsInBothAt(string pSrcPath, string pDestPath)
        {
            for (int i = 0; i < 5; i++)
            {
                SpawnNewDirectory
                (pSrcPath
                + FilesAndDirs.SUB_DIR_EQUAL_IN_BOTH + i.ToString());

                SpawnNewDirectory
                (pDestPath
                + FilesAndDirs.SUB_DIR_EQUAL_IN_BOTH + i.ToString());
            }
        }

        private static void CreateSubEmptyDirectoryMissingInEachOther()
        {
            SpawnNewDirectory
                (FilesAndDirs.SubToEmptyDirMissingInSrc(Direction.Destination)
                + FilesAndDirs.SUB_DIR_MISSING_IN_SRC);

            SpawnNewDirectory
                (FilesAndDirs.SubToEmptyDirMissingInDest(Direction.Source)
                + FilesAndDirs.SUB_DIR_MISSING_IN_DEST);
        }

        private static void CreateSubDirectory_ComplexExcessIn(Direction direction)
        {
            string subDirPath = "";
            string subDirLv2 = "";

            switch (direction)
            {
                case Direction.Source:
                    subDirPath = FilesAndDirs.SubToComplexTest_ExcessInSrc(Direction.Source);
                    subDirLv2 = FilesAndDirs.SUB_DIR_MISSING_IN_DEST;
                    break;
                case Direction.Destination:
                    subDirPath = FilesAndDirs.SubToComplexTest_ExcessInDest(Direction.Destination);
                    subDirLv2 = FilesAndDirs.SUB_DIR_MISSING_IN_SRC;
                    break;
                default:
                    break;
            }
            
            SpawnNewDirectory(subDirPath);

            for (int i = 0; i < 6; i++)
            {
                SpawnNewDirectory(subDirPath
                    + subDirLv2
                    + i.ToString());

                if (i > 0)
                    SpawnNewDirectory(subDirPath
                        + subDirLv2
                        + i.ToString()
                        + "/Excess Dir");

                if (i > 1)
                {
                    using FileStream fs = File.Create(subDirPath
                        + subDirLv2
                        + i.ToString()
                        + "/FileNo" + i.ToString() + ".txt"
                        );
                }

                if (i > 2)
                {
                    using FileStream fs = File.Create(subDirPath
                        + subDirLv2
                        + i.ToString()
                        + "/FileNo" + (i-1).ToString() + ".txt"
                        );
                    using FileStream fs2 = File.Create(subDirPath
                        + subDirLv2
                        + i.ToString()
                        + "/Excess Dir"
                        + "/FileNo" + i.ToString() + ".txt"
                        );
                }

                if (i > 3)
                {
                    using FileStream fs = File.Create(subDirPath
                        + subDirLv2
                        + i.ToString()
                        + "/Excess Dir"
                        + "/FileNo" + (i-1).ToString() + ".txt"
                        );
                    SpawnNewDirectory(subDirPath
                        + subDirLv2
                        + i.ToString()
                        + "/Excess Dir"
                        + "/EmptyContainer" + i.ToString()
                        ) ;
                }
            }
        }

        private static void CreateSubDirectory_ComplexMerge()
        {

        }

        private static void ClearDirectories(string pSrcDir, string pDestDir)
        {
            Directory.Delete(pSrcDir);
            Directory.Delete(pDestDir);
        }
    }
}
