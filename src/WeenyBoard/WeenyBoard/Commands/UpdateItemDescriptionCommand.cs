using System;

namespace WeenyBoard.Commands
{
    public class UpdateItemDescriptionCommand : ICommand
    {
        public UpdateItemDescriptionCommand(Guid itemId, string newDescription, DateTime dateTime)
        {
            ItemId = itemId;
            NewDescription = newDescription;
            DateTime = dateTime;
        }

        public readonly Guid ItemId;
        public readonly string NewDescription;
        public readonly DateTime DateTime;
    }
}