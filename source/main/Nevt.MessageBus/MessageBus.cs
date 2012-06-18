using System;

namespace Nevt.MessageBus
{
    public class Message<TData>
    {
        public TData Body { get; set; }
        public Int32 MessageId { get; set; }
    }

    public class MessageContext
    {
        public void Reply() {}
    }

    public class MessageBus
    {
        public void Send(String address, Object message)
        {
            
        }

        public void RegisterHandler<TData>(String address, Action<TData> handler)
        {
             
        }

        public void RegisterHandler<TData>(String address, Action<TData, MessageContext> handler)
        {
             
        }
    }
}