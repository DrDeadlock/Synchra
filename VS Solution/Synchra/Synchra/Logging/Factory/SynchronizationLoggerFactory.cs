
using log4net;
using log4net.Appender;
using log4net.Repository;
using Synchra.Logging.Wrappers;
using System.Linq;

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
            ILog logger = LogManager.GetLogger(_loggerName);

            SetFilePath_v1();

            return LogManager.GetLogger(_loggerName);
        }

        private void SetFilePath_v1()
        {
            ILoggerRepository repo = LogManager.GetRepository();
            IAppender[] appenders = repo.GetAppenders();
            foreach (var appender in (from iAppender in appenders
                                      where iAppender is FileAppender
                                      select iAppender))
            {
                if (appender.Name == "FA1")
                {
                    FileAppender fileAppender = appender as FileAppender;
                    fileAppender.File = MockGetLogPath();
                    fileAppender.ActivateOptions();
                }
            }
        }

        private void SetFilePath_v2()
        {
            //Not necessary. Look into the LogConfigurator to find out where
            //the magic happens ;) 
        }

        private string MockGetLogPath()
        {
            return "";
        }
    }
}
