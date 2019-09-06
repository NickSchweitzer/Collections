using System;
using System.Collections.Generic;

namespace TheCodingMonkey.Collections.Sort
{
    /// <summary>Implementation of a Bitonic Merge Sort (partition-exchange sort) for IList</summary>
    /// <remarks>The collection being sorted must have a length that is a Power of 2. More information about Bitonic Merge Sorting can be found 
    /// at <a href="https://en.wikipedia.org/wiki/Bitonic_sorter">Wikipedia</a> or 
    /// <a href="https://exceptionnotfound.net/bitonic-merge-sort-csharp-the-sorting-algorithm-family-reunion/">this blog post</a>.</remarks>
    /// <typeparam name="T">T must implement IComparable</typeparam>
    public class BitonicMergeSort<T> : ISort<T>
        where T : IComparable
    {
        /// <summary>Performs an in-place sort of the collection.</summary>
        /// <param name="collection">IList to sort</param>
        /// <param name="direction">Determines whether sort is Ascending or Descending</param>
        /// <exception cref="ArgumentException">Thron if collection.Count is not a Power of 2</exception>
        public void Sort(IList<T> collection, Direction direction)
        {
            int count = collection.Count;
            if (!IsPowerOfTwo(count))
                throw new ArgumentException("Collection Count must be a Power of 2");

            BitonicSort(collection, 0, count, direction);
        }

        /// <summary>Performs an in-place sort of the collection.</summary>
        /// <param name="collection">IList to sort</param>
        /// <exception cref="ArgumentException">Thron if collection.Count is not a Power of 2</exception>
        public void Sort(IList<T> collection)
        {
            Sort(collection, Direction.Ascending);
        }

        //This function first produces a bitonic sequence by recursively  
        //sorting its two halves in opposite sorting directions, and then  
        //calls BitonicMerge to make them in the same direction
        private void BitonicSort(IList<T> collection, int low, int count, Direction direction)
        {
            if (count > 1)
            {
                int k = count / 2;

                // sort left side in ascending order
                BitonicSort(collection, low, k, Direction.Ascending);

                // sort right side in descending order
                BitonicSort(collection, low + k, k, Direction.Descending);

                //Merge entire sequence in ascending order
                BitonicMerge(collection, low, count, direction);
            }
        }

        //This method recursively sorts a bitonic sequence in ascending order,  
        //if dir = 1, and in descending order otherwise (means dir=0).  
        //The sequence to be sorted starts at index position low,  
        //the parameter count is the number of elements to be sorted.
        private void BitonicMerge(IList<T> collection, int low, int count, Direction direction)
        {
            if (count > 1)
            {
                int k = count / 2;
                for (int i = low; i < low + k; i++)
                {
                    CompareAndSwap(collection, i, i + k, direction);
                }
                BitonicMerge(collection, low, k, direction);
                BitonicMerge(collection, low + k, k, direction);
            }
        }

        private static bool IsPowerOfTwo(int n)
        {
            return ((n & (-n)) == n) && (n > 0);
        }

        private static void CompareAndSwap(IList<T> collection, int i, int j, Direction direction)
        {
            Direction k = collection[i].CompareTo(collection[j]) > 0 ? Direction.Ascending : Direction.Descending;

            if (direction == k) //If the order the elements are currently in DOES NOT match the sort direction (array[i] > array[j])...
            {
                //...Swap the elements so they DO match the sort direction
                T temp = collection[i];
                collection[i] = collection[j];
                collection[j] = temp;
            }
        }
    }
}