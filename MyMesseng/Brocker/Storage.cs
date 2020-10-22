using Common;
using System.Collections.Concurrent;

namespace Brocker
{
     static class Storage
     {
          private static ConcurrentQueue<Load> _payloadQueue;


          static Storage()
          {
               _payloadQueue = new ConcurrentQueue<Load>();
          }

          public static void Add(Load payload)
          {
               _payloadQueue.Enqueue(payload);
          }

          public static Load GetNext()
          {
               Load payload = null;
               _payloadQueue.TryDequeue(out payload);
               return payload;
          }

          public static bool IsEmpty()
          {
               return _payloadQueue.IsEmpty;
          }
     }
}
