using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Events
{
    public class Site_DescriptionChangedEvent
    {
        public Guid SiteId { get; set; }
        public String Description { get; set; }
    }
}