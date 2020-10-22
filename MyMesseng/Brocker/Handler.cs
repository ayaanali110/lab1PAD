using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brocker
{
     class Handler
     {
          public static void Handle(byte[] payloadBytes, ConnectInfo connectionInfo)
          {
               var payloadString = Encoding.UTF8.GetString(payloadBytes);

               if (payloadString.StartsWith("subscribe#"))
               {
                    connectionInfo.Topic = payloadString.Split("subscribe#").LastOrDefault();

                    ConnectStorage.Add(connectionInfo); 
               }
               else
               {
                    Load payload = JsonConvert.DeserializeObject<Load>(payloadString);
                    Storage.Add(payload);
               }
               
          }
     }
}
