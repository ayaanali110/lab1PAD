using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriber
{
     class Handler
     {
          public static void Handle(byte[] payloadBytes)
          {
               var payloadString = Encoding.UTF8.GetString(payloadBytes);
               var payload = JsonConvert.DeserializeObject<Load>(payloadString);

               Console.WriteLine(payload.Message);
          }
     }
}
