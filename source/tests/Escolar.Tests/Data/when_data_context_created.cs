using System;
using Escolar.Data;
using Machine.Specifications;

namespace Escolar.Tests.Data
{
    public class when_data_context_created : DataMachineContext
    {
        Because of = () =>
        {
            var context = DataContext.Create(builder => builder
                .AddContract<User>()
                .AddProxy<User_Bson>()
            );

            contractType = context.GetProxy(typeof (User));
        };

        It should_be_correct_contract = () =>
            contractType.ShouldEqual(typeof(User_Bson));

        private static Type contractType;
    }
}