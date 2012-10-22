using System.Collections.Generic;
using WeenyBoard.Commands;
using System.Linq;

namespace WeenyBoard.CommandHandlers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        readonly IList<object> _handlers = new List<object>();

        public void Dispatch<T>(T command) where T : ICommand
        {
            foreach (var handler in _handlers.OfType<IHandleCommands<T>>())
            {
                Handle(command, handler);
            }
                
        }

        private void Handle<T>(T command, object handler) where T : ICommand
        {
            var commandHandler = (IHandleCommands<T>)handler;
            commandHandler.Handle(command);
        }

        public void RegisterHandler<T>(IHandleCommands<T> handler) where T : ICommand
        {
            _handlers.Add(handler);
        }
    }

    public interface ICommandDispatcher
    {
        void Dispatch<T>(T command) where T : ICommand;
        void RegisterHandler<T>(IHandleCommands<T> handler) where T : ICommand
;
    }
}