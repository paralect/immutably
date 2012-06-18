using System.Diagnostics;
using Immutably.Tests.Specs.aggregates.empty_context.tutorial_aggregates;
using Machine.Specifications;

namespace Immutably.Tests.Specs.aggregates.tutorial_aggregates
{
    public class when_tutorial_aggregate_created
    {
        Because of = () =>
        {
            // Create your aggregate object
            user = new User();

            // Perform first operation
            user.CreateUser("user/1", "John Olson", 30);

            // Perform second operation
            user.ChangeName("Bob Dylan");

            // Perform third operation
            user.ChangeScore(100);

            // Check, that name now should be "Bob Dylan"
            Debug.Assert(user.State.Name == "Bob Dylan");

            // Check, that score should be 100
            Debug.Assert(user.State.Score == 100);

            // Check, that aggregate state was changed
            Debug.Assert(user.Changed == true);

            // Check, that aggregate has 3 changes 
            Debug.Assert(user.Changes.Count == 3);

            // Check, that aggregate version is 1
            Debug.Assert(user.CurrentVersion == 1);

            // Check, that version of aggregate, before your changes was 0
            Debug.Assert(user.InitialVersion == 0);
        };

        It version_should_be_zero = () =>
            user.InitialVersion.ShouldEqual(0);

        static User user;
    }
}