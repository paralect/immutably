using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Events
{
    public class Comment_ContentChangedEvent
    {
        public Guid CommentId { get; set; }
        public String Content { get; set; }
    }
}