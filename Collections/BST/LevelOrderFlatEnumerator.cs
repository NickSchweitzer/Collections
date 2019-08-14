using System;
using System.Collections.Generic;

namespace TheCodingMonkey.Collections.BST
{
    /// <summary>In Order Flat Enumerator.  The Binary Search Tree is enumerated in the order of Left Node, Current Value, Right Node at time of Enumerator construction.</summary>
    public class LevelOrderFlatEnumerator<TKey, TValue> : FlatEnumerator<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        private Queue<Node<TKey, TValue>> m_Queue = null;

        /// <summary>Standard Constructor.</summary>
        /// <param name="tree">BinarySearchTree to create an Enumerator for.</param>
        public LevelOrderFlatEnumerator(BinarySearchTree<TKey, TValue> tree)
        : base(tree)
        { }

        /// <summary>Defines a traversal function for shallow copying the elements from the BinarySearchTree
        /// into the passed in ArrayList.</summary>
        /// <param name="current">Node to Traverse</param>
        /// <param name="contents">ArrayList to contain the re-ordered elements.</param>
        protected override void Traverse(Node<TKey, TValue> current, ICollection<KeyValuePair<TKey, TValue>> contents)
        {
            if (m_Queue == null)
            {
                m_Queue = new Queue<Node<TKey, TValue>>();
                Enqueue(current);
            }

            contents.Add(new KeyValuePair<TKey, TValue>(current.Key, current.Value));
            current = m_Queue.Dequeue();
            Enqueue(current);
            Traverse(current, contents);
        }

        private void Enqueue(Node<TKey, TValue> current)
        {
            if (current.Left != null)
                m_Queue.Enqueue(current.Left);
            if (current.Right != null)
                m_Queue.Enqueue(current.Right);
        }
    }
}