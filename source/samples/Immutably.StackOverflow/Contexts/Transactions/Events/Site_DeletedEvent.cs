using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Events
{
    public class Site_DeletedEvent
    {
        public Guid SiteId { get; set; }
    }
}