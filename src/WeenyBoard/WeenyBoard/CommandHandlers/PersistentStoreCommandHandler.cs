using WeenyBoard.Commands;
using WeenyBoard.Data;
using WeenyBoard.Infrastructure;

namespace WeenyBoard.CommandHandlers
{
    public class PersistentStoreCommandHandler : IHandleCommands<UpdateItemDescriptionCommand>, IHandleCommands<ChangeSwimLaneCommand>
    {
        private IBoardRepository _repository;

        public PersistentStoreCommandHandler() : this(ObjectRegistry.Instance.Resolve<IBoardRepository>())
        {
        }

        public PersistentStoreCommandHandler(IBoardRepository repository)
        {
            _repository = repository;
        }

        public void Handle(UpdateItemDescriptionCommand command)
        {
            _repository.UpdateItemDescription(command.ItemId, command.NewDescription);
        }

        public void Handle(ChangeSwimLaneCommand command)
        {
            _repository.ChangeSwimLane(command.ItemId, command.NewSwimLaneId);
        }
    }
}