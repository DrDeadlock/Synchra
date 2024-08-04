
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
        public static string SUBDIR = "";

        private static string COREPATH
        {
            get => Directory.GetCurrentDirectory() + TESTDIRS + SUBDIR;
        }

        private const string SRC = @"/Src";
        private const string DEST = @"/Dest";

        private const string FILE_EQUALCONTENT_IN_BOTH = @"/TxtEqualContentForBoth.txt";
        private const string FILE_DIFFERENTCONTENT_IN_BOTH = @"/TxtDifferentContentForBoth.txt";
        private const string FILE_MISSING_IN_SRC = @"/TxtMissingInSrc.txt";
        private const string FILE_MISSING_IN_DEST = @"/TxtMissingInDest.txt";

        private const string DIRECTORY_EMPTY_EQUAL_IN_BOTH = @"/EmptyDir_EqualForBoth";
        private const string DIRECTORY_EMPTY_DIFFERENT_IN_BOTH = @"/EmptyDir_DifferentForBoth";
        private const string DIRECTORY_EMPTY_MISSING_IN_SRC = @"/EmptyDir_MissingInSrc";
        private const string DIRECTORY_EMPTY_MISSING_IN_DEST = @"/EmptyDir_MissingInDest";

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

        public static string RootEmptyDirectoryMissingInSrc()
        {
            return COREPATH + SrcOrDest(Direction.Destination) + DIRECTORY_EMPTY_MISSING_IN_SRC;
        }

        public static string RootEmptyDirectoryMissingInDest()
        {
            return COREPATH + SrcOrDest(Direction.Source) + DIRECTORY_EMPTY_MISSING_IN_DEST;
        }
    }
}
