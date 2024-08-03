﻿
using System.IO;
using NUnit.Framework;
using Synchra.FileSystemHelpers;
using TestSynchra.FileSystemHelpers;

namespace TestSynchra.SyncTests
{
    public class TestFileCollector
    {
        private string _srcDir;
        private string _destDir;

        private string _localSubDir = @"/TestFileCollector";

        [SetUp]
        public void Setup()
        {
            var currentDir = Directory.GetCurrentDirectory();
            _srcDir = currentDir + FilesAndDirs.LocalSrcDir(_localSubDir);
            _destDir = currentDir + FilesAndDirs.LocalDestDir(_localSubDir);

            FileSystemTestSetup.CreateTestStructure
                (_srcDir, _destDir, TestStructureType.FileSystemCollection);
        }

        [Test]
        public void GetAllFiles_ThreeFilesContained_CountIsThree()
        {
            string[] filePaths =
            FileCollector.GetAllFilesFrom(_srcDir + FilesAndDirs.DIFF_FILE_PATH);
            Assert.AreEqual(3, filePaths.Length);            
        }

        [Test]
        public void GetAllFilesRecursively_ThreeFilesContained_CountIsThree()
        {
            string[] filePaths =
            FileCollector.GetAllFilesRecursivelyFrom(_srcDir
                        + FilesAndDirs.DIFF_FILE_PATH
                        + FilesAndDirs.SubDir_Differing(0));
            Assert.AreEqual(3, filePaths.Length);
        }
    }
}