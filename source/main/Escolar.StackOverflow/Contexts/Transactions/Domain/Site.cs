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
        public class SiteState : IState
        {
            public Guid Id { get; set; }
            public String Name { get; set; }
            public String Description { get; set; }
        }
        #endregion

        /// <summary>
        /// Create user state
        /// </summary>
        public Site(SiteState state, Action<IEvent> changes)
        {
            _state = state ?? new SiteState();
            //_changes = changes;
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

        #region State reconstruction

        public void On(Site_CreatedEvent evnt)
        {
            State.Id = evnt.SiteId;
            State.Name = evnt.Name;
            State.Description = evnt.Description;
        }

        public void On(Site_NameChangedEvent evnt)
        {
            State.Name = evnt.Name;
        }

        public void On(Site_DescriptionChangedEvent evnt)
        {
            State.Description = evnt.Description;
        }

        #endregion
    }
}