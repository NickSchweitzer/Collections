using System;
using System.Collections.Generic;
using System.Linq;

namespace TheCodingMonkey.Collections.Sort
{
    /// <summary>Implementation of an Out of Place Merge Sort (partition-exchange sort) for IList</summary>
    /// <remarks>More information about Merge Sorting can be found at <a href="https://en.wikipedia.org/wiki/Merge_sort">Wikipedia</a> or 
    /// <a href="https://exceptionnotfound.net/merge-sort-csharp-the-sorting-algorithm-family-reunion/">this blog post</a>.</remarks>
    /// <typeparam name="T">T must implement IComparable</typeparam>
    public class MergeSort<T> : IOutOfPlaceSort<T>
        where T : IComparable
    {
        /// <summary>Performs an in-place sort of the collection.</summary>
        /// <remarks>If <paramref name="unsorted">unsorted</paramref> is an empty collection, then it is returned unmodified.</remarks>
        /// <param name="unsorted">IList to sort</param>
        /// <returns>A sorted IList containing the same elements as the original unsorted collection.</returns>
        public IList<T> Sort(IList<T> unsorted)
        {
            if (unsorted.Count <= 1)
                return unsorted;

            IList<T> left = new List<T>();
            IList<T> right = new List<T>();

            int median = unsorted.Count / 2;
            for (int i = 0; i < median; i++)  //Dividing the unsorted list
                left.Add(unsorted[i]);

            for (int i = median; i < unsorted.Count; i++)
                right.Add(unsorted[i]);


            left = Sort(left);
            right = Sort(right);
            return Merge(left, right);
        }

        /// <summary>Method takes two sorted "sublists" (left and right) of original list and merges them into a new colletion </summary>
        private List<T> Merge(IList<T> left, IList<T> right)
        {
            List<T> result = new List<T>(); //The new collection

            while (left.Any() || right.Any())
            {
                if (left.Any() && right.Any())
                {
                    if (left.First().CompareTo(right.First()) <= 0)  //Comparing the first element of each sublist to see which is smaller
                    {
                        result.Add(left.First());
                        left.Remove(left.First());
                    }
                    else
                    {
                        result.Add(right.First());
                        right.Remove(right.First());
                    }
                }
                else if (left.Any())
                {
                    result.Add(left.First());
                    left.Remove(left.First());
                }
                else if (right.Any())
                {
                    result.Add(right.First());
                    right.Remove(right.First());
                }
            }
            return result;
        }
    }
}