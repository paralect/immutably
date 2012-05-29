using System;
using Immutably.Data;
using Machine.Specifications;

namespace Immutably.Tests.Specs.data.empty_context
{
    public class when_contracts_has_inheritance : _context
    {
        Because of = () =>
        {
            context = DataContext.Create(builder => builder
                .AddAssemblyContracts(typeof(_context).Assembly, typeof(_context).Namespace)
                .AddAssemblyProxies(typeof(_context).Assembly, typeof(_context).Namespace)
            );
        };

        It should_correctly_find_proxy_type_for_base_type = () =>
            context.GetProxy(typeof (User))
                .ShouldEqual(typeof(User_Bson));

        It should_correctly_find_proxy_type_for_delivered_type = () =>
            context.GetProxy(typeof (SpecialUser))
                .ShouldEqual(typeof(SpecialUser_Bson));

        It should_return_correct_tag_for_base_contract = () =>
            context.GetTag(typeof(User))
                .ShouldEqual(Guid.Parse("{40952BD5-993B-4706-9442-099C9AD8E33F}"));

        It should_return_correct_tag_for_delivered_contract = () =>
            context.GetTag(typeof(SpecialUser))
                .ShouldEqual(Guid.Parse("{F4DF329F-7F5C-4662-8F27-2B923C33DDEA}"));

        It should_return_correct_tag_for_proxy_of_base_contract = () =>
            context.GetTag(typeof(User_Bson))
                .ShouldEqual(Guid.Parse("{40952BD5-993B-4706-9442-099C9AD8E33F}"));

        It should_return_correct_tag_for_proxy_of_delivered_contract = () =>
            context.GetTag(typeof(SpecialUser_Bson))
                .ShouldEqual(Guid.Parse("{F4DF329F-7F5C-4662-8F27-2B923C33DDEA}"));

        private static IDataContext context;
    }
}