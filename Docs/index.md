# The Coding Monkey Collections

This library contains collections and algorithms that are not part of the standard .NET Generic Collections.  This library is compatible with the full .NET library, as well as .NET Core.

# Collections

## Binary Search Tree Implementations

There are three Generic Binary Search Tree implementations in the library. All three require that the Key implement [IComparable<T>](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable-1), and that
the keys are unique.

### [Basic Unbalanced Binary Search Tree](./Reference/TheCodingMonkey.Collections.BST/BinarySearchTree-2.md)

The BinarySearchTree class is the base class for the other more specialized versions. The basic tree will degrade in performce over time, as items are added depending 
on the order in which they are added. Adding the same keys to the tree in different orders can affect performance because the tree does not self balance. Average times for
all operations are therefore O(log n) with a worst case of O(n).

[More information](https://en.wikipedia.org/wiki/Binary_search_tree).

### [Red Black Tree](./Reference/TheCodingMonkey.Collections.BST/RedBlackTree-2.md)

A Red Black Tree is a type of self balancing binary search tree. Each node tracks a color enumeration, and therefore stores slightly more information about the node
than a standard binary search tree. Because it is self balancing, all operations are O(log n).

[More information](https://en.wikipedia.org/wiki/Red–black_tree).

### [AVL Tree](./Reference/TheCodingMonkey.Collections.BST/AVLTree-2.md)

The AVL Tree is an implementation of a balanced binary search tree named after Adelson-Velsky and Landis. An AVL Tree will be slightly faster than a Red Black Tree 
because the balancing is more strict. All operations are O(log n). 

[More information](https://en.wikipedia.org/wiki/AVL_tree).

### Iterators

Binary Search Trees support iteration in differnet ways. All of the Binary Search Tree implementations support the following iterators:

* [In Order Traversal](./Reference/TheCodingMonkey.Collections.BST/InOrderFlatEnumerator-2.md)
* [Pre-Order Traversal](./Reference/TheCodingMonkey.Collections.BST/PreOrderFlatEnumerator-2.md)
* [Post-Order Traversal](./Reference/TheCodingMonkey.Collections.BST/PostOrderFlatEnumerator-2.md)
* [Level Order (Breadth First) Traversal](./Reference/TheCodingMonkey.Collections.BST/LevelOrderFlatEnumerator-2.md)

[More Information](./BSTIterators.md)

## Skip List

Skip lists are a data structure that defines a linked hierary of sublists, with each sublist having fewer items than the one before. In many ways its a combination of an array with a linked list. Search and insert are both O(log n).

This implementation implements [`IDictionary<TKey, TValue>`](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2) and can be used as a drop in replacement for the [`Dictionary`](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.valuecollection) class, assuming that `TKey` implements [`ICompareable<T>`](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable-1).

## Linked List

This linked list implementation was origionally written prior to .NET 2.0, which is when Microsoft added its own LinkedList implementation to the [`System.Collections.Generic` namespace](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic). It supports many of the same operations, though with slightly different method names.

## [Command Line Argument Collection](./Reference/TheCodingMonkey.Collections/CmdArguments.md)

The command line argument collection takes the argument string from a `main` function, and parses command line switches, adding them to an internal dictionary.

Valid parameters forms are:
`{-,/,--}param{ ,=,:}((",')value(",'))`

Examples: `-param1 value1 --param2 /param3:"Test-:-work" /param4=happy -param5 '--=nice=--'`

[More Information](./CmdArguments.md)

# Sorting Algorithms

Implemented several popular sorting algorithms that all operate generically on any collection wich implements [`IList<T>`](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ilist-1) where `T` 
implements [`IComparable`](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable). [A summary of the different performance characteristics](./SortingPerformance.md) of these algorithms is also available.

## In Place Sorting Algorithms

These algorithms modify the list which is passed into the sorting function. They all derive from [`IInPlaceSort<T>`](./Reference/TheCodingMonkey.Collections.Sort/IInPlaceSort-1.md)

* [Quick Sort](./Reference/TheCodingMonkey.Collections.Sort/QuickSort-1.md)
* [Bubble Sort](./Reference/TheCodingMonkey.Collections.Sort/BubbleSort-1.md)
* [Heap Sort](./Reference/TheCodingMonkey.Collections.Sort/HeapSort-1.md)
* [Odd Even Sort](./Reference/TheCodingMonkey.Collections.Sort/OddEvenSort-1.md)
* [Comb Sort](./Reference/TheCodingMonkey.Collections.Sort/CombSort-1.md)
* [Shell Sort](./Reference/TheCodingMonkey.Collections.Sort/ShellSort-1.md)
* [Bitonic Merge Sort](./Reference/TheCodingMonkey.Collections.Sort/BitonicMergeSort-1.md)
* [Insertion Sort](./Reference/TheCodingMonkey.Collections.Sort/InsertionSort-1.md)
* [Selection Sort](./Reference/TheCodingMonkey.Collections.Sort/SelectionSort-1.md)

## Out of Place Sorting Algorithms

These algorithms create a new list sorted list from the unsorted list passed into the sorting function. They all derive from [`IOutOfPlaceSort<T>`](./Reference/TheCodingMonkey.Collections.Sort/IOutOfPlaceSort-1.md)

* [Merge Sort](./Reference/TheCodingMonkey.Collections.Sort/MergeSort-1.md)

# Iterators

# Library References

[Full reference documentation](./Reference/TheCodingMonkey.Collections.md) of the class library is available as well.