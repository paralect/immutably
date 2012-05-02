using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Events
{
    public class Site_DeletedEvent : IEvent
    {
        public Guid SiteId { get; set; }
    }
}