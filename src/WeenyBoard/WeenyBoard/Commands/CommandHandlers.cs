namespace WeenyBoard.Commands
{
    public class CommandHandlers
    {
    }

    public interface IHandleCommands<T> where T : ICommand
    {
        void Handle(T command);
    }
}