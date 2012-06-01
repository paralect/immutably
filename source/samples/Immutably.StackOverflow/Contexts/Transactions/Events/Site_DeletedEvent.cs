using System;
using Immutably.Messages;

namespace Immutably.StackOverflow.Contexts.Transactions.Events
{
    public class Site_DeletedEvent : IMessage
    {
        public Guid SiteId { get; set; }
    }
}