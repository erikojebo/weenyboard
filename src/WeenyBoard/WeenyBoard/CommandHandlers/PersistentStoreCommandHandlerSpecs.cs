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

        [Test]
        public void Handling_a_ChangeSwimLane_command_changes_the_swimlane_for_the_item_in_the_persistent_store()
        {
            var command = new ChangeSwimLaneCommand(new Guid("AFCCC93D-9BD9-4E83-987C-CD2068B905BB"), new Guid("961C16DF-E40E-4A0E-8846-3AD32359662C"), DateTime.Now);

            _handler.Handle(command);

            _repository.Received().ChangeSwimLane(new Guid("AFCCC93D-9BD9-4E83-987C-CD2068B905BB"), new Guid("961C16DF-E40E-4A0E-8846-3AD32359662C"));
        }
    }
}