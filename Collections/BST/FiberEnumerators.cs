using System;
using System.Collections;

using SummerCampProductions.Threading;

namespace SummerCampProductions.Collections.BST
{
    /// <summary>Abstract base class for all BinarySearchTree enumerators using Fibers.</summary>
    public abstract class FiberEnumerator : Fiber, IDictionaryEnumerator
    {
        #region Private Data

        BinarySearchTree m_Tree = null;
        Node m_nodeCurrent = null;

        #endregion

        #region Construction

        /// <summary>Standard Constructor.</summary>
        /// <param name="tree">BinarySearchTree to create an Enumerator for.</param>
        public FiberEnumerator( BinarySearchTree tree )
        {
            m_Tree = tree;
        }

        #endregion

        #region IDictionaryEnumerator Members

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

        /// <summary>Gets both the key and the value of the current dictionary entry.</summary>
        public DictionaryEntry Entry
        {
            get { return new DictionaryEntry( m_nodeCurrent.Key, m_nodeCurrent.Value ); }
        }

        #endregion

        #region IEnumerator Members

        /// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
        public void Reset()
        {
            // This don't work too well
        }

        /// <summary>Gets the current element in the collection.</summary>
        public object Current
        {
            get { return Entry; }
        }

        /// <summary>Advances the enumerator to the next element of the collection.</summary>
        /// <returns>Returns true if the move was successful.</returns>
        public bool MoveNext()
        {
            m_nodeCurrent = (Node)Resume();
            return ( m_nodeCurrent != null );
        }

        #endregion

        #region Protected Members

        /// <summary>Begins traversing the tree at the root node.</summary>
        protected override sealed void Run()
        {
            Traverse( m_Tree.Root );
        }

        /// <summary>Defines how the tree is traversed for the different styles of enumeration.</summary>
        /// <param name="node">Node to traverse.</param>
        abstract protected void Traverse( Node node );

        #endregion
    }

    /// <summary>Enumerates the tree in an in order fashion.</summary>
    public class InOrderFiberEnumerator : FiberEnumerator
    {
        /// <summary>Constructor.</summary>
        /// <param name="tree">Binary Search Tree to Enumerate.</param>
        public InOrderFiberEnumerator( BinarySearchTree tree )
        : base( tree )
        {}

        /// <summary>Traverses the given node in an in-order fashion.</summary>
        /// <param name="node">Node to traverse.</param>
        protected override void Traverse( Node node )
        {
            if ( node != null )
            {
                Traverse( node.Left );
                Yield( node );
                Traverse( node.Right );
            }
        }
    }
}