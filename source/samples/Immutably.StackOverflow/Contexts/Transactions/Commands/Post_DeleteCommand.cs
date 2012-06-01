using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class Post_DeleteCommand
    {
        public Guid PostId { get; set; }
    }
}