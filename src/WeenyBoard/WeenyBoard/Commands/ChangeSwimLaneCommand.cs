using System;

namespace WeenyBoard.Commands
{
    public class ChangeSwimLaneCommand : ICommand
    {
        public ChangeSwimLaneCommand(Guid itemId, Guid newSwimLaneId, DateTime dateTime)
        {
            ItemId = itemId;
            NewSwimLaneId = newSwimLaneId;
            DateTime = dateTime;
        }

        public readonly Guid ItemId;
        public readonly Guid NewSwimLaneId;
        public readonly DateTime DateTime;
    }
}