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

        public void InfoFileCreate(string filename)
        {
            _logger.Info("The file: " + filename + " has been created.");
        }

        public void InfoFileModified(string filename)
        {
            _logger.Info("The file: " + filename + " has been modified.");
        }

        public void InfoFileRemoved(string filename)
        {
            _logger.Info("The file: " + filename + " has been removed.");
        }

        public void ErrorDuringCreation(string filename)
        {
            _logger.Error("The file" + filename + " could not be created!");
        }

        public void ErrorDuringModification(string filename)
        {
            _logger.Error("The file" + filename + " could not be modified!");
        }

        public void ErrorDuringRemove(string filename)
        {
            _logger.Error("The file" + filename + " could not be removed!");
        }
    }
}
