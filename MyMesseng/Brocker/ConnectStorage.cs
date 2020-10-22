using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brocker
{
     static class ConnectStorage
     {
          private static List<ConnectInfo> _connections;
          private static object _locker;

          static ConnectStorage()
          {
               _connections = new List<ConnectInfo>();
               _locker = new object();
          }

          public static void Add(ConnectInfo connection)
          {
               lock (_locker)
               {
                    _connections.Add(connection);
               }
          }

          public static void Remove(string address)
          {
               lock (_locker)
               {
                    _connections.RemoveAll(x => x.Address == address);
               }
          }

          public static List<ConnectInfo> GetConnectionInfosByTopic(string topic)
          {
               List<ConnectInfo> selectedConnections;

               lock (_locker)
               {
                    selectedConnections = _connections.Where(x => x.Topic == topic).ToList();
               }

               return selectedConnections;
          }
     }
}
