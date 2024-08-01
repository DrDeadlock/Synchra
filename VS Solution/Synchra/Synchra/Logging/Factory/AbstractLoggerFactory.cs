using System;
using log4net;

namespace Synchra.Logging
{
    public abstract class AbstractLoggerFactory
    {
        public abstract ILog GetLogger();
    }
}
