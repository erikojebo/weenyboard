﻿using System;
using WeenyBoard.Models;

namespace WeenyBoard.Data
{
    public interface IBoardRepository
    {
        void UpdateItemDescription(Guid id, string newDescription);
        Board Get();
    }
}