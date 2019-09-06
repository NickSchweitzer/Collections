using System;
using TheCodingMonkey.Collections.Sort;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheCodingMonkey.Collections.Tests
{
    [TestClass, TestCategory("Sort")]
    public class SortTests
    {
        protected int[] unsorted;

        [TestInitialize]
        public void Init()
        {
            unsorted =  new int[] { 72, 12, 6, 33, 81, 97, 37, 59, 52, 1, 20 };
        }

        [TestMethod]
        public void QuickSortTest()
        {
            TestSort(new QuickSort<int>());
        }

        [TestMethod]
        public void BubbleSortTest()
        {
            TestSort(new BubbleSort<int>());
        }

        [TestMethod]
        public void HeapSortTest()
        {
            TestSort(new HeapSort<int>());
        }

        [TestMethod]
        public void OddEvenSortTest()
        {
            TestSort(new OddEvenSort<int>());
        }

        [TestMethod]
        public void CombSortTest()
        {
            TestSort(new CombSort<int>());
        }

        private void TestSort(ISort<int> sorter)
        {
            sorter.Sort(unsorted);
            VerifySorted(unsorted);
        }

        private void VerifySorted(int[] array)
        {
            int previous = 0;
            foreach (int current in array)
            {
                Assert.IsTrue(previous < current);
                previous = current;
            }
        }
    }
}