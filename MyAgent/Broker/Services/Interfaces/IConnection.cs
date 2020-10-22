using Broker.Models;
using System.Collections.Generic;


namespace Broker.Services.Interfaces
{
    public interface IConnection
    {
        void Add(Models.Connection connection);

        void Remove(string address);

        IList<Models.Connection> GetConnectionsByTopic(string topic);
        void Add(Connection connection);
    }
}
