using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheCodingMonkey.Collections.SkipList;

namespace TheCodingMonkey.Collections.Tests
{
    [TestClass]
    public class SkipListTests : DictionaryTest
    {
        [TestInitialize]
        public override void Init()
        {
            m_Dictionary = new SkipList<int, int>();
            referenceDictionary = new SkipList<CloneableInt, CloneableInt>();
        }
    }
}