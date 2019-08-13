// Written by Eric Gunnerson
// Downloaded from GotDotNet.com

using System;
using System.Collections;

namespace TheCodingMonkey.Collections.Iterators
{
	/// <summary>Enumerate a collection, skipping some items at the beginning or end while enumerating</summary>
	/// <example>
    /// // Enumerates collection, skipping 1 item at the start of the list and 2 items and the end of the list. 
	/// foreach ( string s in new SubList( collection, 1, 2 ) )
	/// </example>
 	public class IterSubList : IterIsolate, IEnumerable
	{
		/// <summary>Return an instance of the IterSubList class</summary>
		/// <param name="enumerable">The collection to use</param>
		/// <param name="skipAtStart">The number of items to skip at the start of the list</param>
		/// <param name="skipAtEnd">The number of items to skip at the end of the list</param>
		public IterSubList( IEnumerable enumerable, int skipAtStart, int skipAtEnd )
        : base( enumerable )
		{
			m_nSkipAtStart = skipAtStart;
			m_nSkipAtEnd   = skipAtEnd;
		}

        /// <summary></summary>
        public new IEnumerator GetEnumerator()
        {
			return new IterSubListEnumerator( m_pEnumerable.GetEnumerator(), m_nSkipAtStart, m_nSkipAtEnd );
		}

		private int m_nSkipAtStart;
		private int m_nSkipAtEnd;

        internal class IterSubListEnumerator : IterIsolateEnumerator, IEnumerator
        {
            internal IterSubListEnumerator( IEnumerator enumerator, int skipAtStart, int skipAtEnd )
            : base( enumerator )
            {
                // get rid of items at the start and end
                m_lstItems.RemoveRange( 0, skipAtStart );
                m_lstItems.RemoveRange( m_lstItems.Count - skipAtEnd, skipAtEnd );
            }
        }	
    }
}
