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
        private const string LOCAL_SRC_DIR = @"/TestDirs/Src";
        private const string LOCAL_DEST_DIR = @"/TestDirs/Dest";

        private const string SAME_TXT_01 = "sameText01.txt";
        private const string DIFF_TXT_01 = "diffText01.txt";

        private const string SAME_FILE_PATH = @"/EqualFiles/";
        private const string DIFF_FILE_PATH = @"/DifferingFiles/";

        private string _srcDir;
        private string _destDir;

        [SetUp]
        public void Setup()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            var currentDir = Directory.GetCurrentDirectory();
            _srcDir = currentDir + LOCAL_SRC_DIR;
            _destDir = currentDir + LOCAL_DEST_DIR;

            FileCreator.CreateTxt(_srcDir + SAME_FILE_PATH, SAME_TXT_01);
            FileCreator.CreateTxt(_destDir + SAME_FILE_PATH, SAME_TXT_01);

            FileCreator.CreateTxt(_srcDir + DIFF_FILE_PATH, DIFF_TXT_01, "One Content");
            FileCreator.CreateTxt(_destDir + DIFF_FILE_PATH, DIFF_TXT_01, "And another Content");
        }

        [Test]
        public void FileAreTheSame_ReturnsFalse()
        {           
            string srcSameFile =
            FileCollector.GetAllFilesFrom(_srcDir + SAME_FILE_PATH)
            .Where(x => x.Contains(SAME_TXT_01)).First();

            string destSameFile =
            FileCollector.GetAllFilesFrom(_destDir + SAME_FILE_PATH)
            .Where(x => x.Contains(SAME_TXT_01)).First();

            Assert.IsFalse(SyncStateChecker.FileOutOfSync(srcSameFile, destSameFile));
        }

        [TearDown]
        public void TearDown()
        {
            Trace.Flush();
        }
    }
}
