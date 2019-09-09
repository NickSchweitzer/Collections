using System;
using System.Collections.Generic;

namespace TheCodingMonkey.Collections.Sort
{
    /// <summary>Implementation of an Insertion sort (sinking sort) for IList</summary>
    /// <remarks>More information about Insertion sort can be found at <a href="https://en.wikipedia.org/wiki/Insertion_sort">Wikipedia</a> or 
    /// <a href="https://exceptionnotfound.net/insertion-sort-csharp-the-sorting-algorithm-family-reunion/">this blog post</a>.</remarks>
    /// <typeparam name="T">T must implement IComparable</typeparam>
    public class InsertionSort<T> : IInPlaceSort<T>
        where T : IComparable
    {
        /// <summary>Performs an in-place sort of the collection.</summary>
        /// <param name="collection">IList to sort</param>
        public void Sort(IList<T> collection)
        {
            int length = collection.Count;
            //1. For each value in the array...
            for (int i = 1; i < length; ++i)
            {
                //2. Store the current value in a variable.
                T currentValue = collection[i];
                int pointer = i - 1;

                //3. While we are pointing to a valid value...
                //4. If the current value is less than the value we are pointing at...
                while (pointer >= 0 && collection[pointer].CompareTo(currentValue) > 0)
                {
                    //5. Then move the pointed-at value up one space, and store the
                    //   current value at the pointed-at position.
                    collection[pointer + 1] = collection[pointer];
                    pointer = pointer - 1;
                }
                collection[pointer + 1] = currentValue;
            }
        }
    }
}