using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Events
{
    public class Site_DescriptionChangedEvent : IEvent
    {
        public Guid SiteId { get; set; }
        public String Description { get; set; }
    }
}