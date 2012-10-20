using System.Collections.Generic;

namespace WeenyBoard.Models
{
    public class Board
    {
        public Board()
        {
            SwimLanes = new List<SwimLane>();
        }

        public IList<SwimLane> SwimLanes { get; set; }
        public string Name { get; set; }
    }
}