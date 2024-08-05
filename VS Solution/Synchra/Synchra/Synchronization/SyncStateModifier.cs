using System;
using System.IO;

namespace Synchra.Synchronization
{
    public static class SyncStateModifier
    {
        public static void CreateFile(string from, string to)
        {
            File.Copy(from, to);
        }

        public static void UpdateFile(string from, string to)
        {
            File.Delete(to);
            File.Copy(from, to);
        }

        public static void DeleteFile(string at)
        {
            File.Delete(at);
        }

        public static void CreateDirectory(string at)
        {
            Directory.CreateDirectory(at);
        }

        public static void DeleteDirectory(string at)
        {
            Console.WriteLine("Try to delete directory: " + at);
            Directory.Delete(at);
        }
    }
}
