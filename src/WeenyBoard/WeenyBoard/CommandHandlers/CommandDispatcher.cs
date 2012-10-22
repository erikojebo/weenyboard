using WeenyBoard.Commands;

namespace WeenyBoard.CommandHandlers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        public void Dispatch(ICommand command)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface ICommandDispatcher
    {
        void Dispatch(ICommand command);
    }
}