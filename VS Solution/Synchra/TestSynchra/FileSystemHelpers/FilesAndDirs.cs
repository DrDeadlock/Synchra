
using System.IO;

namespace TestSynchra.FileSystemHelpers
{
    public enum Direction
    {
        Source = 1,
        Destination = 2
    }
    public static class FilesAndDirs
    {        
        private const string TESTDIRS = @"/TestDirs";

        /// <summary>
        /// Local SubDirectory, one for each Test Class.
        /// </summary>
        public static string SUBDIR_OF_TESTCLASS = "";

        private static string COREPATH
        {
            get => Directory.GetCurrentDirectory() + TESTDIRS + SUBDIR_OF_TESTCLASS;
        }

        private const string SRC = @"/Src";
        private const string DEST = @"/Dest";

        private const string CONTENT_DIRECTORY = @"/ContentDirectory";

        private const string FILE_EQUALCONTENT_IN_BOTH = @"/TxtEqualContentForBoth.txt";
        private const string FILE_DIFFERENTCONTENT_IN_BOTH = @"/TxtDifferentContentForBoth.txt";
        private const string FILE_MISSING_IN_SRC = @"/TxtMissingInSrc.txt";
        private const string FILE_MISSING_IN_DEST = @"/TxtMissingInDest.txt";

        private const string DIRECTORY_EMPTY_EQUAL_IN_BOTH = @"/EmptyDir_EqualForBoth";
        private const string DIRECTORY_EMPTY_DIFFERENT_IN_BOTH = @"/EmptyDir_DifferentForBoth";
        private const string DIRECTORY_EMPTY_MISSING_IN_SRC = @"/EmptyDir_MissingInSrc";
        private const string DIRECTORY_EMPTY_MISSING_IN_DEST = @"/EmptyDir_MissingInDest";

        private const string SUB_DIR_CONTAINING_EMPTY_DIRS = @"/ContainingEmptyDirs";
        public const string SUB_DIR_EQUAL_IN_BOTH = @"/EqualInBoth";
        public const string SUB_DIR_MISSING_IN_SRC = @"/MissingInSrc";
        public const string SUB_DIR_MISSING_IN_DEST = @"/MissingInDest";

        private const string SUB_DIR_CONTAINING_COMPLEX_TEST = @"/ContainingComplexTest";
        private const string SUB_DIR_EXCESS_IN_DEST = @"/ExcessInDest";
        private const string SUB_DIR_EXCESS_IN_SRC = @"/ExcessInSrc";
        private const string SUB_DIR_MUST_BE_MERGED = @"/MustBeMerged";

        public static string GetPathToFile(string pPath)
        {
            string pathToDirectory = pPath.Substring(0, pPath.LastIndexOf("/"));
            return pathToDirectory;
        }

        public static string CorePathTo(Direction direction)
        {
            return COREPATH + SrcOrDest(direction);
        }

        private static string SrcOrDest(Direction direction)
        {
            return direction == Direction.Source ? SRC : DEST;
        }

        public static string RootTxtEqualContentInBoth(Direction direction)
        {
            return COREPATH + SrcOrDest(direction) + FILE_EQUALCONTENT_IN_BOTH;
        }

        public static string RootTxtDifferentContentInBoth(Direction direction)
        {
            return COREPATH + SrcOrDest(direction) + FILE_DIFFERENTCONTENT_IN_BOTH;
        }

        public static string RootTxtMissingInSrc(Direction direction)
        {
            return COREPATH + SrcOrDest(direction) + FILE_MISSING_IN_SRC;
        }

        public static string RootTxtMissingInDest(Direction direction)
        {
            return COREPATH + SrcOrDest(direction) + FILE_MISSING_IN_DEST;
        }

        public static string RootEmptyDirectoryEqualInBoth(Direction direction)
        {
            return COREPATH + SrcOrDest(direction) + DIRECTORY_EMPTY_EQUAL_IN_BOTH;
        }

        public static string RootEmptyDirectoryDifferentInBoth(Direction direction)
        {
            return COREPATH + SrcOrDest(direction) + DIRECTORY_EMPTY_DIFFERENT_IN_BOTH;
        }

        public static string RootEmptyDirectoryMissingInSrc(Direction direction)
        {
            return COREPATH + SrcOrDest(direction) + DIRECTORY_EMPTY_MISSING_IN_SRC;
        }

        public static string RootEmptyDirectoryMissingInDest(Direction direction)
        {
            return COREPATH + SrcOrDest(direction) + DIRECTORY_EMPTY_MISSING_IN_DEST;
        }

        public static string PathToSubDirectoryContainingDirsAndFiles(Direction direction)
        {
            return COREPATH + SrcOrDest(direction)
                + CONTENT_DIRECTORY;
        }

        public static string SubToEmptyDirEqualInBoth(Direction direction)
        {
            return COREPATH + SrcOrDest(direction)
                + CONTENT_DIRECTORY
                + SUB_DIR_CONTAINING_EMPTY_DIRS
                + SUB_DIR_EQUAL_IN_BOTH;
        }

        public static string SubToEmptyDirMissingInSrc(Direction direction)
        {
            return COREPATH + SrcOrDest(direction)
                + CONTENT_DIRECTORY
                + SUB_DIR_CONTAINING_EMPTY_DIRS
                + SUB_DIR_MISSING_IN_SRC;
        }

        public static string SubToEmptyDirMissingInDest(Direction direction)
        {
            return COREPATH + SrcOrDest(direction)
                + CONTENT_DIRECTORY
                + SUB_DIR_CONTAINING_EMPTY_DIRS
                + SUB_DIR_MISSING_IN_DEST;
        }

        public static string SubToComplexTest(Direction direction)
        {
            return COREPATH + SrcOrDest(direction)
                + CONTENT_DIRECTORY
                + SUB_DIR_CONTAINING_COMPLEX_TEST;
        }

        public static string SubToComplexTest_ExcessInDest(Direction direction)
        {
            return COREPATH + SrcOrDest(direction)
                + CONTENT_DIRECTORY
                + SUB_DIR_CONTAINING_COMPLEX_TEST
                + SUB_DIR_EXCESS_IN_DEST;
        }

        public static string SubToComplexTest_ExcessInSrc(Direction direction)
        {
            return COREPATH + SrcOrDest(direction)
                + CONTENT_DIRECTORY
                + SUB_DIR_CONTAINING_COMPLEX_TEST
                + SUB_DIR_EXCESS_IN_SRC;
        }

        public static string SubToComplexTest_MustBeMerged(Direction direction)
        {
            return COREPATH + SrcOrDest(direction)
                + CONTENT_DIRECTORY
                + SUB_DIR_CONTAINING_COMPLEX_TEST
                + SUB_DIR_MUST_BE_MERGED;
        }


    }
}
