using Synchra.Logging.Wrappers;
using Synchra.Logging;
using System.Threading.Tasks;
using System;
using Synchra.Synchronization;
using System.Threading;
using Synchra.CLAValidation;

namespace Synchra
{
    class Program
    {
        private static ConsoleCommunicator consoleComm;
        private static SynchronizationCommunicator syncComm;

        static void Main(string[] args)
        {
            consoleComm = new ConsoleCommunicator();

            LogConfigurator.Configure();
            syncComm = SynchronizationCommunicator.Instance;

            if (!CLAValidator.Build(args))
            {
                Shutdown("The given Command Line arguments were not valid.\n" +
                    "Notice that exactly for arguments have to be passed in" +
                    "the exact following order: \n" +
                    "Path of the Source Folder \n" +
                    "Path of the Destination Folder \n" +
                    "Path of the Log Folder \n" +
                    "Synchronization Period in seconds \n");
                return;
            }

            string srcPath = CLAContext.Instance.SrcPath;
            string destPath = CLAContext.Instance.DestPath;
            string logPath = CLAContext.Instance.LogPath;
            int interval = CLAContext.Instance.Interval;

            LogConfigurator.Configure(logPath);

            consoleComm.Greet();

            Action<string, string> executeSync = SyncPerformer.Execute;
            Task syncTask = new Task(() => executeSync(srcPath, destPath));
            try
            {                
                syncTask.Start();
                while (true)
                {
                    Thread.Sleep(interval * 1000);
                    if (syncTask.IsCompleted)
                    {
                        syncTask = new Task(() => executeSync(srcPath, destPath));
                        syncTask.Start();

                    }
                    if (syncTask.IsFaulted)
                        break;
                }
            }
            catch (Exception ex)
            {
                syncComm.Error(ex.Message);
                syncComm.ErrorShutdown();                
            }

            Shutdown();
        }

        private static void Shutdown(string errorMessage = "")
        {
            if (errorMessage != "")
                syncComm.Error(errorMessage);

            consoleComm.Farewell();
        }


    }
}
