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

        private class DummyCommand : ICommand
        {
             
        }
    }
}