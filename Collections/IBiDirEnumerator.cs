using System;
using System.Collections.Generic;

namespace TheCodingMonkey.Collections
{
    /// <summary>Defines an enumerator that go go forward and backwards.</summary>
    public interface IBiDirEnumerator<T> : IEnumerator<T>
    {
        /// <summary>Moves to the previous element in the list</summary>
        /// <returns>Returns false if at the beginning of the list.</returns>
        bool MovePrevious();
    }
}