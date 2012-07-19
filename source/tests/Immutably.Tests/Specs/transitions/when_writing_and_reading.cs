using System;
using System.Text;
using Lokad.Cqrs.TapeStorage;
using Machine.Specifications;

namespace Immutably.Tests.Specs.transitions
{
    public class when_writing_and_reading
    {
        Because of = () =>
        {
            var container = new FileTapeContainer(@"c:\tmp\trans");
            container.InitializeForWriting();

            var stream = container.GetOrCreateStream("user-45");

            var results = stream.ReadRecords(1, int.MaxValue);

            initialVersion = stream.GetCurrentVersion();

            stream.TryAppend(Encoding.UTF8.GetBytes("Hello2"), TapeAppendCondition.None);

            

        };

        //It initial_version_should_be_zero = () =>
            //initialVersion.ShouldEqual(0);

        private static long initialVersion;
    }
}