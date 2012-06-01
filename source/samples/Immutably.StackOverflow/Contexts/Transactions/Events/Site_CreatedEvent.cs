using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Events
{
    public class Site_CreatedEvent
    {
        public Guid SiteId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }
}