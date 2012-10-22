using System;
using NSubstitute;
using NUnit.Framework;
using WeenyBoard.Commands;
using WeenyBoard.Data;

namespace WeenyBoard.CommandHandlers
{
    [TestFixture]
    public class PersistentStoreCommandHandlerSpecs
    {
        private PersistentStoreCommandHandler _handler;
        private IBoardRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IBoardRepository>();
            _handler = new PersistentStoreCommandHandler(_repository);
        }

        [Test]
        public void Handling_an_UpdateItemDescription_command_updates_the_item_description_in_the_persistent_store()
        {
            var command = new UpdateItemDescriptionCommand(new Guid("AFCCC93D-9BD9-4E83-987C-CD2068B905BB"), "new description", DateTime.Now);

            _handler.Handle(command);
            
            _repository.Received().UpdateItemDescription(new Guid("AFCCC93D-9BD9-4E83-987C-CD2068B905BB"), "new description");
        }
    }
}