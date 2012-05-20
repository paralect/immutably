using System;
using Immutably.Messages;

namespace Immutably.StackOverflow.Contexts.Transactions.Events
{
    public class Site_CreatedEvent : IEvent 
    {
        public Guid SiteId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }
}