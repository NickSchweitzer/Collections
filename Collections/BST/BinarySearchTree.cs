using System;
using System.Collections;
using System.Collections.Generic;

namespace TheCodingMonkey.Collections.BST
{
    /// <summary>Basic Unbalanced Binary Search Tree Implementation.</summary>
    public class BinarySearchTree<TKey, TValue> : ICloneable, IDictionary<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        IComparer m_pComparer = null;

        /// <summary>Default Constructor.</summary>
        public BinarySearchTree()
        { }

        /// <summary>Binary Search Tree Constructor</summary>
        /// <param name="comparer">Comparison object to use for comparing dictionary keys.</param>
        public BinarySearchTree( IComparer comparer )
        {
            m_pComparer = comparer;
        }

        /// <summary>Makes a deep copy of this Tree.</summary>
        /// <returns>New instance of the tree.</returns>
        public virtual object Clone()
        {
            var clone = CreateInstance();

            // Clone the root node... the clone will be called recursively through the tree
            if ( Root != null )
                clone.Root = (Node<TKey, TValue>)Root.Clone();

            // Sanity Check that the clone passes BST criteria
            if ( clone.Root != null )
                clone.Root.Check();

            return clone;
        }

        protected virtual BinarySearchTree<TKey, TValue> CreateInstance()
        {
            return new BinarySearchTree<TKey, TValue>
            {
                Count = Count
            };
        }

