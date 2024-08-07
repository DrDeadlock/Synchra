using System;
using System.IO;
using Synchra.Logging.Wrappers;

namespace Synchra.CLAValidation
{
    public static class CLAValidator
    {
        private static SynchronizationCommunicator syncComm;

        public static bool Build(string[] args)
        {
            syncComm = SynchronizationCommunicator.Instance;
            if (args.Length != 4)
            {
                syncComm.Error("Not exactly 4 Command Line Arguments were passed!");
                return false;
            }

            bool isValid = true;

            string srcPath = args[0];
            isValid &= Directory.Exists(srcPath);            
            if (!isValid) { syncComm.Error("The Source Path does not exist!"); return false; }

            string destPath = args[1];
            isValid &= Directory.Exists(destPath);
            if (!isValid) { syncComm.Error("The Destination Path does not exist!"); return false; }

            string logPath = args[2];
            isValid &= Directory.Exists(logPath);
            if (!isValid) { syncComm.Error("The Log Path does not exist!"); return false; }

            int interval; 
            isValid &= int.TryParse(args[3], out interval);
            if (!isValid) { syncComm.Error("The interval number is not valid!"); return false; }

            CLAContext.Instance.SetUp
                (srcPath, destPath, logPath ,interval);
            return true;
            
        }
    }
}
