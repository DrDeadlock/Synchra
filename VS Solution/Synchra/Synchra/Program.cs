using Synchra.Logging.Wrappers;
using Synchra.Logging;

namespace Synchra
{
    class Program
    {
        private static ConsoleCommunicator communicator;

        static void Main(string[] args)
        {
            LogConfigurator.Configure();

            communicator = new ConsoleCommunicator();
            communicator.Greet();

            if (args.Length == 0)
                communicator.InformAboutCLAProblem();


            communicator.Farewell();
        }
    }
}
