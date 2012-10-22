using System;
using System.Collections.Generic;
using System.Linq;

namespace WeenyBoard.Extensions
{
    public static class EnumerableExtensions
    {
         public static bool IsEmpty<T>(this IEnumerable<T> e)
         {
             return !e.Any();
         }
    }
}