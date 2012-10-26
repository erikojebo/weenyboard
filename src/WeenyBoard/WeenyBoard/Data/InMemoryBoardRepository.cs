using System;
using System.Collections.Generic;
using WeenyBoard.Models;
using System.Linq;

namespace WeenyBoard.Data
{
    public class InMemoryBoardRepository : IBoardRepository
    {
        private Board _board;

        public InMemoryBoardRepository()
        {
            _board = CreateHardCodedBoard();
        }

        public void UpdateItemDescription(Guid id, string newDescription)
        {
            var item = _board.SwimLanes.SelectMany(x => x.Items).FirstOrDefault(x => x.Id == id);

            if (item != null)
                item.Description = newDescription;
        }

        public void ChangeSwimLane(Guid itemId, Guid newSwimLaneId)
        {
            var item = _board.SwimLanes.SelectMany(x => x.Items).First(x => x.Id == itemId);
            var oldLane = _board.SwimLanes.First(l => l.Items.Any(i => i.Id == itemId));
            var newLane = _board.SwimLanes.First(x => x.Id == newSwimLaneId);

            oldLane.Items.Remove(item);
            newLane.Items.Add(item);
        }

        public Board Get()
        {
            return _board;
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
                                                            Id = Guid.NewGuid(),
                                                            Name = "Todo",
                                                            Items = new List<BoardItem>
                                                                        {
                                                                            new BoardItem { Description = "Item 1" },
                                                                            new BoardItem { Description = "Item 2" },
                                                                        }
                                                        },
                                                    new SwimLane
                                                        {
                                                            Id = Guid.NewGuid(),
                                                            Name = "In Progress",
                                                            Items = new List<BoardItem>
                                                                        {
                                                                            new BoardItem { Description = "Item 3" },
                                                                        }
                                                        },
                                                    new SwimLane
                                                        {
                                                            Id = Guid.NewGuid(),
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