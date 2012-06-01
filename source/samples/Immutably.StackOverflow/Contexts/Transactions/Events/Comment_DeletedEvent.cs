using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Events
{
    public class Comment_DeletedEvent
    {
        public Guid CommentId { get; set; }
    }
}