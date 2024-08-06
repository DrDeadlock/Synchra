using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using Synchra.Synchronization;
using TestSynchra.FileSystemHelpers;

namespace TestSynchra.SyncTests
{
    public class TestSyncPerformer
    {
        private const string LOCAL_SUB_DIR = @"/TestSyncPerformer";

        [SetUp]
        public void Setup()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
            FileSystemTestSetup.CreateTestStructure
                (LOCAL_SUB_DIR);

            FilesAndDirs.SUBDIR_OF_TESTCLASS = LOCAL_SUB_DIR;
        }

        //[Test]
        //public void Execute_DirectoriesAreEqual_ChecksumStaysEqual()
        //{
        //    Assert.Fail();
        //}

        [Test]
        public void Execute_ExcessFileInDirDest_DestFilesDeleted()
        {
            string srcFile = FilesAndDirs
                .RootTxtMissingInSrc(Direction.Source);
            string destFile = FilesAndDirs
                .RootTxtMissingInSrc(Direction.Destination);

            string srcDirContainingfile =
                srcFile.Substring(0, srcFile.LastIndexOf("/"));
            string destDirContainingfile =
                destFile.Substring(0, destFile.LastIndexOf("/"));

            Assert.IsTrue(SyncStateChecker.FileOutOfSync(
                srcFile, destFile));
            SyncPerformer.ClearExcessFilesInDest(
                srcDirContainingfile, destDirContainingfile, 0);
            Assert.IsTrue(SyncStateChecker.BothMissFile(srcFile, destFile));
        }

        [Test]
        public void Execute_ExcessFileInDirSrc_SrcFileCopied()
        {
            string srcFile = FilesAndDirs
                .RootTxtMissingInDest(Direction.Source);
            string destFile = FilesAndDirs
                .RootTxtMissingInDest(Direction.Destination);

            string srcDirContainingfile =
                srcFile.Substring(0, srcFile.LastIndexOf("/"));
            string destDirContainingfile =
                destFile.Substring(0, destFile.LastIndexOf("/"));

            Assert.IsTrue(SyncStateChecker.FileOutOfSync(
                srcFile, destFile));
            SyncPerformer.CreateAndUpdateFiles(
                srcDirContainingfile, destDirContainingfile, 0);
            Assert.IsTrue(SyncStateChecker.BothContainFile(srcFile, destFile));
        }

        [Test]
        public void Execute_EmptyExcessDirInDest_DestDirDeleted()
        {
            string srcLocalRootDir = FilesAndDirs
                .SubToEmptyDirMissingInSrc(Direction.Source);
            string destLocalRootDir = FilesAndDirs
                .SubToEmptyDirMissingInSrc(Direction.Destination);

            string srcLocalSubDir = FilesAndDirs
                .SubToEmptyDirMissingInSrc(Direction.Source)
                + FilesAndDirs.SUB_DIR_MISSING_IN_SRC;

            string destLocalSubDir = FilesAndDirs
                .SubToEmptyDirMissingInSrc(Direction.Destination)
                + FilesAndDirs.SUB_DIR_MISSING_IN_SRC;

            Assert.IsFalse(SyncStateChecker.BothMissDirectory(srcLocalSubDir, destLocalSubDir));
            Assert.IsTrue(SyncStateChecker.DirectoryOutOfSync(
                srcLocalRootDir, destLocalRootDir));
            SyncPerformer.ClearExcessDirsInDest(
                srcLocalRootDir, destLocalRootDir, 0);
            Assert.IsTrue(SyncStateChecker.BothMissDirectory(srcLocalSubDir, destLocalSubDir));
        }

