using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Synchra.FileSystemHelpers;
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

            FilesAndDirs.SUBDIR = LOCAL_SUB_DIR;            
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
        public void DirectoryOutOfSync_DirectoriesEmptyAreTheSame_ReturnsFalse()
        {
            string srcFilePath = FilesAndDirs.RootTxtMissingInDest(Direction.Source);
            string destFilePath = FilesAndDirs.RootTxtMissingInDest(Direction.Destination);

            Assert.IsTrue(SyncStateChecker.FileOutOfSync(srcFilePath, destFilePath));
        }

        //[Test]
        //public void DirectoryOutOfSync_DirectoriesWithFilesAreTheSame_ReturnsFalse()
        //{
        //    SyncStateChecker.DirectoryOutOfSync(
        //    _srcDir
        //        + FilesAndDirs.EQUAL_FILE_PATH
        //        + FilesAndDirs.SubDir_Equal(0),
        //    _destDir
        //        + FilesAndDirs.EQUAL_FILE_PATH
        //        + FilesAndDirs.SubDir_Equal(0));
        //}

        //[Test]
        //public void DirectoryOutOfSync_DirectoriesWithFilesAreDifferent_ReturnsTrue()
        //{
        //    SyncStateChecker.DirectoryOutOfSync(
        //    _srcDir
        //        + FilesAndDirs.DIFF_FILE_PATH
        //        + FilesAndDirs.SubDir_Differing(0),
        //    _destDir
        //        + FilesAndDirs.DIFF_FILE_PATH
        //        + FilesAndDirs.SubDir_Differing(0));
        //}

        //[Test]
        //public void DirectoryOutOfSync_DirectoryMissingInSrc_ReturnsTrue()
        //{
        //    SyncStateChecker.DirectoryOutOfSync(
        //    _srcDir
        //        + FilesAndDirs.DIFF_FILE_PATH
        //        + FilesAndDirs.SubDir_Differing(0),
        //    _destDir
        //        + FilesAndDirs.DIFF_FILE_PATH
        //        + FilesAndDirs.SubDir_Differing(0)
        //        + "/MissingDirectory");
        //}

        //[Test]
        //public void DirectoryOutOfSync_DirectoryMissingInDest_ReturnsTrue()
        //{
        //    SyncStateChecker.DirectoryOutOfSync(
        //    _srcDir
        //        + FilesAndDirs.DIFF_FILE_PATH
        //        + FilesAndDirs.SubDir_Differing(0)
        //        + "/MissingDirectory",
        //    _destDir
        //        + FilesAndDirs.DIFF_FILE_PATH
        //        + FilesAndDirs.SubDir_Differing(0));
        //}

        //[Test]
        //public void BothContain_SrcAndDestContainFile_ReturnsTrue()
        //{
        //    Assert.IsTrue(File.Exists(_srcDir + FilesAndDirs.EQUAL_FILE_PATH + FilesAndDirs.EqualTxtFileName(20)));
        //    Assert.IsTrue(File.Exists(_destDir + FilesAndDirs.EQUAL_FILE_PATH + FilesAndDirs.EqualTxtFileName(20)));
        //    Assert.IsTrue(SyncStateChecker.BothContain
        //        (
        //            _srcDir
        //            + FilesAndDirs.EQUAL_FILE_PATH,
        //            _destDir
        //            + FilesAndDirs.EQUAL_FILE_PATH,
        //            FilesAndDirs.EqualTxtFileName(20))
        //        );
        //}

        //[Test]
        //public void BothContain_SrcDoesntContainFile_ReturnsTrue()
        //{
        //    Assert.IsFalse(File.Exists(_srcDir + FilesAndDirs.DIFF_FILE_PATH + FilesAndDirs.EqualTxtFileName(21)));
        //    Assert.IsTrue(File.Exists(_destDir + FilesAndDirs.DIFF_FILE_PATH + FilesAndDirs.EqualTxtFileName(21)));
        //    Assert.IsFalse(SyncStateChecker.BothContain
        //        (
        //            _srcDir
        //            + FilesAndDirs.EQUAL_FILE_PATH,
        //            _destDir
        //            + FilesAndDirs.EQUAL_FILE_PATH,
        //            FilesAndDirs.EqualTxtFileName(21))
        //        );
        //}

        //[Test]
        //public void BothContain_DestDoesntContainFile_ReturnsTrue()
        //{
        //    Assert.IsFalse(File.Exists(_destDir + FilesAndDirs.DIFF_FILE_PATH + FilesAndDirs.EqualTxtFileName(22)));
        //    Assert.IsTrue(File.Exists(_srcDir + FilesAndDirs.DIFF_FILE_PATH + FilesAndDirs.EqualTxtFileName(22)));
        //    Assert.IsFalse(SyncStateChecker.BothContain
        //        (
        //            _srcDir
        //            + FilesAndDirs.EQUAL_FILE_PATH,
        //            _destDir
        //            + FilesAndDirs.EQUAL_FILE_PATH,
        //            FilesAndDirs.EqualTxtFileName(22))
        //        );
        //}        

        [TearDown]
        public void TearDown()
        {
            Trace.Flush();
        }
    }
}
