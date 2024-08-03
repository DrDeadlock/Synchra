using System;
using System.IO;

namespace TestSynchra.FileSystemHelpers
{
    public enum TestStructureType
    {
        OutOfSync = 0,
        FileSystemCollection = 1
    }

    public static class FileSystemTestSetup
    {
        public static void CreateTestStructure(string pSrcDir, string pDestDir, TestStructureType pType)
        {
            //ClearDirectories(pSrcDir, pDestDir);
            switch (pType)
            {
                case TestStructureType.OutOfSync:
                    OutOfSync(pSrcDir, pDestDir);
                    break;
                case TestStructureType.FileSystemCollection:
                    FileSystemCollector(pSrcDir, pDestDir);
                    break;
                default:
                    break;
            }
        }

        private static void OutOfSync(string pSrcDir, string pDestDir)
        {            
            FileCreator.CreateTxt(pSrcDir + FilesAndDirs.EQUAL_FILE_PATH,
                FilesAndDirs.EqualTxtFileName(1));
            FileCreator.CreateTxt(pDestDir + FilesAndDirs.EQUAL_FILE_PATH,
                FilesAndDirs.EqualTxtFileName(1));

            FileCreator.CreateTxt(pSrcDir + FilesAndDirs.DIFF_FILE_PATH,
                FilesAndDirs.DiffTxtFileName(1), "One Content");
            FileCreator.CreateTxt(pDestDir + FilesAndDirs.DIFF_FILE_PATH,
                FilesAndDirs.DiffTxtFileName(1), "And another Content");


            CreateEqual2ndSubDirLevel(pSrcDir, pDestDir);
            CreateDiffering2ndSubDirLevel(pSrcDir, pDestDir);

            //File in Both, Src and Dest
            FileCreator.CreateTxt(pSrcDir + FilesAndDirs.EQUAL_FILE_PATH,
                FilesAndDirs.EqualTxtFileName(20));
            FileCreator.CreateTxt(pDestDir + FilesAndDirs.EQUAL_FILE_PATH,
                FilesAndDirs.EqualTxtFileName(20));
            //File missing in Src
            FileCreator.CreateTxt(pDestDir + FilesAndDirs.DIFF_FILE_PATH,
                FilesAndDirs.EqualTxtFileName(21));
            //File missing in Dest
            FileCreator.CreateTxt(pSrcDir + FilesAndDirs.DIFF_FILE_PATH,
                FilesAndDirs.EqualTxtFileName(22));
        }
        

        private static void FileSystemCollector(string pSrcDir, string pDestDir)
        {
            for (int i = 1; i <= 3; i++)
            {
                FileCreator.CreateTxt(
                    pSrcDir
                        + FilesAndDirs.DIFF_FILE_PATH,
                    FilesAndDirs.DiffTxtFileName(i));
            }

            CreateDiffering2ndSubDirLevel(pSrcDir, pDestDir);
        }

        private static void CreateEqual2ndSubDirLevel(string pSrc, string pDest)
        {
            for (int i = 1; i <= 3; i++)
            {
                FileCreator.CreateTxt(
                    pSrc
                        + FilesAndDirs.EQUAL_FILE_PATH
                        + FilesAndDirs.SubDir_Equal(0)
                        + FilesAndDirs.SubDir_Equal(i),
                    FilesAndDirs.EqualTxtFileName(i));

                FileCreator.CreateTxt(
                    pDest
                        + FilesAndDirs.EQUAL_FILE_PATH
                        + FilesAndDirs.SubDir_Equal(0)
                        + FilesAndDirs.SubDir_Equal(i),
                    FilesAndDirs.EqualTxtFileName(i));
            }
        }

        private static void CreateDiffering2ndSubDirLevel(string pSrc, string pDest)
        {
            for (int i = 1; i <= 3; i++)
            {
                FileCreator.CreateTxt(
                    pSrc
                        + FilesAndDirs.DIFF_FILE_PATH
                        + FilesAndDirs.SubDir_Differing(0)
                        + FilesAndDirs.SubDir_Differing(i),
                    FilesAndDirs.DiffTxtFileName(i),
                    "DiffContent No " + i.ToString());

                FileCreator.CreateTxt(
                    pDest
                        + FilesAndDirs.DIFF_FILE_PATH
                        + FilesAndDirs.SubDir_Differing(0)
                        + FilesAndDirs.SubDir_Differing(i),
                    FilesAndDirs.DiffTxtFileName(-(i*i)),
                    "DiffContent No " + (-i).ToString());
            }
        }

        private static void ClearDirectories(string pSrcDir, string pDestDir)
        {
            Directory.Delete(pSrcDir);
            Directory.Delete(pDestDir);
        }
    }
}
