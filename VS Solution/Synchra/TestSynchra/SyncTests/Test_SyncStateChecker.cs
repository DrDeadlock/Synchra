using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Synchra.FileSystemHelpers;
using Synchra.Synchronization;
using TestSynchra.FileSystemHelpers;

namespace TestSynchra.SyncTests
{
    enum SubDirPurpose
    {
        TestChecksum = 1,
        TestBothContain = 2
    }

    public class Test_SyncStateChecker
    {        
        private string _srcDir;
        private string _destDir;

        private string _localSubDir = @"/TestOutOfSync";

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
        public void FileOutOfSync_FileAreTheSame_ReturnsFalse()
        {           
            string srcSameFile =
            FileCollector.GetAllFilesFrom(_srcDir + FilesAndDirs.EQUAL_FILE_PATH)
            .Where(x => x.Contains(FilesAndDirs.EqualTxtFileName(1))).First();

            string destSameFile =
            FileCollector.GetAllFilesFrom(_destDir + FilesAndDirs.EQUAL_FILE_PATH)
            .Where(x => x.Contains(FilesAndDirs.EqualTxtFileName(1))).First();

            Assert.IsFalse(SyncStateChecker.FileOutOfSync(srcSameFile, destSameFile));
        }

        [Test]
        public void FileOutOfSync_FilesAreDifferent_ReturnsTrue()
        {
            string srcDiffFile =
            FileCollector.GetAllFilesFrom(
                _srcDir + FilesAndDirs.DIFF_FILE_PATH)
                .Where(x => x.Contains(FilesAndDirs.DiffTxtFileName(1))).First();

            string destDiffFile =
            FileCollector.GetAllFilesFrom(
                _destDir + FilesAndDirs.DIFF_FILE_PATH)
            .Where(x => x.Contains(FilesAndDirs.DiffTxtFileName(1))).First();

            Assert.IsTrue(SyncStateChecker.FileOutOfSync(srcDiffFile, destDiffFile));
        }

        [Test]
        public void FileOutOfSync_FileMissingInSrc_ReturnsTrue()
        {
            string srcDiffFile =
            FileCollector.GetAllFilesFrom(
                _srcDir + FilesAndDirs.DIFF_FILE_PATH)
                .Where(x => x.Contains(FilesAndDirs.DiffTxtFileName(1))).First();

            string destDiffFile =
            FileCollector.GetAllFilesFrom(
                _destDir + FilesAndDirs.DIFF_FILE_PATH)
            .Where(x => x.Contains(FilesAndDirs.DiffTxtFileName(1))).First();
            destDiffFile += "NotExistent.txt";

            Assert.IsTrue(SyncStateChecker.FileOutOfSync(srcDiffFile, destDiffFile));
        }

        [Test]
        public void DirectoryOutOfSync_DirectoriesAreTheSame_ReturnsFalse()
        {
            SyncStateChecker.DirectoryOutOfSync(
            _srcDir
                + FilesAndDirs.EQUAL_FILE_PATH
                + FilesAndDirs.SubDir_Equal(0),
            _destDir
                + FilesAndDirs.EQUAL_FILE_PATH
                + FilesAndDirs.SubDir_Equal(0));
        }

        [Test]
        public void DirectoryOutOfSync_DirectoriesAreDifferent_ReturnsTrue()
        {
            SyncStateChecker.DirectoryOutOfSync(
            _srcDir
                + FilesAndDirs.DIFF_FILE_PATH
                + FilesAndDirs.SubDir_Differing(0),
            _destDir
                + FilesAndDirs.DIFF_FILE_PATH
                + FilesAndDirs.SubDir_Differing(0));
        }

        [Test]
        public void DirectoryOutOfSync_DirectoryMissingInSrc_ReturnsTrue()
        {
            SyncStateChecker.DirectoryOutOfSync(
            _srcDir
                + FilesAndDirs.DIFF_FILE_PATH
                + FilesAndDirs.SubDir_Differing(0),
            _destDir
                + FilesAndDirs.DIFF_FILE_PATH
                + FilesAndDirs.SubDir_Differing(0)
                + "/NotExistentDirectory");
        }

        [Test]
        public void BothContain_SrcAndDestContainFile_ReturnsTrue()
        {
            Assert.IsTrue(File.Exists(_srcDir + FilesAndDirs.EQUAL_FILE_PATH + FilesAndDirs.EqualTxtFileName(20)));
            Assert.IsTrue(File.Exists(_destDir + FilesAndDirs.EQUAL_FILE_PATH + FilesAndDirs.EqualTxtFileName(20)));
            Assert.IsTrue(SyncStateChecker.BothContain
                (
                    _srcDir
                    + FilesAndDirs.EQUAL_FILE_PATH,
                    _destDir
                    + FilesAndDirs.EQUAL_FILE_PATH,
                    FilesAndDirs.EqualTxtFileName(20))
                );
        }

        [Test]
        public void BothContain_SrcDoesntContainFile_ReturnsTrue()
        {
            Assert.IsFalse(File.Exists(_srcDir + FilesAndDirs.DIFF_FILE_PATH + FilesAndDirs.EqualTxtFileName(21)));
            Assert.IsTrue(File.Exists(_destDir + FilesAndDirs.DIFF_FILE_PATH + FilesAndDirs.EqualTxtFileName(21)));
            Assert.IsFalse(SyncStateChecker.BothContain
                (
                    _srcDir
                    + FilesAndDirs.EQUAL_FILE_PATH,
                    _destDir
                    + FilesAndDirs.EQUAL_FILE_PATH,
                    FilesAndDirs.EqualTxtFileName(21))
                );
        }

        [Test]
        public void BothContain_DestDoesntContainFile_ReturnsTrue()
        {
            Assert.IsFalse(File.Exists(_destDir + FilesAndDirs.DIFF_FILE_PATH + FilesAndDirs.EqualTxtFileName(22)));
            Assert.IsTrue(File.Exists(_srcDir + FilesAndDirs.DIFF_FILE_PATH + FilesAndDirs.EqualTxtFileName(22)));
            Assert.IsFalse(SyncStateChecker.BothContain
                (
                    _srcDir
                    + FilesAndDirs.EQUAL_FILE_PATH,
                    _destDir
                    + FilesAndDirs.EQUAL_FILE_PATH,
                    FilesAndDirs.EqualTxtFileName(22))
                );
        }        

        [TearDown]
        public void TearDown()
        {
            Trace.Flush();
        }
    }
}
