using System;
using Immutably.Messages;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class Comment_DeleteCommand : ICommand
    {
        public Guid CommentId { get; set; }
    }
}