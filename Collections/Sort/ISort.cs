using System;
using System.Collections.Generic;

namespace TheCodingMonkey.Collections.Sort
{
    public interface ISort<T>
        where T : IComparable
    {
        void Sort(IList<T> collection);
    }
}