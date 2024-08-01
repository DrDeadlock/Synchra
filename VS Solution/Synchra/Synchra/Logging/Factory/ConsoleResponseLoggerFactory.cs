using System;
using log4net;
using log4net.Config;

namespace Synchra.Logging
{
    public class ConsoleResponseLoggerFactory : AbstractLoggerFactory
    {
        private string _loggerName;
        public ConsoleResponseLoggerFactory(string pLoggerName)
        {
            _loggerName = pLoggerName;
        }

        public ConsoleResponseLoggerFactory(Type pType)
        {
            _loggerName = pType.ToString();
        }

        public override ILog GetLogger()
        {
            ILog logger = LogManager.GetLogger(_loggerName);
            return logger;
        }
    }
}
