using Synchra.Logging.Wrappers;
using Synchra.Logging;
using System.Threading.Tasks;
using System;
using Synchra.Synchronization;
using System.Threading;

namespace Synchra
{
    class Program
    {
        private static ConsoleCommunicator communicator;

        static void Main(string[] args)
        {
            string mockSrc = @"/Users/juliusnickel/Documents/TestDirForSynchra/Src";
            string mockDest = @"/Users/juliusnickel/Documents/TestDirForSynchra/Dest";
            string mockLogpath = @"/Users/juliusnickel/Documents/TestDirForSynchra/Logs";
            int mockInterval = 5000;

            LogConfigurator.Configure(mockLogpath);

            communicator = new ConsoleCommunicator();
            communicator.Greet();

            var syncComm = SynchronizationCommunicator.Instance;

            //Validate CLAs

            Action<string, string> executeSync = SyncPerformer.Execute;
            Task syncTask = new Task(() => executeSync(mockSrc, mockDest));
            try
            {                
                syncTask.Start();
                while (true)
                {
                    Thread.Sleep(mockInterval);
                    if (syncTask.IsCompleted)
                    {
                        syncTask = new Task(() => executeSync(mockSrc, mockDest));
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

            communicator.Farewell();
        }


    }
}
