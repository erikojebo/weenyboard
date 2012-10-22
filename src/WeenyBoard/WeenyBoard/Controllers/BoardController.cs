using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using WeenyBoard.CommandHandlers;
using WeenyBoard.Commands;
using WeenyBoard.Infrastructure;
using WeenyBoard.Models;

namespace WeenyBoard.Controllers
{
    public class BoardController : ApiController
    {
        private Board _board;
        private ICommandDispatcher _commandDispatcher;

        public BoardController()
        {
            _commandDispatcher = ObjectRegistry.Instance.Resolve<ICommandDispatcher>();
        }

        public BoardController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        // GET api/board
        public Board Get()
        {
            _board = CreateHardCodedBoard();

            return _board;
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

        // POST api/values

        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5

        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5

        public void Delete(int id)
        {
        }

        private static Board CreateHardCodedBoard()
        {
            var board = new Board
                            {
                                Name = "Testing board name",
                                SwimLanes = new List<SwimLane>
                                                {
                                                    new SwimLane
                                                        {
                                                            Name = "Todo",
                                                            Items = new List<BoardItem>
                                                                        {
                                                                            new BoardItem { Description = "Item 1" },
                                                                            new BoardItem { Description = "Item 2" },
                                                                        }
                                                        },
                                                    new SwimLane
                                                        {
                                                            Name = "In Progress",
                                                            Items = new List<BoardItem>
                                                                        {
                                                                            new BoardItem { Description = "Item 3" },
                                                                        }
                                                        },
                                                    new SwimLane
                                                        {
                                                            Name = "Done",
                                                            Items = new List<BoardItem>
                                                                        {
                                                                            new BoardItem { Description = "Item 4" },
                                                                            new BoardItem { Description = "Item 5" },
                                                                            new BoardItem { Description = "Item 6, which has a very long description to simplify styling work" },
                                                                        }
                                                        },
                                                }
                            };
            return board;
        }
    }
}