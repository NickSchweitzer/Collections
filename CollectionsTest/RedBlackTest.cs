using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheCodingMonkey.Collections.BST;

namespace TheCodingMonkey.Collections.Tests
{
    [TestClass, TestCategory("Red Black")]
    public class RedBlackTest : DictionaryTest
    {
        [TestInitialize]
        public override void Init()
        {
            m_Dictionary = new RedBlackTree<int, int>();
            referenceDictionary = new RedBlackTree<CloneableInt, CloneableInt>();
        }
    }
}