using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Synchra.FileSystemHelpers;
using Synchra.Synchronization;
using TestSynchra.FileSystemHelpers;

namespace TestSynchra.SyncTests
{
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
        public void OutOfSync_FileAreTheSame_ReturnsFalse()
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
        public void OutOfSync_FilesAreDifferent_ReturnsTrue()
        {
            string srcSameFile =
            FileCollector.GetAllFilesFrom(
                _srcDir + FilesAndDirs.DIFF_FILE_PATH)
                .Where(x => x.Contains(FilesAndDirs.DiffTxtFileName(1))).First();

            string destSameFile =
            FileCollector.GetAllFilesFrom(
                _destDir + FilesAndDirs.DIFF_FILE_PATH)
            .Where(x => x.Contains(FilesAndDirs.DiffTxtFileName(1))).First();

            Assert.IsTrue(SyncStateChecker.FileOutOfSync(srcSameFile, destSameFile));
        }

        [Test]
        public void OutOfSync_DirectoriesAreTheSame_ReturnsFalse()
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
        public void OutOfSync_DirectoriesAreDifferent_ReturnsTrue()
        {
            SyncStateChecker.DirectoryOutOfSync(
            _srcDir
                + FilesAndDirs.DIFF_FILE_PATH
                + FilesAndDirs.SubDir_Differing(0),
            _destDir
                + FilesAndDirs.DIFF_FILE_PATH
                + FilesAndDirs.SubDir_Differing(0));
        }

        [TearDown]
        public void TearDown()
        {
            Trace.Flush();
        }
    }
}
