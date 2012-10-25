using System;

namespace WeenyBoard.Commands
{
    public class AddBoardItemCommand : ICommand
    {
        public AddBoardItemCommand(Guid id, string description, DateTime  timeStamp)
        {
            ItemId = id;
            Description = description;
            TimeStamp = timeStamp;
        }

        public readonly Guid ItemId;
        public readonly string Description;
        public readonly DateTime TimeStamp;

        public void AddBoardItem()
        {
        }
    }
}