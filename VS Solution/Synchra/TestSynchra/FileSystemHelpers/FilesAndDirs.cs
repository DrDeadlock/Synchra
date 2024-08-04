
namespace TestSynchra.FileSystemHelpers
{

    public static class FilesAndDirs
    {
        private const string TESTDIRS = @"/TestDirs";

        private const string SRC = @"/Src";
        private const string DEST = @"/Dest";

        private const string EQUAL = @"/Equal";
        private const string DIFFERING = @"/Differing";
        private const string SUB = "Sub";
        private const string FILES = "Files";

        private const string EQUAL_TEXT_CONTENT = "sameTextContent";
        private const string DIFF_TEXT_CONTENT = "diffTextContent";        

        public const string EQUAL_FILE_PATH = @"/EqualFiles";
        public const string DIFF_FILE_PATH = @"/DifferingFiles";

        public const string SUBLEVEL = "SubLevel";

        public static string EqualTxtFileName(int pNo)
        {
            return @"/" + EQUAL_TEXT_CONTENT + pNo.ToString() + ".txt";
        }

        public static string DiffTxtFileName(int pNo)
        {
            return @"/" + DIFF_TEXT_CONTENT + pNo.ToString() + ".txt";
        }

        public static string LocalSrcDir(string pLocalSubDir)
        {
            return TESTDIRS + pLocalSubDir + SRC;
        }

        public static string LocalDestDir(string pLocalSubDir)
        {
            return TESTDIRS + pLocalSubDir + DEST;
        }

        public static string SubDir_Equal(string pEnding)
        {
            return EQUAL + SUB + FILES + pEnding;
        }

        public static string SubDir_Equal(int pNo)
        {
            return EQUAL + SUB + FILES + pNo.ToString();
        }

        public static string SubDir_Differing(int pNo)
        {
            return DIFFERING + SUB + FILES + pNo.ToString();
        }
    }
}
