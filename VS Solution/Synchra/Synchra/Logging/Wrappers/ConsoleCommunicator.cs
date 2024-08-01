using log4net;

namespace Synchra.Logging.Wrappers
{
    public class ConsoleCommunicator
    {
        private ILog logger; 
        public ConsoleCommunicator()
        {
            logger =
                new ConsoleResponseLoggerFactory
                    (typeof(ConsoleCommunicator))
                .GetLogger();
        }

        public void Write(string message)
        {
            logger.Info(message);
        }

        public void Greet()
        {            
            logger.Info("Welcome to Synchra, a Veeam Solution to synchronize you local folders!");            
        }

        public void InformAboutCLAProblem()
        {
            System.Console.WriteLine();
            logger.Info("It seems that there is a problem with your command line arguments.");
            logger.Info("Please beware of the following arguments you have to pass:");
            logger.Info("The path to your source directory you want to synchronize.");
            logger.Info("The path to your destination directory which will become an exact copy of source directory.");
            logger.Info("The path to your log directory, where necessary logs will be put into.");
            logger.Info("A time interval for the period of synchronization in seconds.");
            System.Console.WriteLine();
            logger.Info("Please start the program again with the correct order and format of command line arguments.");
            logger.Info("If you need help consider starting the program with the command line argument -h to get further information.");

        }

        public void Farewell()
        {
            System.Console.WriteLine();
            logger.Info("Thank you for using Synchra, a Veeam Solution to synchronize you local folders!");
            logger.Info("The Application exits now.");
        }
    }
}
