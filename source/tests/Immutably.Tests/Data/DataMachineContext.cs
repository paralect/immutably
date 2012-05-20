using System;
using Immutably.Data;
using Machine.Specifications;

namespace Immutably.Tests.Data
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

    [DataContract("{F4DF329F-7F5C-4662-8F27-2B923C33DDEA}")]
    public abstract class SpecialUser : User
    {
        public abstract Int32 Years { get; set; }
    }

    [DataProxy]
    public class User_Bson : User
    {
        public override Guid Id { get; set; }
        public override int Amount { get; set; }
    }    
    
    [DataProxy]
    public class SpecialUser_Bson : SpecialUser
    {
        public override Guid Id { get; set; }
        public override int Amount { get; set; }
        public override int Years { get; set; }
    }

    [DataContract("{B04AD5BE-F0D6-4471-8F4E-C621D1A4146C}")]
    public class ContractWithoutProxy
    {
        public virtual Guid Id { get; set; }
        public virtual Int32 Amount { get; set; }
    }
}