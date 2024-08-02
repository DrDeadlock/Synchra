using Synchra.Logging.Wrappers;
using Synchra.Logging;

namespace Synchra
{
    class Program
    {
        private static ConsoleCommunicator communicator;
        private static SynchronizationCommunicator syncComm;

        static void Main(string[] args)
        {
            LogConfigurator.Configure();

            communicator = new ConsoleCommunicator();
            communicator.Greet();

            if (args.Length == 0)
                communicator.InformAboutCLAProblem();

            foreach (var arg in args)
            {
                communicator.Write(arg);
            }

            syncComm = new SynchronizationCommunicator();
            syncComm.LogFileCreate("Great File.txt");

            communicator.Farewell();
        }
    }
}
