using System;
namespace Synchra.CLAValidation
{
    public class CLAContext
    {
        private static CLAContext _instance = null;

        private CLAContext()
        {

        }

        public static CLAContext Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CLAContext();

                return _instance;
            }
        }

        public string SrcPath;
        public string DestPath;
        public string logPath;
        public int interval; 
    }
}
