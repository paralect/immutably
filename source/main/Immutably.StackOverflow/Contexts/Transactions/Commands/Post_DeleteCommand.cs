using System;
using Immutably.Messages;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class Post_DeleteCommand : ICommand
    {
        public Guid PostId { get; set; }
    }
}