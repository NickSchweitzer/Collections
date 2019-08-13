using System;
using System.Collections;

namespace TheCodingMonkey.Collections
{
    /// <summary>Defines an enumerator that go go forward and backwards.</summary>
    public interface IBiDirEnumerator : IEnumerator
    {
        /// <summary>Moves to the previous element in the list</summary>
        /// <returns>Returns false if at the beginning of the list.</returns>
        bool MovePrev();
    }
}