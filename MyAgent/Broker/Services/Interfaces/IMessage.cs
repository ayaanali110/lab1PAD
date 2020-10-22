using Broker.Models;

namespace Broker.Services.Interfaces
{
    public interface IMessage
    {
        void Add(Models.Message message);

        Models.Message GetNext();

        bool IsEmpty();
    }
}
