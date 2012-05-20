using System;
using Immutably.Messages;

namespace Immutably.StackOverflow.Contexts.Transactions.Events
{
    public class Site_DescriptionChangedEvent : IEvent
    {
        public Guid SiteId { get; set; }
        public String Description { get; set; }
    }
}