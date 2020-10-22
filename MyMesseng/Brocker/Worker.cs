using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Brocker
{
     class Worker
     {
          private const int TIME = 500;
          public void MessageWork()
          {
               while (true)
               {
                    while (!Storage.IsEmpty())
                    {
                         var payload = Storage.GetNext();

                         if (payload != null)
                         {
                              var connections = ConnectStorage.GetConnectionInfosByTopic(payload.Topic);

                              foreach(var connection in connections)
                              {
                                   var payloadString = JsonConvert.SerializeObject(payload);
                                   byte[] data = Encoding.UTF8.GetBytes(payloadString);

                                   connection.Socket.Send(data);
                              }
                         }
                    }
                    Thread.Sleep(TIME);
               }
          }
     }
}
