// Written by Eric Gunnerson
// Downloaded from GotDotNet.com

using System;
using System.Collections;

namespace TheCodingMonkey.Collections.Iterators
{
	/// <summary>Iterate a collection in the Type order</summary>
	public class IterType : IterIsolate, IEnumerable
	{
		/// <summary>Create an instance of the IterType Class</summary>
		/// <param name="enumerable">A class that implements IEnumerable</param>
		/// <param name="t">type of elements to pick out</param>
		public IterType( IEnumerable enumerable, Type t )
        : base( enumerable )
		{
			m_Type = t;
		}

        /// <summary></summary>
        public new IEnumerator GetEnumerator()
		{
			return new IterTypeEnumerator( m_pEnumerable.GetEnumerator(), m_Type );
		}

		Type m_Type;

        /// <summary>Helper class which does the actual work of iteration.</summary>
        internal class IterTypeEnumerator: IterIsolateEnumerator, IEnumerator
        {
            internal IterTypeEnumerator( IEnumerator enumerator, Type t )
            : base( enumerator )
            {
                // reverse traversal so we can delete
                for ( int index = m_lstItems.Count - 1; index >= 0; index-- )
                {
                    if ( m_lstItems[index].GetType() != t )
                        m_lstItems.RemoveAt( index );
                }

                m_nCurrentItem = m_lstItems.Count;
            }

            public new void Reset()
            {
                m_nCurrentItem = m_lstItems.Count;
            }

            public new bool MoveNext()
            {
                return ( --m_nCurrentItem >= 0 );
            }
        }
    }
}