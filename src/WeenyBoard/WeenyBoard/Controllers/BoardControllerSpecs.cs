using System;
using NSubstitute;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using WeenyBoard.CommandHandlers;
using WeenyBoard.Commands;
using WeenyBoard.Data;
using WeenyBoard.Models;

namespace WeenyBoard.Controllers
{
    [TestFixture]
    public class BoardControllerSpecs
    {
        private BoardController _controller;
        private ICommandDispatcher _commandDispatcher;
        private IBoardRepository _boardRepository;

        [SetUp]
        public void SetUp()
        {
            _commandDispatcher = Substitute.For<ICommandDispatcher>();
            _boardRepository = Substitute.For<IBoardRepository>();

            _controller = new BoardController(_commandDispatcher, _boardRepository);
        }

        [Test]
        public void UpdateItemDescription_dispatches_corresponding_command()
        {
            var json = @"{ id: '90B09AAC-0A46-49BE-AEE2-124148C0455D', newDescription: 'new description' }";
            var jObject = JObject.Parse(json);

            _controller.UpdateItemDescription(jObject);

            _commandDispatcher.Received().Dispatch(
                Arg.Is<UpdateItemDescriptionCommand>(x =>
                                                     x.ItemId == new Guid("90B09AAC-0A46-49BE-AEE2-124148C0455D") &&
                                                     x.NewDescription == "new description"));
        }

        [Test]
        public void Get_returns_persisted_board()
        {
            var expectedBoard = new Board();

            _boardRepository.Get().Returns(expectedBoard);

            var actualBoard = _controller.Get();

            Assert.AreSame(expectedBoard, actualBoard);
        }
    }
}