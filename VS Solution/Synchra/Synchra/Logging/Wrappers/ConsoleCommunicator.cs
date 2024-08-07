using log4net;

namespace Synchra.Logging.Wrappers
{
    public class ConsoleCommunicator
    {
        private readonly ILog _logger; 
        public ConsoleCommunicator()
        {
            _logger =
                new ConsoleResponseLoggerFactory()
                .GetLogger();
        }

        public void Write(string message)
        {
            _logger.Info(message);
        }

        public void Greet()
        {            
            _logger.Info("Welcome to Synchra, a Veeam Solution to synchronize you local folders!");            
        }

        public void InformAboutCLAProblem()
        {
            _logger.Info("");
            _logger.Info("It seems that there is a problem with your command line arguments.");
            _logger.Info("Please beware of the following arguments you have to pass:");
            _logger.Info("The path to your source directory you want to synchronize.");
            _logger.Info("The path to your destination directory which will become an exact copy of source directory.");
            _logger.Info("The path to your log directory, where necessary logs will be put into.");
            _logger.Info("A time interval for the period of synchronization in seconds.");
            _logger.Info("");
            _logger.Info("Please start the program again with the correct order and format of command line arguments.");
            _logger.Info("If you need help consider starting the program with the command line argument -h to get further information.");

        }

        public void Farewell()
        {
            _logger.Info("");
            _logger.Info("Thank you for using Synchra, a Veeam Solution to synchronize you local folders!");
            _logger.Info("The Application exits now.");
        }
    }
}
