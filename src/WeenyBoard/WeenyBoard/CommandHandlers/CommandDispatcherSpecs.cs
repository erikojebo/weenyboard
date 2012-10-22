using NSubstitute;
using NUnit.Framework;
using WeenyBoard.Commands;

namespace WeenyBoard.CommandHandlers
{
    [TestFixture]
    public class CommandDispatcherSpecs
    {
        private CommandDispatcher _dispatcher;

        [SetUp]
        public void SetUp()
        {
            _dispatcher = new CommandDispatcher();
        }

        [Test]
        public void Command_without_handlers_can_be_dispatched_without_exceptions()
        {
            _dispatcher.Dispatch(new DummyCommand());
        }

        [Test]
        public void Dispatching_command_with_single_handler_invokes_that_handler()
        {
            var handler = Substitute.For<IHandleCommands<DummyCommand>>();
            _dispatcher.RegisterHandler(handler);

            var expectedCommand = new DummyCommand();

            _dispatcher.Dispatch(expectedCommand);

            handler.Received().Handle(expectedCommand);
        }

        [Test]
        public void Dispatching_command_with_multiple_handlers_invokes_all_those_handlers()
        {
            var handler1 = Substitute.For<IHandleCommands<DummyCommand>>();
            var handler2 = Substitute.For<IHandleCommands<DummyCommand>>();

            _dispatcher.RegisterHandler(handler1);
            _dispatcher.RegisterHandler(handler2);

            var expectedCommand = new DummyCommand();

            _dispatcher.Dispatch(expectedCommand);

            handler1.Received().Handle(expectedCommand);
            handler2.Received().Handle(expectedCommand);
        }

        [Test]
        public void Dispatching_command_when_handlers_are_registered_for_multiple_different_types_only_invokes_handlers_for_current_command_type()
        {
            var handler1 = Substitute.For<IHandleCommands<DummyCommand>>();
            var handler2 = Substitute.For<IHandleCommands<DummyCommand>>();
            var otherHandler1 = Substitute.For<IHandleCommands<OtherDummyCommand>>();
            var otherHandler2 = Substitute.For<IHandleCommands<OtherDummyCommand>>();

            _dispatcher.RegisterHandler(handler1);
            _dispatcher.RegisterHandler(otherHandler1);
            _dispatcher.RegisterHandler(handler2);
            _dispatcher.RegisterHandler(otherHandler2);

            var expectedCommand = new DummyCommand();

            _dispatcher.Dispatch(expectedCommand);

            handler1.Received().Handle(expectedCommand);
            handler2.Received().Handle(expectedCommand);
        }

        public class DummyCommand : ICommand
        {
             
        }

        public class OtherDummyCommand : ICommand
        {
             
        }
    }
}