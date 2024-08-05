using System.Diagnostics;
using NUnit.Framework;
using Synchra.Synchronization;
using TestSynchra.FileSystemHelpers;

namespace TestSynchra.SyncTests
{
    public class TestSyncStateChecker
    {        
        private const string LOCAL_SUB_DIR = @"/SyncState";

        [SetUp]
        public void Setup()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
            FileSystemTestSetup.CreateTestStructure
                (LOCAL_SUB_DIR);

            FilesAndDirs.SUBDIR_OF_TESTCLASS = LOCAL_SUB_DIR;            
        }

        [Test]
        public void FileOutOfSync_FilesAreTheSame_ReturnsFalse()
        {
            string srcFilePath = FilesAndDirs.RootTxtEqualContentInBoth(Direction.Source);
            string destFilePath = FilesAndDirs.RootTxtEqualContentInBoth(Direction.Destination);            

            Assert.IsFalse(SyncStateChecker.FileOutOfSync(srcFilePath, destFilePath));
        }

        [Test]
        public void FileOutOfSync_FilesAreDifferent_ReturnsTrue()
        {
            string srcFilePath = FilesAndDirs.RootTxtDifferentContentInBoth(Direction.Source);
            string destFilePath = FilesAndDirs.RootTxtDifferentContentInBoth(Direction.Destination);

            Assert.IsTrue(SyncStateChecker.FileOutOfSync(srcFilePath, destFilePath));
        }

        [Test]
        public void FileOutOfSync_FileMissingInSrc_ReturnsTrue()
        {
            string srcFilePath = FilesAndDirs.RootTxtMissingInSrc(Direction.Source);
            string destFilePath = FilesAndDirs.RootTxtMissingInSrc(Direction.Destination);

            Assert.IsTrue(SyncStateChecker.FileOutOfSync(srcFilePath, destFilePath));
        }

        [Test]
        public void FileOutOfSync_FileMissingInDest_ReturnsTrue()
        {
            string srcFilePath = FilesAndDirs.RootTxtMissingInDest(Direction.Source);
            string destFilePath = FilesAndDirs.RootTxtMissingInDest(Direction.Destination);

            Assert.IsTrue(SyncStateChecker.FileOutOfSync(srcFilePath, destFilePath));
        }

        [Test]
        public void DirectoryOutOfSync_DirectoriesEmptyAreTheSame_ReturnsFalse()
        {
            string srcFilePath = FilesAndDirs
                .RootEmptyDirectoryEqualInBoth(Direction.Source);
            string destFilePath = FilesAndDirs
                .RootEmptyDirectoryEqualInBoth(Direction.Destination);

            Assert.IsFalse(SyncStateChecker.DirectoryOutOfSync(srcFilePath, destFilePath));
        }

        /// <summary>
        /// Can be neglected for the moment... 
        /// </summary>
        //[Test]
        //public void DirectoryOutOfSync_DirectoriesEmptyHaveDifferentTimeStamps_ReturnsTrue()
        //{
        //    string srcFilePath = FilesAndDirs
        //        .RootEmptyDirectoryDifferentInBoth(Direction.Source);
        //    string destFilePath = FilesAndDirs
        //        .RootEmptyDirectoryDifferentInBoth(Direction.Destination);

        //    Assert.IsTrue(SyncStateChecker.DirectoryOutOfSync(srcFilePath, destFilePath));
        //}

        [Test]
        public void DirectoryOutOfSync_DirectoryEmptyMissingInSrc_ReturnsTrue()
        {
            string srcFilePath = FilesAndDirs
                .RootEmptyDirectoryMissingInSrc(Direction.Source);
            string destFilePath = FilesAndDirs
                .RootEmptyDirectoryMissingInSrc(Direction.Destination);

            Assert.IsTrue(SyncStateChecker.DirectoryOutOfSync(srcFilePath, destFilePath));
        }

        [Test]
        public void DirectoryOutOfSync_DirectoriesEmptyMissingInDest_ReturnsTrue()
        {
            string srcFilePath = FilesAndDirs
                .RootEmptyDirectoryMissingInDest(Direction.Source);
            string destFilePath = FilesAndDirs
                .RootEmptyDirectoryMissingInDest(Direction.Destination);

            Assert.IsTrue(SyncStateChecker.DirectoryOutOfSync(srcFilePath, destFilePath));
        }

        //[Test]
        //public void DirectoryOutOfSync_SubDirHierarchyEqual_ReturnsFalse()
        //{
        //    string srcFilePath = FilesAndDirs
        //        .SubToEmptyDirEqualInBoth(Direction.Source);

        //    string destFilePath = FilesAndDirs
        //        .SubToEmptyDirEqualInBoth(Direction.Source);

        //    Assert.IsFalse(SyncStateChecker.DirectoryOutOfSync(srcFilePath, destFilePath));
        //}

        //[Test]
        //public void DirectoryOutOfSync_SubDirHierarchyPartMissingInSrc_ReturnsTrue()
        //{
        //    string srcFilePath = FilesAndDirs
        //        .SubToEmptyDirMissingInSrc(Direction.Source);

        //    string destFilePath = FilesAndDirs
        //        .SubToEmptyDirMissingInSrc(Direction.Source);

        //    Assert.IsTrue(SyncStateChecker.DirectoryOutOfSync(srcFilePath, destFilePath));
        //}

        //[Test]
        //public void DirectoryOutOfSync_SubDirHierarchyPartMissingInDest_ReturnsTrue()
        //{
        //    string srcFilePath = FilesAndDirs
        //        .SubToEmptyDirMissingInDest(Direction.Source);

        //    string destFilePath = FilesAndDirs
        //        .SubToEmptyDirMissingInDest(Direction.Source);

        //    Assert.IsTrue(SyncStateChecker.DirectoryOutOfSync(srcFilePath, destFilePath));
        //}            

        [TearDown]
        public void TearDown()
        {
            Trace.Flush();
        }
    }
}
