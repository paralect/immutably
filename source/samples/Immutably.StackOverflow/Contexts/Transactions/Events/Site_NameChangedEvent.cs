using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Events
{
    public class Site_NameChangedEvent
    {
        public Guid SiteId { get; set; }
        public String Name { get; set; }
    }
}