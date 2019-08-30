using System;
using System.Collections.Generic;

namespace TheCodingMonkey.Collections.Sort
{
    /// <summary>Implementation of a Quicksort (partition-exchange sort) for IList</summary>
    /// <remarks>More information about QuickSort can be found at <a href="https://en.wikipedia.org/wiki/Quicksort">Wikipedia</a> or 
    /// <a href="https://exceptionnotfound.net/quick-sort-csharp-the-sorting-algorithm-family-reunion/">this blog post</a>.</remarks>
    /// <typeparam name="T">T must implement IComparable</typeparam>
    public class QuickSort<T> : ISort<T>
        where T : IComparable
    {
        /// <summary>Performs an in-place sort of the collection.</summary>
        /// <param name="collection">IList to sort</param>
        public void Sort(IList<T> collection)
        {
            Sort(collection, 0, collection.Count - 1);
        }

        private void Sort(IList<T> collection, int low, int high)
        {
            if (low < high)
            {
                int partitionIndex = Partition(collection, low, high);

                //3. Recursively continue sorting the array
                Sort(collection, low, partitionIndex - 1);
                Sort(collection, partitionIndex + 1, high);
            }
        }

        private int Partition(IList<T> collection, int low, int high)
        {
            //1. Select a pivot point.
            T pivot = collection[high];

            int lowIndex = (low - 1);

            //2. Reorder the collection.
            for (int j = low; j < high; j++)
            {
                if (collection[j].CompareTo(pivot) <= 0)
                {
                    lowIndex++;

                    T temp = collection[lowIndex];
                    collection[lowIndex] = collection[j];
                    collection[j] = temp;
                }
            }

            T temp1 = collection[lowIndex + 1];
            collection[lowIndex + 1] = collection[high];
            collection[high] = temp1;

            return lowIndex + 1;
        }
    }
}