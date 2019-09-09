using System;
using System.Collections.Generic;

namespace TheCodingMonkey.Collections.Sort
{
    /// <summary>Implementation of an Odd Even sort (brick sort) for IList</summary>
    /// <remarks>More information about Odd Even Sort can be found at <a href="https://en.wikipedia.org/wiki/Odd%E2%80%93even_sort">Wikipedia</a> or 
    /// <a href="https://exceptionnotfound.net/odd-even-sort-csharp-the-sorting-algorithm-family-reunion/">this blog post</a>.</remarks>
    /// <typeparam name="T">T must implement IComparable</typeparam>
    public class OddEvenSort<T> : IInPlaceSort<T>
        where T : IComparable
    {
        /// <summary>Performs an in-place sort of the collection.</summary>
        /// <param name="collection">IList to sort</param>
        public void Sort(IList<T> collection)
        {
            Sort(collection, collection.Count);
        }

        private void Sort(IList<T> collection, int length)
        {
            bool isSorted = false;

            while (!isSorted)
            {
                isSorted = true;

                //Swap i and i+1 if they are out of order, for i == odd numbers
                for (int i = 1; i <= length - 2; i = i + 2)
                {
                    if (collection[i].CompareTo(collection[i + 1]) > 0)
                    {
                        T temp = collection[i];
                        collection[i] = collection[i + 1];
                        collection[i + 1] = temp;
                        isSorted = false;
                    }
                }

                //Swap i and i+1 if they are out of order, for i == even numbers
                for (int i = 0; i <= length - 2; i = i + 2)
                {
                    if (collection[i].CompareTo(collection[i + 1]) > 0)
                    {
                        T temp = collection[i];
                        collection[i] = collection[i + 1];
                        collection[i + 1] = temp;
                        isSorted = false;
                    }
                }
            }
        }
    }
}