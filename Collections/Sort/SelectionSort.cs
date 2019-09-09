using System;
using System.Collections.Generic;

namespace TheCodingMonkey.Collections.Sort
{
    /// <summary>Implementation of a Selection sort for IList</summary>
    /// <remarks>More information about Selection Sort can be found at <a href="https://en.wikipedia.org/wiki/Selection_sort">Wikipedia</a> or 
    /// <a href="https://exceptionnotfound.net/selection-sort-csharp-the-sorting-algorithm-family-reunion/">this blog post</a>.</remarks>
    /// <typeparam name="T">T must implement IComparable</typeparam>
    public class SelectionSort<T> : IInPlaceSort<T>
        where T : IComparable
    {
        /// <summary>Performs an in-place sort of the collection.</summary>
        /// <param name="collection">IList to sort</param>
        public void Sort(IList<T> collection)
        {
            int count = collection.Count;

            //The algorithm builds the sorted list from the left.
            //1. For each item in the array...
            for (int i = 0; i < count - 1; i++)
            {
                //2. ...assume the first item is the smallest value
                int smallest = i;

                //3. Cycle through the rest of the array
                for (int j = i + 1; j < count; j++)
                {
                    //4. If any of the remaining values are smaller, find the smallest of these
                    if (collection[j].CompareTo(collection[smallest]) < 0)
                        smallest = j;
                }

                //5. Swap the found-smallest value with the current value
                T temp = collection[smallest];
                collection[smallest] = collection[i];
                collection[i] = temp;
            }
        }
    }
}