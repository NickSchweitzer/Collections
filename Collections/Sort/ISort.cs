using System;
using System.Collections.Generic;

namespace TheCodingMonkey.Collections.Sort
{
    /// <summary>Generic interface for all algorithms in this collection that perform an in-place sort of an IList</summary>
    /// <typeparam name="T">T must implement IComparable</typeparam>
    public interface ISort<T>
        where T : IComparable
    {
        /// <summary>Performs an in-place sort of the collection.</summary>
        /// <param name="collection">IList to sort</param>
        void Sort(IList<T> collection);
    }
}