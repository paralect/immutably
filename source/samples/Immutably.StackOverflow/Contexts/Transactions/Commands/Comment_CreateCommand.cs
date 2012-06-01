using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class Comment_CreateCommand
    {
        public Guid CommentId { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public String Content { get; set; }
    }
}