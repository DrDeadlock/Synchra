using System;
using log4net;
using Synchra.Logging.Wrappers;

namespace Synchra.Logging
{
    public class ConsoleResponseLoggerFactory : AbstractLoggerFactory
    {
        private string _loggerName;

        public ConsoleResponseLoggerFactory()
        {
            //TODO: Obsidian - Resources/Tasks/LogTasks
            //#H1 if there is enough time left
            _loggerName = (typeof(ConsoleCommunicator).ToString());
        }

        public override ILog GetLogger()
        {
            return LogManager.GetLogger(_loggerName);
        }
    }
}
