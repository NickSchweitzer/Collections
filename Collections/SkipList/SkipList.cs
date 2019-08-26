using System;
using System.Collections;
using System.Collections.Generic;

namespace TheCodingMonkey.Collections.SkipList
{
	/// <summary>Implementation for a Skip List.</summary>
	/// <remarks>A SkipList is a sorted linked list where each element contains a list of references to a random
	/// number of other elements in the list skipping items throughout to provided faster traversal of the list.</remarks>
	public class SkipList<TKey, TValue> : IDictionary<TKey, TValue>, ICloneable
        where TKey : IComparable<TKey>
	{
        private const double cProb = 0.5;
        private Random m_Random;

        /// <summary>Default Constructor.</summary>
		public SkipList()
        : this( -1 )
		{ }

        /// <summary>SkipList Constructor.</summary>
        /// <param name="nRandomSeed">Random Number Seed for Height of SkipList Nodes.</param>
        public SkipList( int nRandomSeed )
        {
            Head = new Node<TKey, TValue>( 1 );
            Count   = 0;

            if ( nRandomSeed < 0 )
                m_Random = new Random();
            else
                m_Random = new Random( nRandomSeed );
        }

        /// <summary>Makes a deep copy of this SkipList.</summary>
        /// <returns>New instance of the SkipList.</returns>
        public object Clone()
        {
            SkipList<TKey, TValue> clone = new SkipList<TKey, TValue>();

            Node<TKey, TValue> current = Head;
            while ( current[0] != null )
            {
                TKey cloneKey;
                if ( current[0].Key is ICloneable )
                    cloneKey = (TKey)((ICloneable)current[0].Key).Clone();
                else
                    cloneKey = current[0].Key;

                TValue cloneValue;
                if ( current[0].Value is ICloneable )
                    cloneValue = (TValue)((ICloneable)current[0].Value).Clone();
                else
                    cloneValue = current[0].Value;

                clone.Add( cloneKey, cloneValue );
                current = current[0];
            }

            return clone;
        }

        /// <summary>Creates an Enumerator object which can be used to iterate through the SkipList.</summary>
        /// <returns>IEnumerator object for this SkipList.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator<TKey, TValue>( this );
        }

        /// <summary>Number of items in the SkipList.</summary>
        public int Count { get; private set; }

        /// <summary>Copies the Skip List elements to a one-dimensional Array instance at the specified index.</summary>
        /// <param name="array">The one dimensional Array to copy the elements to.</param>
        /// <param name="index">The zero-based index in array at which copying begins.</param>
        /// <exception cref="ArgumentNullException">Thrown if array is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if index is less than zero or if index
        /// is greater than the size of array.</exception>
        /// <exception cref="ArgumentException">Thrown if the number of items in the list is greater than the
        /// available space in array.</exception>
        /// <exception cref="InvalidCastException">The type of the source List cannot be cast automatically 
        /// to the type of the destination array.</exception>
        public void CopyTo( KeyValuePair<TKey, TValue>[] array, int index )
        {
            if ( array == null )
                throw new ArgumentNullException( "array", "array cannot be null" );

            if ( ( index < 0 ) || ( index >= array.Length ) )
                throw new ArgumentOutOfRangeException( "index", index, "index must be greater than zero and less than the size of array" );

            if ( ( array.Length - index ) < Count )
                throw new ArgumentException( "array is not large enough to store the list", "array" );

            Enumerator<TKey, TValue> enumerator = new Enumerator<TKey, TValue>( this );
            while ( enumerator.MoveNext() )
                array.SetValue( enumerator.Current, index++ );
        }

        /// <summary>Gets a value indicating whether the Skip List is read-only.</summary>
        public bool IsReadOnly => false;

