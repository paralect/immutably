using System.Diagnostics;
using Machine.Specifications;

namespace Immutably.Tests.Specs.aggregates.tutorial_aggregates
{
    public class when_configuring_aggregate
    {
        Because of = () =>
        {
            user = new User();

            user.EstablishContext(context => context
                .SetId("user/1")
                .SetVersion(14)
                .SetState(new UserState { Id = "user/1", Name = "John", Score = 100 })
            );

            // Check, that aggregate ID is "user/1"
            Debug.Assert(user.Id == "user/1");

            // Check, that aggregate version is 14
            Debug.Assert(user.CurrentVersion == 14);

            // Check, that state initialized with non default UserState:
            Debug.Assert(user.State.Name == "John");
            Debug.Assert(user.State.Score == 100);

            // Check, that aggregate state wasn't changed
            Debug.Assert(user.Changed == false);

            // Perform operations on aggregate with established context:
            user.ChangeName("Tom");
            

        };

        It should_work = () => {};

        static User user;
    }
}