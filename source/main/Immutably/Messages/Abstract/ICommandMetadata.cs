
using System;

namespace Immutably.Messages
{
    public interface ICommandMetadata<TReceiverId> : IMessageMetadata
    {
        /// <summary>
        /// Id of Aggregate Root, Service, Saga or Process this commant addressed to
        /// </summary>
        TReceiverId ReceiverId { get; set; }

        /// <summary>
        /// Expected version of receiver (Aggregate Root, Service, Saga or Process). 
        /// Receiving party can reject command, if expected version doesn't equals to current version of receiver.
        /// </summary>
        Int32 ExpectedVersion { get; set; }
    }
}