        /// <summary>Indexer for the Skip List.</summary>
        /// <exception cref="ArgumentNullException">Thrown if key is null.</exception>
        /// <exception cref="InvalidCastException">Thrown if key is not of type IComparable.</exception>
        /// <exception cref="NotSupportedException">Thrown if attempting to get a value for a key that does not 
        /// exist in the list.</exception>
        public TValue this[TKey key]
        {
            get
            {
                if ( key == null )
                    throw new ArgumentNullException( "key", "key cannot be null" );

                Node<TKey, TValue> node = Search( key );
                if ( node != null )
                    return node.Value;
                else
                    throw new NotSupportedException( "Specified key does not exist in the list" );
            }
            set
            { 
                if ( key == null )
                    throw new ArgumentNullException( "key", "key cannot be null" );

                Node<TKey, TValue> node = Search( key );
                if ( node != null )
                    node.Value = value;
                else
                    Add( key, value );
            }
        }

        public bool TryGetValue( TKey key, out TValue value )
        {
            value = default( TValue );
            bool bFound = false;

            if ( key != null )
            {
                Node<TKey, TValue> node = Search( key );
                if ( node != null )
                {
                    value = node.Value;
                    bFound = true;
                }
            }
            return bFound;
        }

        /// <summary>Gets an ICollection containing the keys of the Skip List.</summary>
        public ICollection<TKey> Keys
        {
            get
            {
                List<TKey> lstKeys = new List<TKey>( Count );
                Traverse( new Predicate<TKey>( AddKey ), lstKeys );
                return lstKeys;
            }
        }

        /// <summary>Gets an ICollection containing the values of the Skip List.</summary>
        public ICollection<TValue> Values
        {
            get
            {
                List<TValue> lstValues = new List<TValue>( Count );
                Traverse( new Predicate<TValue>( AddValue ), lstValues );
                return lstValues;
            }
        }

        /// <summary>Removes all items from the list.</summary>
        public void Clear()
        {
            Head = new Node<TKey, TValue>( 1 );
            Count   = 0;
        }

        public bool Contains( KeyValuePair<TKey, TValue> pair )
        {
            return ContainsKey( pair.Key );
        }

        /// <summary>Determines whether the list contains the specified key.</summary>
        /// <param name="key">The value to test for.</param>
        /// <returns>True if the key is contained in the list, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if key is null.</exception>
        /// <exception cref="InvalidCastException">Thrown if key is not of type IComparable.</exception>
        public virtual bool ContainsKey( TKey key )
        {
            if ( key == null )
                throw new ArgumentNullException( "key", "key cannot be null" );

            return ( Search( key ) != null );
        }

        public void Add( KeyValuePair<TKey, TValue> pair )
        {
            Add( pair.Key, pair.Value );
        }

        /// <summary>Adds an item to the List</summary>
        /// <param name="key">Key used for comparison and lookup.</param>
        /// <param name="value">Value to add to the list.</param>
        /// <exception cref="ArgumentNullException">Thrown if key is null.</exception>
        /// <exception cref="ArgumentException">Thrown if key already exists in the list.</exception>
        /// <exception cref="InvalidCastException">Thrown if key is not of type IComparable.</exception>
        public virtual void Add( TKey key, TValue value )
        {
            if ( key == null )
                throw new ArgumentNullException( "key", "key cannot be null" );

            Node<TKey, TValue>[] updates = new Node<TKey, TValue>[Head.Height];
            Node<TKey, TValue> current = Head;
            int i = 0;

            // First, determine the nodes that need to be updated at each level
            for ( i = Head.Height - 1; i >= 0; i-- )
            {
                while ( ( current[i] != null ) && ( current[i].Key.CompareTo(key) < 0 ) )
                    current = current[i];

                updates[i] = current;
            }

            // Cannot enter a duplicate
            if ( ( current[0] != null ) && ( current[0].Key.CompareTo(key) == 0 ) )
                throw new ArgumentException( "Attempting to add duplicate item to the list.  The items has a value of " + key.ToString(), "key" );

            // Create a new node
            Node<TKey, TValue> node = new Node<TKey, TValue>( key, value, ChooseRandomHeight( Head.Height + 1 ) );
            Count++;

            // If the node's level is greater than the head's level, increase the head's level
            if ( node.Height > Head.Height )
            {
                Head.IncrementHeight();
                Head[Head.Height - 1] = node;
            }

            // Splice the new node into the list
            for ( i = 0; i < node.Height; i++ )
            {
                if ( i < updates.Length )
                {
                    node[i] = updates[i][i];
                    updates[i][i] = node;
                }
            }
        }

