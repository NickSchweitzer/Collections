using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheCodingMonkey.Collections.BST;

namespace TheCodingMonkey.Collections.Tests
{
    [TestClass, TestCategory("BST")]
    public abstract class BalancedTreeTest
    {
        protected BinarySearchTree<int, int> bst;

        [TestInitialize]
        public abstract void Init();

        [TestMethod]
        public void TraversalOrderTest()
        {
            Assert.IsTrue(bst.Empty);

            int[] addItems = new int[] { 13, 3, 20, 7, 15, 1, 4 };
            //          13
            //      3           20
            //  1       7   15
            //        4
            foreach (int i in addItems)
                bst.Add(i, i);

            int[] preOrderItems = new int[] { 13, 3, 1, 7, 4, 20, 15 };
            int[] postOrderItems = new int[] { 1, 4, 7, 3, 15, 20, 13 };
            int[] levelOrderItems = new int[] { 13, 3, 20, 1, 7, 15, 4 };

            AssertTraversalOrder(new PreOrderFlatEnumerator<int, int>(bst), preOrderItems);
            AssertTraversalOrder(new PostOrderFlatEnumerator<int, int>(bst), postOrderItems);
            AssertTraversalOrder(new LevelOrderFlatEnumerator<int, int>(bst), levelOrderItems);
        }

        private void AssertTraversalOrder(FlatEnumerator<int, int> enumerator, int[] expectedItems)
        {
            int i = 0;
            while(enumerator.MoveNext())
            {
                Assert.AreEqual(expectedItems[i], enumerator.Key);
                Assert.AreEqual(expectedItems[i], enumerator.Value);
                Assert.AreEqual(expectedItems[i], enumerator.Current.Key);
                Assert.AreEqual(expectedItems[i], enumerator.Current.Value);
                i++;
            }
        }
    }

    [TestClass, TestCategory("AVL")]
    public class AVLIteratorTest : BalancedTreeTest
    {
        [TestInitialize]
        public override void Init()
        {
            bst = new AVLTree<int, int>();
        }
    }

    [TestClass, TestCategory("Red Black")]
    public class RedBlackIteratorTest : BalancedTreeTest
    {
        [TestInitialize]
        public override void Init()
        {
            bst = new RedBlackTree<int, int>();
        }
    }
}