using System;

namespace Immutably.Messages
{
    public class EventMetadata : MessageMetadata, IEventMetadata
    {
        /// <summary>
        /// Id of Aggregate Root, Service or Process that emits this events.
        /// </summary>
        public String SenderId { get; set; }

        /// <summary>
        /// StreamVersion of Aggregate Root, Service, Saga or Process at the moment event was emitted.
        /// Emitting party should increment version and next event should have version incremented by one.
        /// Can be used to preserve ordering of messages inside Aggregate boundary.
        /// Starts from 1.
        /// </summary>
        public int StreamVersion { get; set; }

        /// <summary>
        /// Starts from 1.
        /// </summary>
        public int TransitionSequence { get; set; }

        public Guid DataContractTag
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public DateTime StoredUtc
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Copy event metadata to another object, that implements IEventMetadata
        /// </summary>
        public override void Copy(IMessageMetadata to)
        {
            base.Copy(to);

            var eventMetadata = to as IEventMetadata;

            if (eventMetadata == null)
                return;

            eventMetadata.SenderId = SenderId;
            eventMetadata.StreamVersion = StreamVersion;
            eventMetadata.TransitionSequence = TransitionSequence;
        }
    }
}