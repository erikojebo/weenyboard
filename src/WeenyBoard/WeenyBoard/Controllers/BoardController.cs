using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using WeenyBoard.CommandHandlers;
using WeenyBoard.Commands;
using WeenyBoard.Data;
using WeenyBoard.Infrastructure;
using WeenyBoard.Models;

namespace WeenyBoard.Controllers
{
    public class BoardController : ApiController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IBoardRepository _boardRepository;

        public BoardController()
            : this(ObjectRegistry.Instance.Resolve<ICommandDispatcher>(), ObjectRegistry.Instance.Resolve<IBoardRepository>())
        {
        }

        public BoardController(ICommandDispatcher commandDispatcher, IBoardRepository boardRepository)
        {
            _commandDispatcher = commandDispatcher;
            _boardRepository = boardRepository;
        }

        // GET api/board
        public Board Get()
        {
            return _boardRepository.Get();
        }

        public void UpdateItemDescription([FromBody]JToken token)
        {
            dynamic data = token;

            var id = Guid.Parse((string)data.id);
            string newDescription = data.newDescription;

            var command = new UpdateItemDescriptionCommand(id, newDescription, DateTime.Now);
            _commandDispatcher.Dispatch(command);
        }

        // GET api/values/5

        public string Get(int id)
        {
            return "value";
        }

        public void Post([FromBody]JToken token)
        {
            dynamic data = token;

            var id = Guid.Parse((string)data.id);
            string newDescription = data.newDescription;

            var command = new AddBoardItemCommand(id, newDescription, DateTime.Now);
            _commandDispatcher.Dispatch(command);
        }

        // PUT api/values/5

        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5

        public void Delete(int id)
        {
        }
    }
}