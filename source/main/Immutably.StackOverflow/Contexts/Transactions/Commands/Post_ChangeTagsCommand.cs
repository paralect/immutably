using System;
using Immutably.Messages;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class Post_ChangeTagsCommand : ICommand
    {
        public Guid PostId { get; set; }
        public String Tags { get; set; }
    }
}