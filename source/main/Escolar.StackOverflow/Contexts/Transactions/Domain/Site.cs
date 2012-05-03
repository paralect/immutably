using System;
using Escolar.Aggregates;
using Escolar.Messages;
using Escolar.StackOverflow.Commands;
using Escolar.StackOverflow.Events;
using Paralect.Machine.Processes;

namespace Escolar.StackOverflow.Domain
{
    public class Site : Aggregate<Site.SiteState>
    {
        #region Site State
        /// <summary>
        /// Serialization friendly, inner class for Site state
        /// </summary>
        public class SiteState
        {
            public Guid Id { get; set; }
            public String Name { get; set; }
            public String Description { get; set; }

            public void On(Site_CreatedEvent evnt)
            {
                Id = evnt.SiteId;
                Name = evnt.Name;
                Description = evnt.Description;
            }

            public void On(Site_NameChangedEvent evnt)
            {
                Name = evnt.Name;
            }

            public void On(Site_DescriptionChangedEvent evnt)
            {
                Description = evnt.Description;
            }
        }
        #endregion

        /// <summary>
        /// Site changes
        /// </summary>
        private readonly Action<IEvent> _changes;

        /// <summary>
        /// Create user state
        /// </summary>
        public Site(SiteState state, Action<IEvent> changes)
        {
            _state = state ?? new SiteState();
            _changes = changes;
        }

        public void Create(Site_CreateCommand command)
        {
            var evnt = new Site_CreatedEvent()
            {
                SiteId = command.SiteId,
                Name = command.Name,
                Description = command.Description
            };

            Apply(evnt);
        }

        public void ChangeName(Site_ChangeNameCommand command)
        {
            var evnt = new Site_NameChangedEvent()
            {
                SiteId = command.SiteId,
                Name = command.Name,
            };

            Apply(evnt);
        }

        public void ChangeDescription(Site_ChangeDescriptionCommand command)
        {
            var evnt = new Site_DescriptionChangedEvent()
            {
                SiteId = command.SiteId,
                Description = command.Description,
            };

            Apply(evnt);
        }

        public void Apply(IEvent evnt)
        {
            _changes(evnt);
            ((dynamic)_state).On((dynamic) evnt);
        }
    }
}