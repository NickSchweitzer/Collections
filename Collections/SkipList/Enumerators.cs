using System;
using System.Collections;
using System.Collections.Generic;

namespace TheCodingMonkey.Collections.SkipList
{
	/// <summary>SkipList Enumerator.</summary>
	public class Enumerator<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable
        where TKey : IComparable<TKey>
	{
        private SkipList<TKey, TValue> m_SkipList;
        private Node<TKey, TValue>     m_nodeCurrent;

        /// <summary>Standard Constructor.</summary>
        /// <param name="list">List to Enumerate.</param>
		public Enumerator( SkipList<TKey, TValue> list )
		{
            m_SkipList    = list;
            m_nodeCurrent = m_SkipList.Head;
        }

        /// <summary>Cleans up any resources being used by this object.</summary>
        public void Dispose()
        {
            m_SkipList = null;
        }

        /// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
        public void Reset()
        {
            m_nodeCurrent = null;
        }

        /// <summary>Gets both the key and the value of the current dictionary entry.</summary>
        public KeyValuePair<TKey, TValue> Current
        {
            get { return new KeyValuePair<TKey, TValue>( m_nodeCurrent.Key, m_nodeCurrent.Value ); }
        }

        object IEnumerator.Current
        {
            get { return new KeyValuePair<TKey, TValue>( m_nodeCurrent.Key, m_nodeCurrent.Value ); }
        }

        /// <summary>Advances the enumerator to the next element of the collection.</summary>
        /// <returns>Returns true if the move was successful.</returns>
        public bool MoveNext()
        {
            m_nodeCurrent = m_nodeCurrent[0];
            return ( m_nodeCurrent != null );
        }

        /// <summary>Gets the key of the current dictionary entry.</summary>
        public object Key
        {
            get { return m_nodeCurrent.Key; }
        }

        /// <summary>Gets the value of the current dictionary entry.</summary>
        public object Value
        {
            get { return m_nodeCurrent.Value; }
        }
    }
}