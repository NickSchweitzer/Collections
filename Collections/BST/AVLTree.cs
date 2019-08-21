using System;
using System.Collections;

namespace TheCodingMonkey.Collections.BST
{
    /// <summary>Implementation of a Balanced AVL Binary Search Tree.</summary>
    public class AVLTree<TKey, TValue> : BinarySearchTree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        /// <summary>Default Constructor</summary>
        public AVLTree()
        { }

        /// <summary>Helper function used during Clone to create a new AVL Tree</summary>
        /// <returns>A new reference to an AVL Tree that can be used in a clone operation.</returns>
        protected override BinarySearchTree<TKey, TValue> CloneInstance()
        {
            return new AVLTree<TKey, TValue>
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

            int nChange = 0;
            AVLNode<TKey, TValue>.Add( ref Root, key, data, ref nChange );
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

            int nChange = 0;
            bool bFound = false;
            if ( AVLNode<TKey, TValue>.Remove( ref Root, key, ref nChange, Node<TKey, TValue>.CompType.EQ_CMP ) != null )
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