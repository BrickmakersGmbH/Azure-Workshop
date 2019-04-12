using System;
using System.Collections.Generic;
using System.Linq;

namespace CatDogCore.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> enumerable, int partitionSize)
        {
            return enumerable
                .Select((item, index) => new { Index = index, Item = item })
                .GroupBy(indexedItem => indexedItem.Index / partitionSize)
                .Select(group => group.Select(indexedItem => indexedItem.Item));
        }
    }
}
