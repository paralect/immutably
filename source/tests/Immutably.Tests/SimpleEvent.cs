using System;
using Immutably.Messages;

namespace Immutably.Tests
{
    public class SimpleEvent : IEvent
    {
        public String Id { get; set; }
        public Int32 Year { get; set; }
        public String Name { get; set; }
    }
}