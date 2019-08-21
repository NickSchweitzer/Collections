using System;
using System.Diagnostics;

namespace TheCodingMonkey.Collections.BST
{
    /// <summary>Binary Search Tree Node.</summary>
    public class Node<TKey, TValue> : ICloneable
        where TKey : IComparable<TKey>
    {
        /// <summary>Array index for left subtree.</summary>
        protected const uint LEFT  = 0;
        /// <summary>Array index for right subtree.</summary>
        protected const uint RIGHT = 1;

        /// <summary>Array of node subtrees.</summary>
        protected readonly Node<TKey, TValue>[] Nodes;

        /// <summary>Default Constructor</summary>
        internal Node()
        : this( default, default )
        { }

        /// <summary>Constructs a node with data, but no children.</summary>
        /// <param name="key">Key used for comparison and lookup.</param>
        /// <param name="data">Data at this node.</param>
        internal Node( TKey key, TValue data ) 
        : this( key, data, null, null ) 
        { }

        /// <summary>Constructs a node with data, and two children.</summary>
        /// <param name="key">Key used for comparison and lookup.</param>
        /// <param name="value">Value at this node.</param>
        /// <param name="left">Left child node.</param>
        /// <param name="right">Right child node.</param>
        internal Node( TKey key, TValue value, Node<TKey, TValue> left, Node<TKey, TValue> right )
        {
            Key = key;
            Value = value;
            Nodes = new Node<TKey, TValue>[] { left, right };
        }

        /// <summary>Performs a deep copy on this node, and any children if they exist.</summary>
        /// <returns>Copy of this node.</returns>
        public object Clone()
        {
            Node<TKey, TValue> clone = CloneInstance();

            // If the key is cloneable, then do a deep copy.  Otherwise do a shallow one
            if ( Key is ICloneable )
                clone.Key = (TKey)((ICloneable)Key).Clone();
            else
                clone.Key = Key;

            // If the data is cloneable, then do a deep copy.  Otherwise do a shallow one
            if ( Value is ICloneable )
                clone.Value = (TValue)((ICloneable)Value).Clone();
            else
                clone.Value = Value;

            // Clone the children if they exist
            if ( Left != null )
                clone.Left = (Node<TKey, TValue>)Left.Clone();
        	
            if ( Right != null )
                clone.Right = (Node<TKey, TValue>)Right.Clone();

            return clone;
        }

        /// <summary>Helper function used during Clone to create the correct instance of the specific Node implementation</summary>
        /// <returns>A new reference to a Node derived object that can be used in a clone operation.</returns>
        protected virtual Node<TKey, TValue> CloneInstance()
        {
            return new Node<TKey, TValue>();
        }

        /// <summary>Key used for comparison and lookup.</summary>
        public TKey Key { get;set; }

        /// <summary>Data at this node location.</summary>
        public TValue Value { get; set; }

        /// <summary>Left Child Node.</summary>
        internal Node<TKey, TValue> Left
        {
            get { return Nodes[LEFT];  }
            set { Nodes[LEFT] = value; }
        }

        /// <summary>Right Child Node.</summary>
        internal Node<TKey, TValue> Right
        {
            get { return Nodes[RIGHT];  }
            set { Nodes[RIGHT] = value; }
        }

        /// <summary>Returns the given child node.</summary>
        internal Node<TKey, TValue> this[uint dir]
        {
            get { return Nodes[dir];  }
            set { Nodes[dir] = value; }
        }

        /// <summary>Checks the node for valid AVL Tree properties.</summary>
        /// <exception cref="BSTException">Thrown if the Binary Search Tree is not valid.</exception>
        [Conditional( "DEBUG" )]
        internal virtual void Check()
        {
            // First verify that subtrees are correct
            if ( Left != null )
                Left.Check();

            if ( Right != null )
                Right.Check();

            // Verify that search-tree property is satisfied
            if ( ( Left != null ) && ( Left.Key.CompareTo( Key ) > 0 ) )
                throw new BSTException("Current Node is larger than the left subtree", Key, Value, Left.Key, Left.Value);

            if ( ( Right != null ) && ( Right.Key.CompareTo( Key ) < 0 ) )
                throw new BSTException("Current Node is smaller than the right subtree", Key, Value, Right.Key, Right.Value);
        }

        /// <summary>Returns the opposite direction of the given index.</summary>
        protected static uint Opposite( uint dir )
        {
            return (1 - dir);
        }

        /// <summary>Performs a comparison with the passed in key value.</summary>
        /// <param name="key">Key to compare to.</param>
        /// <param name="cmp">Type of comparison to perform.</param>
        /// <returns>Returns -1 if less than, 1 if greater than, and 0 if equal.</returns>
        protected int Compare( TKey key, CompType cmp )
        {
            switch ( cmp )
            {
            case CompType.MIN_CMP:
                return ( Nodes[LEFT] == null ) ? 0 : -1;
            case CompType.MAX_CMP:
                return ( Nodes[RIGHT] == null ) ? 0 : 1;
            case CompType.EQ_CMP: default:
                return key.CompareTo( Key );
            }
        }

        /// <summary>Comparison Type Enumeration</summary>
        public enum CompType
        {
            /// <summary>Compare left node for null.</summary>
            MIN_CMP = -1,
            /// <summary>Compare key values for equality.</summary>
            EQ_CMP = 0,
            /// <summary>Compare right node for null.</summary>
            MAX_CMP = 1
        }
    }
}