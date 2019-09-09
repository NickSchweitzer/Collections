using System;
using System.Collections.Generic;

namespace TheCodingMonkey.Collections.Sort
{
    /// <summary>Implementation of a Bubble sort (sinking sort) for IList</summary>
    /// <remarks>More information about QuickSort can be found at <a href="https://en.wikipedia.org/wiki/Bubble_sort">Wikipedia</a> or 
    /// <a href="https://exceptionnotfound.net/bubble-sort-csharp-the-sorting-algorithm-family-reunion/">this blog post</a>.</remarks>
    /// <typeparam name="T">T must implement IComparable</typeparam>
    public class BubbleSort<T> : IInPlaceSort<T>
        where T : IComparable
    {
        /// <summary>Performs an in-place sort of the collection.</summary>
        /// <param name="collection">IList to sort</param>
        public void Sort(IList<T> collection)
        {
            int length = collection.Count;
            T temp;
            for (int i = 0; i <= length - 2; i++)
            {
                for (int j = 0; j <= length - 2; j++)
                {
                    //2. ...if two adjoining elements are in the wrong order, swap them.
                    if (collection[j].CompareTo(collection[j + 1]) > 0)
                    {
                        temp = collection[j + 1];
                        collection[j + 1] = collection[j];
                        collection[j] = temp;
                    }
                    //3. Repeat this for all pairs of elements.
                }
            }
        }
    }
}