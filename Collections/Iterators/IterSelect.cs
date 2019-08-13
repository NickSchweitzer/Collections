// Written by Eric Gunnerson
// Downloaded from GotDotNet.com

using System;
using System.Collections;

namespace TheCodingMonkey.Collections.Iterators
{
    /// <summary>Predicate to select iterated elements.</summary>
    public delegate bool IterSelectDelegate( object o );
	
	/// <summary>Iterate a collection in the Select order.</summary>
	public class IterSelect: IterIsolate, IEnumerable
	{
		/// <summary>Create an instance of the IterSelect Class</summary>
		/// <param name="enumerable">A class that implements IEnumerable</param>
        /// <param name="selector">a predicate to pick out items</param>
        public IterSelect( IEnumerable enumerable, IterSelectDelegate selector )
        : base( enumerable )
		{
			m_pSelector = selector;
		}

        /// <summary></summary>
        public new IEnumerator GetEnumerator()
		{
			return new IterSelectEnumerator( m_pEnumerable.GetEnumerator(), m_pSelector );
		}

		private IterSelectDelegate m_pSelector;

        /// <summary>Helper class which does the actial enumeration.</summary>
        internal class IterSelectEnumerator : IterIsolateEnumerator, IEnumerator
        {
            internal IterSelectEnumerator( IEnumerator enumerator, IterSelectDelegate selector )
            : base( enumerator )
            {
                // reverse traversal so we can delete
                for ( int index = m_lstItems.Count - 1; index >= 0; index-- )
                {
                    if ( !selector( m_lstItems[index] ) )
                        m_lstItems.RemoveAt( index );
                }

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