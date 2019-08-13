// Written by Eric Gunnerson
// Downloaded from GotDotNet.com

using System;
using System.Collections;

namespace TheCodingMonkey.Collections.Iterators
{
	/// <summary>Iterate a collection in sorted order, either using the built-in ordering for the object or 
	/// using a class implementing IComparer.</summary>
	public class IterSort : IterIsolate, IEnumerable
	{
		/// <summary>Create an instance of the IterIsolate Class</summary>
		/// <param name="enumerable">A class that implements IEnumerable</param>
		public IterSort( IEnumerable enumerable )
        : base( enumerable )
		{ }

		/// <summary>Create an instance of the IterIsolate Class, using a different sort order.</summary>
		/// <param name="enumerable">A class that implements IEnumerable</param>
		/// <param name="comparer">A class that implements IComparer</param>
		public IterSort( IEnumerable enumerable, IComparer comparer )
        : base( enumerable )
		{
			m_pComparer = comparer;
		}

        /// <summary></summary>
        public new IEnumerator GetEnumerator()
		{
			return new IterSortEnumerator( m_pEnumerable.GetEnumerator(), m_pComparer );
		}

		IComparer m_pComparer;

        /// <summary>Helper class which does the actual interation.</summary>
        internal class IterSortEnumerator : IterIsolateEnumerator, IEnumerator
        {
            internal IterSortEnumerator( IEnumerator enumerator, IComparer comparer )
            : base( enumerator )
            {
                if ( comparer != null )
                    m_lstItems.Sort(comparer);
                else
                    m_lstItems.Sort();
            }
        }
	}
}
