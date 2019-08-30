using System;
using System.Collections.Generic;

namespace TheCodingMonkey.Collections.Sort
{
    /// <summary>Implementation of a Bubble sort (sinking sort) for IList</summary>
    /// <remarks>More information about QuickSort can be found at <a href="https://en.wikipedia.org/wiki/Heapsort">Wikipedia</a> or 
    /// <a href="https://exceptionnotfound.net/heap-sort-csharp-the-sorting-algorithm-family-reunion/">this blog post</a>.</remarks>
    /// <typeparam name="T">T must implement IComparable</typeparam>
    public class HeapSort<T> : ISort<T>
        where T : IComparable
    {
        /// <summary>Performs an in-place sort of the collection.</summary>
        /// <param name="collection">IList to sort</param>
        public void Sort(IList<T> collection)
        {
            var length = collection.Count;
            for (int i = length / 2 - 1; i >= 0; i--)
                Heapify(collection, length, i);

            for (int i = length - 1; i >= 0; i--)
            {
                T temp = collection[0];
                collection[0] = collection[i];
                collection[i] = temp;
                Heapify(collection, i, 0);
            }
        }

        //Rebuilds the heap
        static void Heapify(IList<T> collection, int length, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            if (left < length && collection[left].CompareTo(collection[largest]) > 0)
                largest = left;

            if (right < length && collection[right].CompareTo(collection[largest]) > 0)
                largest = right;

            if (largest != i)
            {
                T swap = collection[i];
                collection[i] = collection[largest];
                collection[largest] = swap;
                Heapify(collection, length, largest);
            }
        }
    }
}