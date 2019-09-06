using System;
using System.Collections.Generic;

namespace TheCodingMonkey.Collections.Sort
{
    /// <summary>Implementation of a Shell Sort for IList</summary>
    /// <remarks>More information about Shell Sort can be found at <a href="https://en.wikipedia.org/wiki/Shellsort">Wikipedia</a> or 
    /// <a href="https://exceptionnotfound.net/shell-sort-csharp-the-sorting-algorithm-family-reunion/">this blog post</a>.</remarks>
    /// <typeparam name="T">T must implement IComparable</typeparam>

    public class ShellSort<T> : ISort<T>
        where T : IComparable
    {
        /// <summary>Performs an in-place sort of the collection.</summary>
        /// <param name="collection">IList to sort</param>
        public void Sort(IList<T> collection)
        {
            int length = collection.Count;

            for (int h = length / 2; h > 0; h /= 2)
            {
                for (int i = h; i < length; i += 1)
                {
                    T temp = collection[i];

                    int j;
                    for (j = i; j >= h && collection[j - h].CompareTo(temp) > 0; j -= h)
                    {
                        collection[j] = collection[j - h];
                    }

                    collection[j] = temp;
                }
            }
        }
    }
}