        /// <summary>Creates an Enumerator object which can be used to iterate through the Tree.</summary>
        /// <returns>IEnumerator object for this SkipList.</returns>
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return GetDefaultEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetDefaultEnumerator();
        }

        /// <summary>Returns the number of items in the tree.</summary>
        public int Count { get; protected set; } = 0;

        /// <summary>Gets a value indicating whether access to the ICollection is synchronized.</summary>
        public bool IsSynchronized => false;

        /// <summary>Gets an object that can be used to synchronize access to the ICollection.</summary>
        public object SyncRoot => this;

        /// <summary>Copies the Tree elements to a one-dimensional Array instance at the specified index.</summary>
        /// <param name="array">The one dimensional Array to copy the elements to.</param>
        /// <param name="index">The zero-based index in array at which copying begins.</param>
        /// <exception cref="ArgumentNullException">Thrown if array is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if index is less than zero or if index
        /// is greater than the size of array.</exception>
        /// <exception cref="ArgumentException">Thrown if the number of items in the list is greater than the
        /// available space in array.</exception>
        /// <exception cref="InvalidCastException">The type of the source Tree cannot be cast automatically 
        /// to the type of the destination array.</exception>
        public void CopyTo( KeyValuePair<TKey, TValue>[] array, int index )
        {
            if ( array == null )
                throw new ArgumentNullException( "array", "array cannot be null" );

            if ( ( index < 0 ) || ( index >= array.Length ) )
                throw new ArgumentOutOfRangeException( "index", index, "index must be greater than zero and less than the size of array" );

            if ( ( array.Length - index ) < Count )
                throw new ArgumentException( "array is not large enough to store the list", "array" );

            InOrderFlatEnumerator<TKey, TValue> enumerator = new InOrderFlatEnumerator<TKey, TValue>( this );
            enumerator.Iterator.CopyTo( array, index );
        }

        /// <summary>Gets a value indicating whether the Skip List has a fixed size.</summary>
        public bool IsFixedSize => false;

        /// <summary>Gets a value indicating whether the Skip List is read-only.</summary>
        public bool IsReadOnly => false;

        /// <summary>Indexer for the Binary Search Tree.</summary>
        /// <exception cref="ArgumentNullException">Thrown if key is null.</exception>
        /// <exception cref="InvalidCastException">Thrown if key is not of type IComparable.</exception>
        /// <exception cref="NotSupportedException">Thrown if attempting to get a value for a key that does not 
        /// exist in the tree.</exception>
        public TValue this[TKey key]
        {
            get
            { 
                if ( key == null )
                    throw new ArgumentNullException( "key" );

                Node<TKey, TValue> node = Search( Root, key );
                if ( node != null )
                    return node.Value;
                else
                    throw new NotSupportedException( "Specified key does not exist in the tree" );
            }
            set 
            {
                if ( key == null )
                    throw new ArgumentNullException( "key" );

                Node<TKey, TValue> node = Search( Root, key );
                if ( node != null )
                    node.Value = value;
                else
                    Add( key, value );
            }
        }

        public bool TryGetValue( TKey key, out TValue value )
        {
            value = default;
            bool found = false;

            if ( key != null )
            {
                Node<TKey, TValue> node = Search( Root, key );
                if ( node != null )
                {
                    value = node.Value;
                    found = true;
                }
            }
            return found;
        }

        /// <summary>Gets an ICollection containing the keys of the Tree.</summary>
        public ICollection<TKey> Keys
        {
            get
            {
                ICollection<TKey> lstKeys = new List<TKey>( Count );
                TraverseInOrder( new Predicate<TKey>( AddKey ), lstKeys );
                return lstKeys;
            }
        }

        /// <summary>Gets an ICollection containing the values of the Tree.</summary>
        public ICollection<TValue> Values
        {
            get
            {
                ICollection<TValue> lstValues = new List<TValue>( Count );
                TraverseInOrder( new Predicate<TValue>( AddValue ), lstValues );
                return lstValues;
            }
        }

        /// <summary>Removes all items from the Tree.</summary>
        public virtual void Clear()
        {
            Root = null;
            Count   = 0;
        }

        public bool Contains( KeyValuePair<TKey, TValue> pair )
        {
            return ContainsKey( pair.Key );
        }

        /// <summary>Determines if the value is contained somewhere in the tree.</summary>
        /// <param name="key">Key to find.</param>
        /// <returns>True if exists in the tree, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if key is null.</exception>
        /// <exception cref="InvalidCastException">Thrown if key is not of type IComparable.</exception>
        public virtual bool ContainsKey( TKey key )
        {
            if ( key == null )
                throw new ArgumentNullException( "key" );

            return Search( Root, key ) != null;
        }

        public void Add( KeyValuePair<TKey, TValue> pair )
        {
            Add( pair.Key, pair.Value );
        }

        /// <summary>Adds a node to the tree.</summary>
        /// <param name="key">Key used for comparison and lookup.</param>
        /// <param name="data">Data to be contained at the new node location.</param>
        /// <exception cref="ArgumentNullException">Thrown if key is null.</exception>
        /// <exception cref="ArgumentException">Thrown if key already exists in the tree.</exception>
        /// <exception cref="InvalidCastException">Thrown if key is not of type IComparable.</exception>
        public virtual void Add( TKey key, TValue data )
        {
            if ( key == null )
                throw new ArgumentNullException( "key" );

            // First create a new Node
            Node<TKey, TValue> node = new Node<TKey, TValue>( key, data );
            int result;

            // Insert the node into the tree, by tracing down the tree until we hit a null
            Node<TKey, TValue> current = Root;
            Node<TKey, TValue> parent  = null;

            while ( current != null )
            {
                result = Compare( current.Key, node.Key );
                // If they are equal, then attempting to insert a duplicate.  Throw an Exception
                if ( result == 0 )
                    throw new ArgumentException( "Attempting to add duplicate item to the tree.  The items has a value of " + data.ToString(), "data" );
                // Go down left subtree.
                else if ( result > 0 )
                {
                    parent  = current;
                    current = current.Left;
                }
                // Go down right subtree.
                else if ( result < 0 )
                {
                    parent  = current;
                    current = current.Right;
                }
            }

            Count++;    // We are adding this new node

            // If the tree was empty, this is the root
            if ( parent == null )
                Root = node;
            else
            {
                result = Compare( parent.Key, node.Key );
                // Make this the new left leaf
                if ( result > 0 )
                    parent.Left = node;
                // Make this the new right leaf
                else if ( result < 0 )
                    parent.Right = node;
            }

            // Do a Debug Sanity Check on the tree
            if ( Root != null )
                Root.Check();
        }

        public bool Remove( KeyValuePair<TKey, TValue> pair )
        {
            return Remove( pair.Key );
        }

        /// <summary>Removes the node containing this data from the tree.</summary>
        /// <param name="key">Value to remove from the tree.</param>
        /// <exception cref="ArgumentNullException">Thrown if key is null.</exception>
        /// <exception cref="InvalidCastException">Thrown if key is not of type IComparable.</exception>
        /// <remarks>If data does not exist in the tree, then the tree remains unchanged.  No exception is thrown.</remarks>
        public virtual bool Remove( TKey key )
        {
            if ( key == null )
                throw new ArgumentNullException( "key" );

            Node<TKey, TValue> current = Root;
            Node<TKey, TValue> parent = null;

            // Find the item that we need to delete
            int result = -1;
            while ( ( result != 0 ) && ( current != null ) )
            {
                result = Compare( current.Key, key );
                
                // Data must be in left subtree
                if ( result > 0 )
                {
                    parent  = current;
                    current = current.Left;
                }
                // Data must be in right subtree
                else if ( result < 0 )
                {
                    parent  = current;
                    current = current.Right;
                }
            }

            // Key is not in the tree... bail
            if ( current == null )
                return false;

            Count--;    // Found an item... and we're deleting it
                
            // CASE 1: Current node has no right child. Current's left child becomes the node pointed to by the parent
            if ( current.Right == null )
            {
                if ( parent == null )
                    Root = current.Left;
                else
                {
                    result = Compare( parent.Key, current.Key );
                    // Parent's left subtree is now current's left subtree
                    if ( result > 0 )
                        parent.Left = current.Left;
                    // Parent's right subtree is now current's left subtree
                    else if ( result < 0 )
                        parent.Right = current.Left;
                }
            }
            // CASE 2: Current's right child has no left child. Current's right child replaces current in the tree
            else if ( current.Right.Left == null )
            {
                if ( parent == null )
                    Root = current.Right;
                else
                {
                    result = Compare( parent.Key, current.Key );
                    // Parent's left subtree is now current's right subtree
                    if ( result > 0 )
                        parent.Left = current.Right;
                    // Parent's right subtree is now current's right subtree
                    else if ( result < 0 )
                        parent.Right = current.Right;
                }
            }
            // CASE 3: Current's right child has a left child. Replace current with current's right child's left-most node.
            else
            {
                // Find the right node's left-most child
                Node<TKey, TValue> leftmost = current.Right.Left;
                Node<TKey, TValue> lmParent = current.Right;
                while ( leftmost.Left != null )
                {
                    lmParent = leftmost;
                    leftmost = leftmost.Left;
                }

                // Parent's left subtree becomes the leftmost's right subtree
                lmParent.Left = leftmost.Right;
                    
                // Assign leftmost's left and right to current's left and right
                leftmost.Left  = current.Left;
                leftmost.Right = current.Right;

                if ( parent == null )
                    Root = leftmost;
                else
                {
                    result = Compare( parent.Key, current.Key );
                    // Parent's left subtree is now current's right subtree
                    if ( result > 0 )
                        parent.Left = leftmost;
                    // Parent's right subtree is now current's right subtree
                    else if ( result < 0 )
                        parent.Right = leftmost;
                }
            }

            // Do a Debug Sanity Check on the tree
            if ( Root != null )
                Root.Check();

            return true;
        }

        /// <summary>Creates a Enumerator object which can be used to iterate through the Tree.</summary>
        /// <returns>IDictionaryEnumerator object for this SkipList.</returns>
        IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return GetDefaultEnumerator();
        }

        /// <summary>Returns true if the tree is empty.</summary>
        public bool Empty => Count == 0;

        /// <summary>Root node of the tree.</summary>
        internal Node<TKey, TValue> Root;

        /// <summary>Compares two objects using the IComparable interface if possible, then using one of the 
        /// alternate comparison methods if needed.</summary>
        protected int Compare( object left, object right )
        {
            IComparable comp = left as IComparable;
            if ( comp != null )
                return comp.CompareTo( right );
            else if ( m_pComparer != null )
                return m_pComparer.Compare( left, right );
            else
                throw new ArgumentException( "Object does not implement IComparable, and no IComparer object was given" );
        }

        /// <summary>Recursive function used to find a given value in a subtree where current is the root node.</summary>
        protected Node<TKey, TValue> Search( Node<TKey, TValue> current, object key )
        {
            // Node not found, return null.
            if ( current == null )
                return null;
            else
            {
                // Figure out where the data might be
                int result = Compare( current.Key, key );
                
                // Data equal.  This is the node
                if ( result == 0 )
                    return current;
                // current.Value > data.  Must be in left subtree
                else if ( result > 0)
                    return Search( current.Left, key );
                // current.Value < data.  Must be in right subtree
                else
                    return Search( current.Right, key );
            }
        }

        private delegate T Predicate<T>( KeyValuePair<TKey, TValue> entry );

        private void TraverseInOrder<T>( Predicate<T> action, ICollection<T> list )
        {
            InOrderFlatEnumerator<TKey, TValue> enumerator = new InOrderFlatEnumerator<TKey, TValue>( this );
            ICollection<KeyValuePair<TKey, TValue>> lstIterator  = enumerator.Iterator;

            foreach ( KeyValuePair<TKey, TValue> entry in lstIterator )
                list.Add( action( entry ) );
        }
        
        private TKey AddKey( KeyValuePair<TKey, TValue> entry )
        {
            return entry.Key;
        }

        private TValue AddValue( KeyValuePair<TKey, TValue> entry )
        {
            return entry.Value;
        }

        private IEnumerator<KeyValuePair<TKey, TValue>> GetDefaultEnumerator()
        {
            return new InOrderFlatEnumerator<TKey, TValue>( this );
        }
    }
}