        [Test]
        public void Execute_EmptyExcessDirInSrc_SrcDirCopied()
        {
            string srcLocalRootDir = FilesAndDirs
                .SubToEmptyDirMissingInDest(Direction.Source);
            string destLocalRootDir = FilesAndDirs
                .SubToEmptyDirMissingInDest(Direction.Destination);

            string srcLocalSubDir = FilesAndDirs
                .SubToEmptyDirMissingInDest(Direction.Source)
                + FilesAndDirs.SUB_DIR_MISSING_IN_DEST;

            string destLocalSubDir = FilesAndDirs
                .SubToEmptyDirMissingInDest(Direction.Destination)
                + FilesAndDirs.SUB_DIR_MISSING_IN_DEST;

            Assert.IsFalse(SyncStateChecker.BothContainDirectory(srcLocalSubDir, destLocalSubDir));
            Assert.IsTrue(SyncStateChecker.DirectoryOutOfSync(
                srcLocalRootDir, destLocalRootDir));
            SyncPerformer.CreateDirectories(
                srcLocalRootDir, destLocalRootDir, 0);
            Assert.IsTrue(SyncStateChecker.BothContainDirectory(srcLocalSubDir, destLocalSubDir));
        }

        [Test]
        public void Execute_ExcessDirWithContentInDest_DestDirDeleted()
        {
            string srcSubDirPath =
                FilesAndDirs.SubToComplexTest_ExcessInDest(Direction.Source);
            string destSubDirPath =
                FilesAndDirs.SubToComplexTest_ExcessInDest(Direction.Destination);

            Assert.IsTrue(SyncStateChecker.DirectoryOutOfSyncRecursively
                (srcSubDirPath, destSubDirPath));

            string srcSubDirToSync =
                FilesAndDirs.SubToComplexTest(Direction.Source);
            string destSubDirToSync =
                FilesAndDirs.SubToComplexTest(Direction.Destination);
            SyncPerformer.ClearExcessFilesInDestRecursively
                (srcSubDirToSync, destSubDirToSync, 1);
            SyncPerformer.ClearExcessDirsInDestRecursively
                (srcSubDirToSync, destSubDirToSync, 1);

            Assert.IsFalse(SyncStateChecker.DirectoryOutOfSyncRecursively
                (srcSubDirPath, destSubDirPath));
        }

        [Test]
        public void Execute_ExcessDirWithContentInSrc_SrcDirCopied()
        {
            string srcSubDirPath =
                FilesAndDirs.SubToComplexTest_ExcessInSrc(Direction.Source);
            string destSubDirPath =
                FilesAndDirs.SubToComplexTest_ExcessInSrc(Direction.Destination);

            Assert.IsTrue(SyncStateChecker.DirectoryOutOfSyncRecursively
                (srcSubDirPath, destSubDirPath));

            string srcSubDirToSync =
                FilesAndDirs.SubToComplexTest(Direction.Source);
            string destSubDirToSync =
                FilesAndDirs.SubToComplexTest(Direction.Destination);
            SyncPerformer.CreateDirectoriesRecursively
                (srcSubDirToSync, destSubDirToSync, 1);
            SyncPerformer.CreateAndUpdateFilesRecursively
                (srcSubDirToSync, destSubDirToSync, 1);

            Assert.IsFalse(SyncStateChecker.DirectoryOutOfSyncRecursively
                (srcSubDirPath, destSubDirPath));
        }

        //[Test]
        //public void Execute_MissingFileInTopLevelDir_FileCreationInDest()
        //{
        //    Assert.Fail();
        //}

        //[Test]
        //public void Execute_DifferentFileContentInTopLevelDirectory_FileCopyInDest()
        //{
        //    Assert.Fail();
        //}

        //[Test]
        //public void Execute_ExcessFileInSubDirectory_FileDeleteInDest()
        //{
        //    Assert.Fail();
        //}

        //[Test]
        //public void Execute_MissingFileInSubDirectory_FileCreateInDest()
        //{
        //    Assert.Fail();
        //}

        //[Test]
        //public void Execute_DifferentFileContentInSubDirectory_FileCopyInDest()
        //{
        //    Assert.Fail();
        //}

        //[Test]
        //public void Execute_ExcessEmptySubDirectories_DirectoryDeleteInDest()
        //{
        //    Assert.Fail();
        //}

        //[Test]
        //public void Execute_MissingEmptySubDirectory_DirectoryCreateInDest()
        //{
        //    Assert.Fail();
        //}
    }
}
