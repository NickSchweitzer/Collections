//using System;
//using System.Collections;
//using System.Collections.Generic;

//namespace TheCodingMonkey.Collections.BST
//{
//    public abstract class TraversalEnumerator<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>>
//        where TKey : IComparable<TKey>
//    {
//        private BinarySearchTree<TKey, TValue> Tree { get; set; }
//        protected Node<TKey, TValue> currentNode = null;

//        public TraversalEnumerator(BinarySearchTree<TKey, TValue> tree)
//        {
//            Tree = tree;
//        }

//        public KeyValuePair<TKey, TValue> Current
//        {
//            get
//            {
//                if (currentNode == null)
//                    throw new IndexOutOfRangeException();

//                return new KeyValuePair<TKey, TValue>(currentNode.Key, currentNode.Value);
//            }
//        }

//        object IEnumerator.Current
//        {
//            get
//            {
//                if (currentNode == null)
//                    throw new IndexOutOfRangeException();

//                return new KeyValuePair<TKey, TValue>(currentNode.Key, currentNode.Value);
//            }
//        }

//        public abstract bool MoveNext();

//        public void Reset()
//        {
//            currentNode = null;
//        }

//        public void Dispose()
//        { }
//    }
//}