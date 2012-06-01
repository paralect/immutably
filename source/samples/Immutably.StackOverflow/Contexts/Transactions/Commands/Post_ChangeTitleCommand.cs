using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class Post_ChangeTitleCommand
    {
        public Guid PostId { get; set; }
        public String Title { get; set; }
    }
}