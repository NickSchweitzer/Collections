﻿using System;
using System.Collections.Generic;

namespace TheCodingMonkey.Collections.BST
{
    /// <summary>Pre Order Traversal Enumerator.  The BinarySearchTree is enumerated in the order of Current Value, Left Node, Right Node.</summary>
    public class PreOrderFlatEnumerator<TKey, TValue> : FlatEnumerator<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        /// <summary>Standard Constructor.</summary>
        /// <param name="tree">BinarySearchTree to create an Enumerator for.</param>
        public PreOrderFlatEnumerator(BinarySearchTree<TKey, TValue> tree)
        : base(tree)
        { }

        /// <summary>Defines a traversal function for shallow copying the elements from the BinarySearchTree
        /// into the passed in Collection.</summary>
        /// <param name="current">Node to Traverse</param>
        /// <param name="contents">ICollection to contain the re-ordered elements.</param>
        protected override void Traverse(Node<TKey, TValue> current, ICollection<KeyValuePair<TKey, TValue>> contents)
        {
            if (current != null)
            {
                contents.Add(new KeyValuePair<TKey, TValue>(current.Key, current.Value));
                Traverse(current.Left, contents);
                Traverse(current.Right, contents);
            }
        }
    }
}