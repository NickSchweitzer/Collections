using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheCodingMonkey.Collections.Sort;

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
            InPlaceTestSort(new QuickSort<int>());
        }

        [TestMethod]
        public void BubbleSortTest()
        {
            InPlaceTestSort(new BubbleSort<int>());
        }

        [TestMethod]
        public void HeapSortTest()
        {
            InPlaceTestSort(new HeapSort<int>());
        }

        [TestMethod]
        public void OddEvenSortTest()
        {
            InPlaceTestSort(new OddEvenSort<int>());
        }

        [TestMethod]
        public void CombSortTest()
        {
            InPlaceTestSort(new CombSort<int>());
        }

        [TestMethod]
        public void ShellSortTest()
        {
            InPlaceTestSort(new ShellSort<int>());
        }

        [TestMethod]
        public void InsertionSortTest()
        {
            InPlaceTestSort(new InsertionSort<int>());
        }

        [TestMethod]
        public void SelectionSortTest()
        {
            InPlaceTestSort(new SelectionSort<int>());
        }

        [TestMethod]
        public void MergeSortTest()
        {
            OutOfPlaceTestSort(new MergeSort<int>());
        }

        [TestMethod]
        public void BitonicMergeSortTest()
        {
            InPlaceTestSort(new BitonicMergeSort<int>(), unsorted2);
        }

        [TestMethod]
        public void BitonicMergeSortInvalidCountTest()
        {
            // Verifies that the sort throws an exception is passed a collection with a size that is not a power of 2
            Assert.ThrowsException<ArgumentException>(() => InPlaceTestSort(new BitonicMergeSort<int>()));
        }

        private void InPlaceTestSort(IInPlaceSort<int> sorter, int[] arrayToSort)
        {
            sorter.Sort(arrayToSort);
            VerifySorted(arrayToSort);
        }

        private void InPlaceTestSort(IInPlaceSort<int> sorter)
        {
            InPlaceTestSort(sorter, unsorted);
        }

        private void OutOfPlaceTestSort(IOutOfPlaceSort<int> sorter)
        {
            int[] original = (int[])unsorted.Clone();

            var sorted = sorter.Sort(unsorted);
            VerifySorted(sorted);

            // Verify that the original array wasn't modified
            CollectionAssert.AreEqual(original, unsorted);
        }

        private void VerifySorted(IList<int> array)
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