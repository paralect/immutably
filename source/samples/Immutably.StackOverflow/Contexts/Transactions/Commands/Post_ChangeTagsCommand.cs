using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class Post_ChangeTagsCommand
    {
        public Guid PostId { get; set; }
        public String Tags { get; set; }
    }
}