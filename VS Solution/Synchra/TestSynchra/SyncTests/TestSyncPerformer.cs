using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using TestSynchra.FileSystemHelpers;

namespace TestSynchra.SyncTests
{
    public class TestSyncPerformer
    {
        private string _srcDir;
        private string _destDir;

        private string _localSubDir = @"/TestSyncPerformer";

        [SetUp]
        public void Setup()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            var currentDir = Directory.GetCurrentDirectory();
            _srcDir = currentDir + FilesAndDirs.LocalSrcDir(_localSubDir);
            _destDir = currentDir + FilesAndDirs.LocalDestDir(_localSubDir);
            FileSystemTestSetup.CreateTestStructure
                (_srcDir, _destDir, TestStructureType.OutOfSync);
        }

        [Test]
        public void Execute_DirectoriesAreEqual_ChecksumStaysEqual()
        {
            Assert.Fail();
        }

        [Test]
        public void Execute_ExcessFileInTopLevelDir_DestFilesDeleted()
        {
            Assert.Fail();
        }

        [Test]
        public void Execute_MissingFileInTopLevelDir_FileCreationInDest()
        {
            Assert.Fail();
        }

        [Test]
        public void Execute_DifferentFileContentInTopLevelDirectory_FileCopyInDest()
        {
            Assert.Fail();
        }

        [Test]
        public void Execute_ExcessFileInSubDirectory_FileDeleteInDest()
        {
            Assert.Fail();
        }

        [Test]
        public void Execute_MissingFileInSubDirectory_FileCreateInDest()
        {
            Assert.Fail();
        }

        [Test]
        public void Execute_DifferentFileContentInSubDirectory_FileCopyInDest()
        {
            Assert.Fail();
        }

        [Test]
        public void Execute_ExcessEmptySubDirectories_DirectoryDeleteInDest()
        {
            Assert.Fail();
        }

        [Test]
        public void Execute_MissingEmptySubDirectory_DirectoryCreateInDest()
        {
            Assert.Fail();
        }
    }
}
