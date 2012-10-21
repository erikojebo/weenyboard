﻿using System.Collections.Generic;
using System.Web.Http;
using WeenyBoard.Models;

namespace WeenyBoard.Controllers
{
    public class BoardController : ApiController
    {
        private Board _board;

        // GET api/board
        public Board Get()
        {
            _board = CreateHardCodedBoard();

            return _board;
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
                                                                            new BoardItem { Description = "Item 6" },
                                                                        }
                                                        },
                                                }
                            };
            return board;
        }
    }
}