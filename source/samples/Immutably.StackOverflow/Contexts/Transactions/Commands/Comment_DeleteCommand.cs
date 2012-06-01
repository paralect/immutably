using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class Comment_DeleteCommand
    {
        public Guid CommentId { get; set; }
    }
}