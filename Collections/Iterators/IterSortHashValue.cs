// Written by Eric Gunnerson
// Downloaded from GotDotNet.com

using System;
using System.Collections;

namespace TheCodingMonkey.Collections.Iterators
{
	/// <summary>Iterate the keys in a hashtable, ordering them by the values corresponding to those keys. 
	/// Either uses the defined ordering on the values or a passed-in IComparer implementation.</summary>
	public class IterSortHashValue : IEnumerable
	{
        /// <summary>Create an instance of the IterSortHashValue class</summary>
        /// <param name="hashtable">The hashtable to use</param>
        /// <remarks>The items contained in hashtable must implement IComparable</remarks>
        public IterSortHashValue( Hashtable hashtable )
        {
            m_hashTable = hashtable;
        }

        /// <summary>Create an instance of the IterSortHashValue class, using a specific comparer.</summary>
        /// <param name="hashtable">The hashtable to use</param>
        /// <param name="comparer">The comparer to use</param>
        public IterSortHashValue( Hashtable hashtable, IComparer comparer )
        {
            m_hashTable = hashtable;
            m_pComparer = comparer;
        }

        /// <summary></summary>
        public IEnumerator GetEnumerator()
        {
            return new IterSortHashValueEnumerator( m_hashTable, m_pComparer );
        }

        Hashtable m_hashTable;
        IComparer m_pComparer;

        /// <summary>Helper class which does actual interation.</summary>
		internal class IterSortHashValueEnumerator : IEnumerator
		{
			private ArrayList m_lstItems = new ArrayList();
			private int       m_nCurrentItem;

			internal IterSortHashValueEnumerator( Hashtable hashtable, IComparer comparer )
			{
			    // create a new SortHashValueItem for each key. 
				foreach ( object key in hashtable.Keys )
				{
					SortHashValueItem item = new SortHashValueItem( hashtable, key, comparer );
					m_lstItems.Add( item );
				}

				m_nCurrentItem = -1;
				m_lstItems.Sort();
			}

            #region IEnumerable Methods

			public void Reset()
			{
				m_nCurrentItem = -1;
			}

			public bool MoveNext()
			{
                return ( ++m_nCurrentItem != m_lstItems.Count );
			}

			public object Current
			{
				get
				{
					SortHashValueItem current = (SortHashValueItem)m_lstItems[m_nCurrentItem];
					return current.Key;
				}
			}

            #endregion
		}

		/// <summary>Item to hold a key and the related hash table.</summary>
 		internal class SortHashValueItem : IComparable
		{
			Hashtable m_hashTable;
            IComparer m_pComparer;
            object    m_pKey;

			public SortHashValueItem( Hashtable hashtable, object key, IComparer comparer )
			{
				m_hashTable = hashtable;
                m_pComparer = comparer;
                m_pKey      = key;
			}

			public object Key
			{
				get { return m_pKey; }
			}

            #region IComparable Methods

			public int CompareTo( object object2 )
			{
				SortHashValueItem item2 = (SortHashValueItem)object2;

			    // get the two values for these keys...
				object value1 = m_hashTable[m_pKey];
				object value2 = item2.m_hashTable[item2.m_pKey];

				if ( m_pComparer != null )
					return m_pComparer.Compare( value1 ,value2 );
				else
				{
				    // compare one to another. They must implement IComparable...
					IComparable comparable = (IComparable)value1;
					return comparable.CompareTo( value2 );
				}
			}

            #endregion
		}
	}
}
