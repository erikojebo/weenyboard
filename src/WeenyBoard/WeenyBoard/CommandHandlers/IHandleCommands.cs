using WeenyBoard.Commands;

namespace WeenyBoard.CommandHandlers
{
    public interface IHandleCommands<T> where T : ICommand
    {
        void Handle(T command);
    }
}