using System;
using System.Collections;
using System.Collections.Generic;

namespace TheCodingMonkey.Collections.BST
{
    /// <summary>Abstract base class for all BinarySearchTree enumerators. Implements an Enumerator for a Binary Search Tree
    /// by traversing the entire tree at Construction and flattening it according to the rules of the enumerator.</summary>
    public abstract class FlatEnumerator<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>>
        where TKey : IComparable<TKey>
    {
        private List<KeyValuePair<TKey, TValue>> m_lstIterator = null;
        private int m_nIndex = -1;

        /// <summary>Standard Constructor.</summary>
        /// <param name="tree">BinarySearchTree to create an Enumerator for.</param>
        public FlatEnumerator( BinarySearchTree<TKey, TValue> tree )
        {
            // Shallow copy the contents of the tree into an ArrayList and then return this ArrayList's enumerator
            m_lstIterator = new List<KeyValuePair<TKey, TValue>>( tree.Count );
            Traverse( tree.Root, m_lstIterator );
        }

        /// <summary>Gets the key of the current dictionary entry.</summary>
        public TKey Key
        {
            get { return m_lstIterator[m_nIndex].Key; }
        }

        /// <summary>Gets the value of the current dictionary entry.</summary>
        public TValue Value
        {
            get { return m_lstIterator[m_nIndex].Value; }
        }

        /// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
        public void Reset()
        {
            m_nIndex = -1;
        }

        /// <summary>Gets the current element in the collection.</summary>
        public KeyValuePair<TKey, TValue> Current
        {
            get { return m_lstIterator[m_nIndex]; }
        }

        object IEnumerator.Current
        {
            get { return m_lstIterator[m_nIndex]; }
        }

        /// <summary>Advances the enumerator to the next element of the collection.</summary>
        /// <returns>Returns true if the move was successful.</returns>
        public bool MoveNext()
        {
            return (++m_nIndex < m_lstIterator.Count );
        }

        public void Dispose()
        { }

        /// <summary>Defines a traversal function for shallow copying the elements from the BinarySearchTree
        /// into the passed in ArrayList.</summary>
        /// <param name="current">Node to Traverse</param>
        /// <param name="contents">ArrayList to contain the re-ordered elements.</param>
        protected abstract void Traverse( Node<TKey, TValue> current, ICollection<KeyValuePair<TKey, TValue>> contents );

        internal ICollection<KeyValuePair<TKey, TValue>> Iterator
        {
            get { return m_lstIterator; }
        }
    }
}