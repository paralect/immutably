using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Events
{
    public class Comment_CreatedEvent
    {
        public Guid CommentId { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public String Content { get; set; }
    }
}