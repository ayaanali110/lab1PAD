using Common;
using System;
using System.Threading.Tasks;

namespace Brocker
{
     class AppBrocker
     {
          static void Main(string[] args)
          {
               Console.WriteLine("Brocker...");

               Brocker socket = new Brocker();
               socket.Start(Settings.IP, Settings.PORT);

               var worker = new Worker();
               Task.Factory.StartNew(worker.MessageWork, TaskCreationOptions.LongRunning);
               Console.ReadLine();
          }
     }
}
