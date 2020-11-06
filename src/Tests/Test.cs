using Akka.Actor;
using Akka.TestKit.Xunit2;
using FluentAssertions;
using Xunit;

namespace Tests
{
    public class Test : TestKit
    {
        [Fact]
        public void SyncParentTest()
        {
            // arrange, act
            var childActor = ActorOfAsTestActorRef<ChildActor>(TestActor);

            // asserts
            ExpectMsg<string>().Should().Be("Hello, Father!");
        }

        [Fact]
        public void AsyncParentTest()
        {
            // arrange
            var parentProbe = CreateTestProbe();

            // act
            var childActor = parentProbe.ChildActorOf<ChildActor>();

            // asserts
            parentProbe.ExpectMsg<string>().Should().Be("Hello, Father!");
        }
    }

    internal class ChildActor : ReceiveActor
    {
        public ChildActor() => Context.Parent.Tell("Hello, Father!");
    }
}
