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

A Red Black Tree is a type of self balancing binary search tree. Each node tracks a color enumeration, and therfore stores slightly more information about the node
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

## Linked List

## [Command Line Argument Collection](./Reference/TheCodingMonkey.Collections/CmdArguments.md)

The command line argument collection takes the argument string from a `main` function, and parses command line switches, adding them to an internal dictionary.

Valid parameters forms are:
`{-,/,--}param{ ,=,:}((",')value(",'))`

Examples: `-param1 value1 --param2 /param3:"Test-:-work" /param4=happy -param5 '--=nice=--'`

[More Information](./CmdArguments.md)

# Iterators

# Library References

[Full reference documentation](./Reference/TheCodingMonkey.Collections.md) of the class library is available as well.