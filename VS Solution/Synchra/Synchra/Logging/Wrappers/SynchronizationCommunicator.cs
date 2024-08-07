using log4net;
using Synchra.Logging.Factory;

namespace Synchra.Logging.Wrappers
{
    public class SynchronizationCommunicator
    {
        #region Singleton
        private static SynchronizationCommunicator _instance;

        public static SynchronizationCommunicator Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SynchronizationCommunicator();
                return _instance;
            }
        }

        private SynchronizationCommunicator()
        {
            _logger =
                new SynchronizationLoggerFactory()                    
                .GetLogger();
        }
        
        #endregion

        private readonly ILog _logger;

        public void InfoText(string text)
        {
            _logger.Info(text);
        }

        public void InfoSyncStarted()
        {
            _logger.Info("Synchronization Process started.");
        }

        public void InfoSyncCompleted()
        {
            _logger.Info("Synchronization Process completed.");
        }

        public void InfoFileCreated(string filename)
        {
            _logger.Info("The file: " + filename + " has been created.");
        }

        public void InfoFileUpdated(string filename)
        {
            _logger.Info("The file: " + filename + " has been modified.");
        }

        public void InfoFileDeleted(string filename)
        {
            _logger.Info("The file: " + filename + " has been removed.");
        }

        public void InfoDirectoryCreated(string directoryName)
        {
            _logger.Info("The Directory: " + directoryName + " has been created.");
        }

        public void InfoDirectoryDeleted(string directoryName)
        {
            _logger.Info("The Directory: " + directoryName + " has been removed.");
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }                

        public void WarnSrcOrDestNewCreated()
        {
            _logger.Warn("The Source or Destination path was not found and would " +
                "newly created during synchronization. This might be happened " +
                "because you moved or deleted one of these folders. " +
                "If you want to change the folders for sychronization please " +
                "restart Synchra and enter the new Paths for Source and Destination.");
        }

        public void ErrorShutdown()
        {
            _logger.Error("Due to an error, the synchronization process has been stopped. " +
                "Please inspect the previous log entries for further information " +
                "and restart Synchra after the problem sources have been removed.");
        }
    }
}
