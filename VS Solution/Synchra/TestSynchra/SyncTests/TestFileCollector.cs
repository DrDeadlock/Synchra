
using System.Diagnostics;
using NUnit.Framework;
using TestSynchra.FileSystemHelpers;

namespace TestSynchra.SyncTests
{
    public class TestFileCollector
    {
        private const string LOCAL_SUB_DIR = @"/TestFileCollector";

        [SetUp]
        public void Setup()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
            FileSystemTestSetup.CreateTestStructure
                (LOCAL_SUB_DIR);

            FilesAndDirs.SUBDIR_OF_TESTCLASS = LOCAL_SUB_DIR;
        }        
    }
}
