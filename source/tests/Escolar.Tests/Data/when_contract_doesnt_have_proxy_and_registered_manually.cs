using System;
using Escolar.Data;
using Machine.Specifications;

namespace Escolar.Tests.Data
{
    public class when_contract_doesnt_have_proxy_and_registered_manually : DataMachineContext
    {
        Because of = () =>
        {
            context = DataContext.Create(builder => builder
                .AddContract<User>()
            );
        };

        It should_have_the_same_proxy_type = () =>
            context.GetProxy(typeof(User)).ShouldEqual(typeof(User));

        It should_return_correct_tag_for_contract = () =>
            context.GetTag(typeof(User)).ShouldEqual(Guid.Parse("{40952BD5-993B-4706-9442-099C9AD8E33F}"));

        private static DataContext context;
    }
}