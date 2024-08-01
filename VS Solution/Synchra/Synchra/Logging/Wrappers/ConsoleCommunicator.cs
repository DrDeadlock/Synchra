using log4net;

namespace Synchra.Logging.Wrappers
{
    public class ConsoleCommunicator
    {
        private ILog logger; 
        public ConsoleCommunicator()
        {
            System.Console.WriteLine("Console Communicator Ctor");
            logger =
                new ConsoleResponseLoggerFactory
                    (typeof(ConsoleCommunicator))
                .GetLogger();
        }

        public void GreetUser()
        {            
            logger.Info("Welcome to Synchra, a Veeam Solution for synchronizing you local folders!");
        }
    }
}
