using System;
using System.Linq;

namespace WeenyBoard.Extensions
{
    public static class TypeExtensions
    {
         public static bool HasDefaultConstructor(this Type type)
         {
             return type.GetConstructors().Any(x => x.GetParameters().IsEmpty());
         }
    }
}