using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Events
{
    public class Site_NameChangedEvent : IEvent
    {
        public Guid SiteId { get; set; }
        public String Name { get; set; }
    }
}