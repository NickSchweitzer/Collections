// Written by Eric Gunnerson
// Downloaded from GotDotNet.com

using System;
using System.Collections;

namespace TheCodingMonkey.Collections.Iterators
{
	/// <summary>Iterate a collection in the reverse order</summary>
	public class IterReverse : IterIsolate, IEnumerable
	{
		/// <summary>Create an instance of the IterReverse Class</summary>
		/// <param name="enumerable">A class that implements IEnumerable</param>
		public IterReverse( IEnumerable enumerable )
        : base( enumerable )
		{ }

        /// <summary>Returns a reverse iterator for the given enumerable object.</summary>
        public new IEnumerator GetEnumerator()
		{
			return new IterReverseEnumerator( m_pEnumerable.GetEnumerator() );
		}

        /// <summary>Internal helper class to do the actual iteration.</summary>
        internal class IterReverseEnumerator : IterIsolateEnumerator, IEnumerator
        {
            internal IterReverseEnumerator( IEnumerator enumerator )
            : base( enumerator )
            {
                m_nCurrentItem = m_lstItems.Count;
            }

            #region IEnumerator Methods

            public new void Reset()
            {
                m_nCurrentItem = m_lstItems.Count;
            }

            public new bool MoveNext()
            {
                return ( --m_nCurrentItem >= 0 );
            }

            #endregion
        }
	}
}