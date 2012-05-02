using System;

namespace Escolar.Messages
{
    public class CommandMetadata : MessageMetadata, ICommandMetadata
    {
        /// <summary>
        /// Id of Aggregate Root, Service or Process that command addressed to
        /// </summary>
        public Guid ReceiverId { get; set; }

        /// <summary>
        /// Expected version of receiver (Aggregate Root, Service, Saga or Process). 
        /// Receiving party can reject command, if expected version doesn't equals to current version of receiver.
        /// </summary>
        public int ExpectedVersion { get; set; }
    }
}