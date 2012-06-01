using System;
using Immutably.Messages;

namespace Immutably.StackOverflow.Contexts.Transactions.Events
{
    public class Post_ContentChangedEvent : IMessage
    {
        public Guid SenderId { get; set; }
        public String Title { get; set; }
    }
}