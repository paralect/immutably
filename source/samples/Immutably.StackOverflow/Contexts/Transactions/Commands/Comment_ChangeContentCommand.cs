using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class Comment_ChangeContentCommand
    {
        public Guid CommentId { get; set; }
        public String Content { get; set; }
    }
}