using System;
using Immutably.Messages;

namespace Immutably.StackOverflow.Contexts.Transactions.Events
{
    public class Post_TagsChangedEvent : IEvent
    {
        public Guid SenderId { get; set; }
        public String Tags { get; set; }
    }
}