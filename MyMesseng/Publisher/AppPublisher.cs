using Common;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Text;

namespace Publisher
{
     class AppPublisher
     {
          static void Main(string[] args)
          {
               Console.WriteLine("Publisher...");

               var publisher = new Publisher();
                publisher.Connect(Settings.IP, Settings.PORT);

               if (publisher.IsConnected)
               {
                    while (true)
                    {
                         var load = new Load();

                         Console.Write("Topic: ");
                        load.Topic = Console.ReadLine().ToLower();

                         Console.Write("Message: ");
                        load.Message = Console.ReadLine();

                         var payloadString = JsonConvert.SerializeObject(load);
                         byte[] data = Encoding.UTF8.GetBytes(payloadString);

                         publisher.Send(data);
                    }
               }
               
          }
     }
}
