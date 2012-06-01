using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Events
{
    public class Post_TagsChangedEvent
    {
        public Guid SenderId { get; set; }
        public String Tags { get; set; }
    }
}