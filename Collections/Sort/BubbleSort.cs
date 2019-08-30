using System;
using System.Collections.Generic;
using System.Text;

namespace TheCodingMonkey.Collections.Sort
{
    public class BubbleSort<T> : ISort<T>
        where T : IComparable
    {
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