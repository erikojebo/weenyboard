using System.Collections.Generic;
using WeenyBoard.Commands;

namespace WeenyBoard.CommandHandlers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        readonly IList<object> _handlers = new List<object>();

        public void Dispatch(ICommand command)
        {
        
        }

        public void RegisterHandler<T>(IHandleCommands<T> handler) where T : ICommand
        {
            _handlers.Add(handler);
        }
    }

    public interface ICommandDispatcher
    {
        void Dispatch(ICommand command);
        void RegisterHandler<T>(IHandleCommands<T> handler) where T : ICommand
;
    }
}