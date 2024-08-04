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

        public void ErrorPermissionMissing(string filename)
        {
            _logger.Error("The File: " + filename + " could not be read or modified " +
                "due to its protection level!");
        }

        public void ErrorPathTooLong(string path)
        {
            _logger.Error("The Path: " + path + " is too long to be handable by Synchra. " +
                "Please consider putting your source folder and/or destination folder " +
                "not so deeply nested in other folders.");
        }

        public void WarnSrcOrDestNewCreated()
        {
            _logger.Warn("The Source or Destination path was not found and would " +
                "newly created during synchronization. This might be happened " +
                "because you moved or deleted one of these folders. " +
                "If you want to change the folders for sychronization please " +
                "restart Synchra and enter the new Paths for Source and Destination.");
        }
    }
}