        public bool Remove( KeyValuePair<TKey, TValue> pair )
        {
            return Remove( pair.Key );
        }

        /// <summary>Removes an value from the List.</summary>
        /// <param name="key">Key to remove.</param>
        /// <exception cref="ArgumentNullException">Thrown if key is null.</exception>
        /// <exception cref="InvalidCastException">Thrown if key is not of type IComparable.</exception>
        /// <remarks>If key does not exist in the list, then the list remains unchanged.  No exception is thrown.</remarks>
        public virtual bool Remove( TKey key )
        {
            if ( key == null )
                throw new ArgumentNullException( "key", "key cannot be null" );

            Node<TKey, TValue>[] updates = new Node<TKey, TValue>[Head.Height];
            Node<TKey, TValue> current = Head;
            int i = 0;

            // First, determine the nodes that need to be updated at each level
            for ( i = Head.Height - 1; i >= 0; i-- )
            {
                while ( ( current[i] != null ) && ( current[i].Key.CompareTo(key) < 0 ) )
                    current = current[i];

                updates[i] = current;
            }

            current = current[0];
            if ( ( current != null ) && ( current.Key.CompareTo(key) == 0 ) )
            {
                Count--;

                // We found the data to delete... update the inner node lists
                for ( i = 0; i < Head.Height; i++ )
                {
                    if ( updates[i][i] != current )
                        break;
                    else
                        updates[i][i] = current[i];
                }

                // Finally, see if we need to trim the height of the list
                if ( Head[Head.Height - 1] == null )
                    Head.DecrementHeight();

                return true;
            }
            // The data to delete wasn't found... bail
            else
                return false;
        }

        /// <summary>Creates a Enumerator object which can be used to iterate through the SkipList.</summary>
        /// <returns>IDictionaryEnumerator object for this SkipList.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new Enumerator<TKey, TValue>( this );
        }

        /// <summary>First item in the SkipList.</summary>
        internal Node<TKey, TValue> Head { get; private set; }

        /// <summary>Height of the Head Node reference list.</summary>
        internal int Height
        {
            get { return Head.Height; }
        }

        /// <summary>Returns true if the list is empty.</summary>
        public bool Empty => Count == 0;

        /// <summary>Functions which determines the height of a SkipList Node's reference list.</summary>
        /// <param name="nMaxLevel">Maximum Height Allowed.</param>
        /// <returns>Random Height</returns>
        private int ChooseRandomHeight( int nMaxLevel )
        {
            int nLevel = 1;

            while ( ( m_Random.NextDouble() < cProb ) && ( nLevel < nMaxLevel ) )
                nLevel++;

            return nLevel;
        }

        private Node<TKey, TValue> Search( TKey key )
        {
            Node<TKey, TValue> current = Head;

            // First, determine the nodes that need to be updated at each level
            for ( int i = Head.Height - 1; i >= 0; i-- )
            {
                while ( current[i] != null )
                {
                    int nResult = current[i].Key.CompareTo(key);
                    if ( nResult == 0 )
                        return current[i];
                    else if ( nResult < 0 )
                        current = current[i];
                    else
                        break;	// exit while loop
                }
            }

            // if we reach here, we searched to the end of the list without finding the element
            return null;
        }

        private delegate T Predicate<T>( Node<TKey, TValue> node );

        private void Traverse<T>( Predicate<T> action, List<T> list )
        {
            Node<TKey, TValue> current = Head;  // Head node contains no data
            while ( current[0] != null )
            {
                current = current[0];
                list.Add( action( current ) );
            }
        }

        private TKey AddKey( Node<TKey, TValue> node )
        {
            return node.Key;
        }

        private TValue AddValue( Node<TKey, TValue> node )
        {
            return node.Value;
        }
    }
}