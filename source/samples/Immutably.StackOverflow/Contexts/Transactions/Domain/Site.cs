using System;
using Immutably.Aggregates;
using Immutably.StackOverflow.Contexts.Transactions.Commands;
using Immutably.StackOverflow.Contexts.Transactions.Events;

namespace Immutably.StackOverflow.Contexts.Transactions.Domain
{
    public class Site : StatefullAggregate<Site.SiteState>
    {
        #region Site State
        /// <summary>
        /// Serialization friendly, inner class for Site state
        /// </summary>
        public abstract class SiteState
        {
            public abstract Guid Id { get; set; }
            public abstract String Name { get; set; }
            public abstract String Description { get; set; }

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

        // [BsonIgnoreExtraElements]
        public class JsonImpl : SiteState
        {
            // [BsonId] 
            public override Guid Id { get; set; }
            public override string Name { get; set; }
            public override string Description { get; set; }
        }

        public class ProtobufImpl : SiteState
        {
            // [ProtoMember]
            public override Guid Id { get; set; }
            public override string Name { get; set; }
            public override string Description { get; set; }
        }
        #endregion

        /// <summary>
        /// Create user state
        /// </summary>
        public Site(SiteState state, Action<Object> changes)
        {
            var json = new JsonImpl();

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
            if (State.Name == null)
                return;

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