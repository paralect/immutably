using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Events
{
    public class Post_DeletedEvent
    {
        public Guid PostId { get; set; }
    }
}