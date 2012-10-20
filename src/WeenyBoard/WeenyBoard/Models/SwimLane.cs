using System.Collections.Generic;

namespace WeenyBoard.Models
{
    public class SwimLane
    {
        public SwimLane()
        {
            Items = new List<BoardItem>();
        }

        public string Name { get; set; }
        public IList<BoardItem> Items { get; set; }
    }
}