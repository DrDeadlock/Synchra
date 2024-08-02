
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Synchra.Synchronization
{
    public static class SyncStateChecker
    {
        public static bool FileOutOfSync(string srcPath, string  destPath)
        {
            byte[] srcHash;
            byte[] destHash;

            using (var md5 = MD5.Create())
            {
                using (var srcFs = File.OpenRead(srcPath))
                {
                    srcHash = md5.ComputeHash(srcFs);
                }

                using (var destFs = File.OpenRead(destPath))
                {
                    destHash = md5.ComputeHash(destFs);
                }
            }

            if (srcHash.Length != destHash.Length)
                return true;
            else
            {
                return Enumerable.SequenceEqual(srcHash, destHash);

            }
        }
    }
}
