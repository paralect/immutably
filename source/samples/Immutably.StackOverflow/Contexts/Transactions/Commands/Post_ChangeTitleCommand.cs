using System;
using Immutably.Messages;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class Post_ChangeTitleCommand : ICommand
    {
        public Guid PostId { get; set; }
        public String Title { get; set; }
    }
}