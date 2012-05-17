using System;
using Escolar.Data;
using Machine.Specifications;

namespace Escolar.Tests.Serializy
{
    public class SerializyContext
    {
        Establish context = () =>
        {

        };
    }

    [DataContract("{4380213C-AB42-4D74-82A6-BF1E10F5E749}")]
    public abstract class SomeMessage
    {
        public abstract Guid Id { get; set; }
        public abstract String Name { get; set; }
        public abstract Int32 Year { get; set; }
    }

    [DataProxy(typeof(SomeMessage))]
    public class SomeMessage_Json : SomeMessage
    {
        public override Guid Id { get; set; }
        public override string Name { get; set; }
        public override int Year { get; set; }
    }

}