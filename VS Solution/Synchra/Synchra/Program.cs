using System;
using Synchra.Logging.Wrappers;
using log4net.Config;

namespace Synchra
{
    class Program
    {
        private static ConsoleCommunicator communicator;

        static void Main(string[] args)
        {
            BasicConfigurator.Configure();

            Console.WriteLine("Hello World!");
            communicator = new ConsoleCommunicator();
            communicator.GreetUser();
        }
    }
}
