
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

        public void SetUp
            (string src, string dest, string log, int interval)
        {
            SrcPath = src;
            DestPath = dest;
            LogPath = log;
            Interval = interval;
        }

        public string SrcPath;
        public string DestPath;
        public string LogPath;
        public int Interval; 
    }
}
