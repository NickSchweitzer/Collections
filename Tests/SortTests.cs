using System;
using TheCodingMonkey.Collections.Sort;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheCodingMonkey.Collections.Tests
{
    [TestClass, TestCategory("Sort")]
    public class SortTests
    {
        protected int[] unsorted;
        protected int[] unsorted2;

        [TestInitialize]
        public void Init()
        {
            unsorted = new int[] { 72, 12, 6, 33, 81, 97, 37, 59, 52, 1, 20 };

            // For Bitonic Sort which required a collection to be a power of 2
            unsorted2 = new int[] { 66, 98, 11, 43, 7, 28, 14, 49, 77, 61, 31, 12, 71, 93, 15, 2 };
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

        [TestMethod]
        public void ShellSortTest()
        {
            TestSort(new ShellSort<int>());
        }

        [TestMethod]
        public void InsertionSortTest()
        {
            TestSort(new InsertionSort<int>());
        }

        [TestMethod]
        public void BitonicMergeSortTest()
        {
            TestSort(new BitonicMergeSort<int>(), unsorted2);
        }

        [TestMethod]
        public void BitonicMergeSortInvalidCountTest()
        {
            Assert.ThrowsException<ArgumentException>(() => TestSort(new BitonicMergeSort<int>()));
        }

        private void TestSort(ISort<int> sorter, int[] arrayToSort)
        {
            sorter.Sort(arrayToSort);
            VerifySorted(arrayToSort);
        }

        private void TestSort(ISort<int> sorter)
        {
            TestSort(sorter, unsorted);
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