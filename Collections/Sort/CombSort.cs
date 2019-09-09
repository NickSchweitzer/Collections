using System;
using System.Collections.Generic;

namespace TheCodingMonkey.Collections.Sort
{
    /// <summary>Implementation of a Comb Sort for IList</summary>
    /// <remarks>More information about Comb Sort can be found at <a href="https://en.wikipedia.org/wiki/Comb_sort">Wikipedia</a> or 
    /// <a href="https://exceptionnotfound.net/comb-sort-csharp-the-sorting-algorithm-family-reunion/">this blog post</a>.</remarks>
    /// <typeparam name="T">T must implement IComparable</typeparam>
    public class CombSort<T> : IInPlaceSort<T>
        where T : IComparable
    {
        /// <summary>Performs an in-place sort of the collection.</summary>
        /// <param name="collection">IList to sort</param>
        public void Sort(IList<T> collection)
        {
            int length = collection.Count;
            int gap = length;

            //We initialize this as true to enter the while loop.
            bool swapped = true;

            while (gap != 1 || swapped == true)
            {
                gap = GetNextGap(gap);

                //Set swapped as false.  Will go to true when two values are swapped.
                swapped = false;

                //Compare all elements with current gap 
                for (int i = 0; i < length - gap; i++)
                {
                    if (collection[i].CompareTo(collection[i + gap]) > 0)
                    {
                        //Swap
                        T temp = collection[i];
                        collection[i] = collection[i + gap];
                        collection[i + gap] = temp;
                        swapped = true;
                    }
                }
            }
        }

        private static int GetNextGap(int gap)
        {
            //The "shrink factor", empirically shown to be 1.3
            gap = (gap * 10) / 13;
            if (gap < 1)
            {
                return 1;
            }
            return gap;
        }
    }
}