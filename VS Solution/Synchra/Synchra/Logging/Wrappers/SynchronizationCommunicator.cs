using log4net;

namespace Synchra.Logging.Wrappers
{
    public class SynchronizationCommunicator
    {
        private readonly ILog _logger;

        public SynchronizationCommunicator()
        {
            _logger =
                new ConsoleResponseLoggerFactory()                    
                .GetLogger();
        }

        public void LogFileCreate(string filename)
        {

        }
    }
}
