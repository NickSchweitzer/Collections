using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheCodingMonkey.Collections.BST;

namespace TheCodingMonkey.Collections.Tests
{
    [TestClass, TestCategory("AVL")]
    public class AVLTest : DictionaryTest
    {
        [TestInitialize]
        public override void Init()
        {
            m_Dictionary = new AVLTree<int, int>();
            referenceDictionary = new AVLTree<CloneableInt, CloneableInt>();
        }
    }
}