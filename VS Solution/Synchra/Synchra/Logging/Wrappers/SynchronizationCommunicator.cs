using log4net;
using Synchra.Logging.Factory;

namespace Synchra.Logging.Wrappers
{
    public class SynchronizationCommunicator
    {
        private readonly ILog _logger;

        public SynchronizationCommunicator()
        {
            _logger =
                new SynchronizationLoggerFactory()                    
                .GetLogger();
        }

        public void LogFileCreate(string filename)
        {
            _logger.Info("The file: " + filename + " has been created.");
        }
    }
}
