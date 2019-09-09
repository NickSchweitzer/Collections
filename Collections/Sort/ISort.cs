using System;
using System.Collections.Generic;

namespace TheCodingMonkey.Collections.Sort
{
    /// <summary>Generic interface for all algorithms in this collection that perform an in-place sort of an IList</summary>
    /// <remarks>The passed in Collection is modified by the sorting algorithm.</remarks>
    /// <typeparam name="T">T must implement IComparable</typeparam>
    public interface IInPlaceSort<T>
        where T : IComparable
    {
        /// <summary>Performs an in-place sort of the collection.</summary>
        /// <param name="collection">IList to sort</param>
        void Sort(IList<T> collection);
    }

    /// <summary>Generic interface for all algorithms in this collection that perform an out of place sort of an IList</summary>
    /// <remarks>The passed in Collection is unmodified by the sorting algorithm.</remarks>
    /// <typeparam name="T">T must implement IComparable</typeparam>
    public interface IOutOfPlaceSort<T>
        where T : IComparable
    {
        /// <summary>Performs an in-place sort of the collection.</summary>
        /// <param name="unsorted">IList to sort</param>
        /// <returns>A sorted IList containing the same elements as the original unsorted collection.</returns>
        IList<T> Sort(IList<T> unsorted);
    }
}