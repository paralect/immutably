using System;
using Escolar.Data;
using Machine.Specifications;

namespace Escolar.Tests.Data
{
    public class DataMachineContext
    {
        Establish context = () =>
        {

        };
    }

    [DataContract("{40952BD5-993B-4706-9442-099C9AD8E33F}")]
    public abstract class User
    {
        public abstract Guid Id { get; set; }
        public abstract Int32 Amount { get; set; }
    }

    [DataProxy]
    public class User_Bson : User
    {
        public override Guid Id { get; set; }
        public override int Amount { get; set; }
    }
}