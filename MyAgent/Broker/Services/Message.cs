using Broker.Models;
using Broker.Services.Interfaces;
using Google.Protobuf;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Broker.Services
{
    public class Message : Interfaces.IMessage
    {
        private readonly ConcurrentQueue<Models.Message> _messages;

        public Message()
        {
            _messages = new ConcurrentQueue<Models.Message>();
        }



        public void Add(Models.Message message)
        {
            _messages.Enqueue(message);
        }

        public Models.Message GetNext()
        {
            Models.Message message;
            _messages.TryDequeue(out message);

            return message;
        }

        public bool IsEmpty()
        {
            return _messages.IsEmpty;
        }
    }
}
