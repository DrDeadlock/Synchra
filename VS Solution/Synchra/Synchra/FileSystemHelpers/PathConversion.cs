using System;
namespace Synchra.FileSystemHelpers
{
    public static class PathConversion
    {
        public static string MakePathLocal(string pPath, string relationPath)
        {
            int index = pPath.IndexOf(relationPath);
            string cleanPath = (index < 0)
                ? pPath
                : pPath.Remove(index, relationPath.Length);

            return cleanPath;
        }
    }
}
