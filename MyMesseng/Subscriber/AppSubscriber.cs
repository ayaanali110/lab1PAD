using Common;
using System;

namespace Subscriber
{
     class AppSubscriber
     {
          static void Main(string[] args)
          {
               Console.WriteLine("Subscriber...");

               string topic;

               Console.Write("Topic: ");
               topic = Console.ReadLine().ToLower();

               var subscriberSocket = new Subscriber(topic);

               subscriberSocket.Connect(Settings.IP, Settings.PORT);

               Console.WriteLine("Press key to exit...");
               Console.ReadLine();
          }
     }
}
