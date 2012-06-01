using System;

namespace Immutably.Messages
{
    public class CommandMetadata : MessageMetadata, ICommandMetadata
    {
        /// <summary>
        /// Id of Aggregate Root, Service or Process that command addressed to
        /// </summary>
        public String ReceiverId { get; set; }

        /// <summary>
        /// Expected version of receiver (Aggregate Root, Service, Saga or Process). 
        /// Receiving party can reject command, if expected version doesn't equals to current version of receiver.
        /// </summary>
        public int ExpectedVersion { get; set; }

        /// <summary>
        /// Copy event metadata to another object, that implements IEventMetadata
        /// </summary>
        public override void Copy(IMessageMetadata to)
        {
            base.Copy(to);

            var eventMetadata = to as ICommandMetadata;

            if (eventMetadata == null)
                return;

            eventMetadata.ReceiverId = ReceiverId;
            eventMetadata.ExpectedVersion = ExpectedVersion;
        }
    }
}