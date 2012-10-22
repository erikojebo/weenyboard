using System;
using NUnit.Framework;
using WeenyBoard.CommandHandlers;

namespace WeenyBoard.Infrastructure
{
    [TestFixture]
    public class ObjectRegistrySpecs
    {
        private ObjectRegistry _registry;

        [SetUp]
        public void SetUp()
        {
            _registry = new ObjectRegistry();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "ISomeInterface", MatchType = MessageMatch.Contains)]
        public void Resolving_an_unregistered_interface_throws_exception_with_error_message_containing_type_name()
        {
            _registry.Resolve<ISomeInterface>();
        }

        [Test]
        public void Resolving_an_unregistered_concrete_class_with_a_default_constructor_returns_instance_of_resolved_type()
        {
            var actual = _registry.Resolve<SomeClassWithDefaultConstructor>();
            Assert.IsInstanceOf<SomeClassWithDefaultConstructor>(actual);
        }

        [Test]
        public void Resolving_interface_registered_with_concrete_class_with_default_constructor_returns_instance_of_registered_class()
        {
            _registry.Register<ISomeInterface, SomeClassWithDefaultConstructor>();
            var actual = _registry.Resolve<ISomeInterface>();

            Assert.IsInstanceOf<SomeClassWithDefaultConstructor>(actual);
        }

        [Test]
        public void Resolving_interface_registered_with_instance_always_returns_same_instance()
        {
            var expectedInstance = new SomeClassWithDefaultConstructor();

            _registry.Register<ISomeInterface>(expectedInstance);

            var actual1 = _registry.Resolve<ISomeInterface>();
            var actual2 = _registry.Resolve<ISomeInterface>();

            Assert.AreSame(expectedInstance, actual1);
            Assert.AreSame(expectedInstance, actual2);
        }

        private interface ISomeInterface
        {
        }

        private class SomeClassWithDefaultConstructor : ISomeInterface
        {
             
        }
    }
}