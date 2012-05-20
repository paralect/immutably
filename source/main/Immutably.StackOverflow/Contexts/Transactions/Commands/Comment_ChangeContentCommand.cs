using System;
using Immutably.Messages;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class Comment_ChangeContentCommand : ICommand
    {
        public Guid CommentId { get; set; }
        public String Content { get; set; }
    }
}