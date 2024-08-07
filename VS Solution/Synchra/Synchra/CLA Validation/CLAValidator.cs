using System;
namespace Synchra.CLAValidation
{
    public static class CLAValidator
    {
        public static bool IsValid(string[] args)
        {
            if (args.Length != 4) return false;

            bool isValid = true;
            string srcPath = args[0];
            string destPath = args[1];
            string logPath = args[2];
            int interval; 
            isValid &= int.TryParse(args[3], out interval);
            isValid &= Uri.IsWellFormedUriString(srcPath, UriKind.Absolute);
            isValid &= Uri.IsWellFormedUriString(destPath, UriKind.Absolute);
            isValid &= Uri.IsWellFormedUriString(logPath, UriKind.Absolute);

            if (isValid)
            {
                CLAContext.Instance.SetUp
                    (srcPath, destPath, logPath ,interval);
                return true;
            }

            return false;
        }
    }
}
