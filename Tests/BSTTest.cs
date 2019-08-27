using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheCodingMonkey.Collections.BST;

namespace TheCodingMonkey.Collections.Tests
{
    [TestClass, TestCategory("BST")]
    public class BSTTest : DictionaryTest
    {
        [TestInitialize]
        public override void Init()
        {
            m_Dictionary = new BinarySearchTree<int, int>();
            referenceDictionary = new BinarySearchTree<CloneableInt, CloneableInt>();
        }

        [TestMethod]
        public void TestEmpty()
        {
            BinarySearchTree<int, int> bst = (BinarySearchTree<int, int>)m_Dictionary;
            Assert.IsTrue(bst.Empty);
            AddToEmpty(10);
            Assert.IsFalse(bst.Empty);
        }
    }
}