using Broker.Models;
using Broker.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Broker.Services
{
    public class Connection : IConnection
    {
        private readonly List<Models.Connection> _connections;
        private readonly object _locker;

        public Connection()
        {
            _connections = new List<Models.Connection>();
            _locker = new object();
        }

        public Connection(string address, string topic)
        {
            Address = address;
            Topic = topic;
        }

        public string Address { get; }
        public string Topic { get; }

        public void Add(Models.Connection connection)
        {
            lock (_locker)
            {
                _connections.Add(connection);
            }
        }

        public void Add(Connection connection)
        {
            throw new NotImplementedException();
        }

        public IList<Models.Connection> GetConnectionsByTopic(string topic)
        {
            lock (_locker)
            {
                var filteredConnections = _connections.Where(x => x.Topic == topic).ToList();
                return filteredConnections;
            }
        }

        public void Remove(string address)
        {
            lock (_locker)
            {
                _connections.RemoveAll(x => x.Address == address);
            }
        }
    }
}
