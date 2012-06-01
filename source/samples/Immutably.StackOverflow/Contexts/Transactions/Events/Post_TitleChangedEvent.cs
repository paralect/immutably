using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Events
{
    public class Post_TitleChangedEvent
    {
        public Guid SenderId { get; set; }
        public String Title { get; set; }
    }
}