using System;

namespace WeenyBoard.Models
{
    public class BoardItem
    {
        public BoardItem()
        {
            Id = Guid.NewGuid();
        }

        public string Description { get; set; }
        public Guid Id { get; set; }
    }
}