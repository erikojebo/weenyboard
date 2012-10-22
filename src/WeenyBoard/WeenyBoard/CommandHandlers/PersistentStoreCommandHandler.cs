using WeenyBoard.Commands;
using WeenyBoard.Data;
using WeenyBoard.Infrastructure;

namespace WeenyBoard.CommandHandlers
{
    public class PersistentStoreCommandHandler : IHandleCommands<UpdateItemDescriptionCommand>
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
    }
}