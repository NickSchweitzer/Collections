using System;
using System.Collections.Generic;

namespace TheCodingMonkey.Collections.SkipList
{
	/// <summary>SkipList Node.</summary>
	public class Node<TKey, TValue>
        where TKey : IComparable<TKey>
	{
        private NodeList<TKey, TValue> m_lstNodes;

        /// <summary>SkipNode Constructor</summary>
        /// <param name="nHeight">Number of items this node references.</param>
        internal Node( int nHeight ) 
        : this( default(TKey), default(TValue), nHeight ) 
        {}

        /// <summary>SkipNode Constructor</summary>
        /// <param name="key">Key used for comparison and lookup.</param>
        /// <param name="value">Value stored at this node.</param>
        /// <param name="nHeight">Number of items this node references.</param>
        internal Node( TKey key, TValue value, int nHeight )
        {
            Key     = key;
            Value   = value;
            m_lstNodes = new NodeList<TKey, TValue>( nHeight );
        }

        /// <summary>Increments the Height</summary>
        public void IncrementHeight()
        {
            m_lstNodes.IncrementHeight();
        }

        /// <summary>Decrements the Height</summary>
        public void DecrementHeight()
        {
            m_lstNodes.DecrementHeight();
        }

        /// <summary>Number of other nodes referenced by this Node.</summary>
        public int Height
        {
            get { return m_lstNodes.Capacity; }
        }

        /// <summary>Key used for comparison and lookup.</summary>
        public TKey Key { get; set; }

        /// <summary>Value compared at this node.</summary>
        public TValue Value { get; set; }

        /// <summary>List of references to other nodes stored by this node.</summary>
        public Node<TKey, TValue> this[int index]
        {
            get { return m_lstNodes[index];  }
            set { m_lstNodes[index] = value; }
        }
	}

    /// <summary>Tracks references from a SkipList node to the rest of the associated nodes.</summary>
    internal class NodeList<TKey, TValue> : List<Node<TKey, TValue>>
        where TKey : IComparable<TKey>
    {
        /// <summary>Standard Constructor</summary>
        /// <param name="nHeight">Height of the Skip List node.</param>
        public NodeList( int nHeight )
        {
            // Set the capacity based on the height
            Capacity = nHeight;

            // Create dummy values up to the Capacity
            for ( int i = 0; i < nHeight; i++ )
                Add( null );
        }

        ///// <summary>Adds a reference to another node to the collection.</summary>
        ///// <param name="node">Node to add to the collection.</param>
        //public void Add( Node<TKey, TValue> node )
        //{
        //    base.InnerList.Add( node );
        //}

        ///// <summary>Array Access function for the reference collection.</summary>
        //public Node<TKey, TValue> this[int index]
        //{
        //    get { return (Node)base.InnerList[index]; }
        //    set { base.InnerList[index] = value;      }
        //}

        /// <summary>Increments the total height (capacity) of the reference collection.</summary>
        public void IncrementHeight()
        {
            Capacity++;
            Add( null );            // Add a dummy entry
        }

        /// <summary>Decrements the total height (capacity) of the reference collection.  If an item exists
        /// at the end of the list, then it is removed.</summary>
        public void DecrementHeight()
        {
            // Delete the last entry
            RemoveAt( Count - 1 );
            Capacity--;
        }
    }
}