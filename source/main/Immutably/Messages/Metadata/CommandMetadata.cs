namespace Immutably.Messages
{
    public class CommandMetadata<TId> : MessageMetadata, ICommandMetadata<TId>
    {
        /// <summary>
        /// Id of Aggregate Root, Service or Process that command addressed to
        /// </summary>
        public TId ReceiverId { get; set; }

        /// <summary>
        /// Expected version of receiver (Aggregate Root, Service, Saga or Process). 
        /// Receiving party can reject command, if expected version doesn't equals to current version of receiver.
        /// </summary>
        public int ExpectedVersion { get; set; }
    }
}