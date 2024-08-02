
using log4net;
using Synchra.Logging.Wrappers;

namespace Synchra.Logging.Factory
{
    public class SynchronizationLoggerFactory : AbstractLoggerFactory
    {
        private string _loggerName;

        public SynchronizationLoggerFactory()
        {
            _loggerName = typeof(SynchronizationCommunicator).ToString();
        }

        public override ILog GetLogger()
        {
            //SetFilePath_v1();

            return LogManager.GetLogger(_loggerName);
        }
    }
}
