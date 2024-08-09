using System;
namespace Synchra.FileSystemHelpers
{
    public static class PathConversion
    {
        /// <summary>
        /// Return the part of pPath which does not contain the relationPath 
        /// </summary>
        /// <param name="pPath"></param>
        /// <param name="relationPath"></param>
        /// <returns></returns>
        public static string MakePathLocal(string pPath, string relationPath)
        {
            int index = pPath.IndexOf(relationPath);
            string cleanPath = (index < 0)
                ? pPath
                : pPath.Remove(index, relationPath.Length);

            return cleanPath;
        }

        /// <summary>
        /// Return the very last part of a file path
        /// </summary>
        /// <param name="pPath"></param>
        /// <returns></returns>
        public static string GetFileName(string pPath)
        {
            if (!pPath.Contains("/"))
                return pPath;
            
            return pPath.Substring(pPath.LastIndexOf("/"));
        }
    }
}
