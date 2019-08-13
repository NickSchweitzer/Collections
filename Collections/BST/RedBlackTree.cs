using System;
using System.Collections;

namespace TheCodingMonkey.Collections.BST
{
    /// <summary>Implementation of a Balanced Red-Black Binary Search Tree.</summary>
    public class RedBlackTree<TKey, TValue> : BinarySearchTree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        /// <summary>Default Constructor</summary>
        public RedBlackTree()
        { }

        /// <summary>Red Black Tree Constructor</summary>
        /// <param name="comparer">Comparison object to use for comparing dictionary keys.</param>
        public RedBlackTree( IComparer comparer )
        : base( comparer )
        { }

        protected override BinarySearchTree<TKey, TValue> CreateInstance()
        {
            return new RedBlackTree<TKey, TValue>
            {
                Count = Count
            };
        }

        /// <summary>Adds a node to the tree.</summary>
        /// <param name="data">Data to be contained at the new node location.</param>
        /// <param name="key">Key used for comparison and lookup.</param>
        /// <exception cref="ArgumentNullException">Thrown if data is null.</exception>
        /// <exception cref="ArgumentException">Thrown if data already exists in the tree.</exception>
        /// <exception cref="ArgumentException">Thrown if key is not of type IComparable.</exception>
        public override void Add( TKey key, TValue data )
        {
            if ( key == null )
                throw new ArgumentNullException( "key" );

            Node<TKey, TValue> parent = null;
            RedBlackNode<TKey, TValue>.Add( ref Root, ref parent, key, data );
            Count++;

            // Do a Debug Sanity Check on the Tree
            if ( Root != null )
                Root.Check();
        }

        /// <summary>Removes the node containing this data from the tree.</summary>
        /// <param name="key">Value to remove from the tree.</param>
        /// <exception cref="ArgumentNullException">Thrown if data is null.</exception>
        /// <exception cref="ArgumentException">Thrown if key is not of type IComparable.</exception>
        /// <remarks>If data does not exist in the tree, then the tree remains unchanged.  No exception is thrown.</remarks>
        public override bool Remove( TKey key )
        {
            if ( key == null )
                throw new ArgumentNullException( "key" );

            bool blackDecrease = false;
            bool bFound = false;
            if ( RedBlackNode<TKey, TValue>.Remove( ref Root, key, ref blackDecrease, Node<TKey, TValue>.CompType.EQ_CMP ) != null )
            {
                Count--;
                bFound = true;
            }

            // Do a Debug Sanity Check on the Tree
            if ( Root != null )
                Root.Check();

            return bFound;
        }
    }
}