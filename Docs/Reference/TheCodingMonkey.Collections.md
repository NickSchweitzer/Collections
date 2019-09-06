# TheCodingMonkey.Collections assembly

## TheCodingMonkey.Collections namespace

| public type | description |
| --- | --- |
| class [CmdArguments](TheCodingMonkey.Collections/CmdArguments.md) | Class for performing verbose parsing of command line arguments. Accepts command line parameters in a variety of patterns, and puts the keys and values in a StringDictionary for easy use later. |
| interface [IBiDirEnumerator&lt;T&gt;](TheCodingMonkey.Collections/IBiDirEnumerator-1.md) | Defines an enumerator that go go forward and backwards. |

## TheCodingMonkey.Collections.BST namespace

| public type | description |
| --- | --- |
| class [AVLException](TheCodingMonkey.Collections.BST/AVLException.md) | Exception thrown if an AVL Search Tree fails validation. |
| class [AVLNode&lt;TKey,TValue&gt;](TheCodingMonkey.Collections.BST/AVLNode-2.md) | AVL Binary Search Tree Node. |
| class [AVLTree&lt;TKey,TValue&gt;](TheCodingMonkey.Collections.BST/AVLTree-2.md) | Implementation of a Balanced AVL Binary Search Tree. |
| class [BinarySearchTree&lt;TKey,TValue&gt;](TheCodingMonkey.Collections.BST/BinarySearchTree-2.md) | Basic Unbalanced Binary Search Tree Implementation. |
| class [BSTException](TheCodingMonkey.Collections.BST/BSTException.md) | Exception thrown if a Binary Search Tree fails validation. |
| abstract class [FlatEnumerator&lt;TKey,TValue&gt;](TheCodingMonkey.Collections.BST/FlatEnumerator-2.md) | Abstract base class for all BinarySearchTree enumerators. Implements an Enumerator for a Binary Search Tree by traversing the entire tree at Construction and flattening it according to the rules of the enumerator. |
| class [InOrderFlatEnumerator&lt;TKey,TValue&gt;](TheCodingMonkey.Collections.BST/InOrderFlatEnumerator-2.md) | In Order Traversal Enumerator. The BinarySearchTree is enumerated in the order of Left Node, Current Value, Right Node. |
| class [LevelOrderFlatEnumerator&lt;TKey,TValue&gt;](TheCodingMonkey.Collections.BST/LevelOrderFlatEnumerator-2.md) | Level Order Flat Enumerator. The Binary Search Tree is enumerated breadth first at time of Enumerator construction. |
| class [Node&lt;TKey,TValue&gt;](TheCodingMonkey.Collections.BST/Node-2.md) | Binary Search Tree Node. |
| class [PostOrderFlatEnumerator&lt;TKey,TValue&gt;](TheCodingMonkey.Collections.BST/PostOrderFlatEnumerator-2.md) | Post Order Traversal Enumerator. The BinarySearchTree is enumerated in the order of Left Node, Right Node, Current Value. |
| class [PreOrderFlatEnumerator&lt;TKey,TValue&gt;](TheCodingMonkey.Collections.BST/PreOrderFlatEnumerator-2.md) | Pre Order Traversal Enumerator. The BinarySearchTree is enumerated in the order of Current Value, Left Node, Right Node. |
| class [RedBlackNode&lt;TKey,TValue&gt;](TheCodingMonkey.Collections.BST/RedBlackNode-2.md) | Red-Black Binary Search Tree Node. |
| class [RedBlackTree&lt;TKey,TValue&gt;](TheCodingMonkey.Collections.BST/RedBlackTree-2.md) | Implementation of a Balanced Red-Black Binary Search Tree. |
| abstract class [TraversalEnumerator&lt;TKey,TValue&gt;](TheCodingMonkey.Collections.BST/TraversalEnumerator-2.md) |  |

## TheCodingMonkey.Collections.Iterators namespace

| public type | description |
| --- | --- |
| class [IterIsolate](TheCodingMonkey.Collections.Iterators/IterIsolate.md) | Isolate this the iteration from the collection. Allows you to modify the underlying collection while in the middle of a foreach loop. |
| class [IterReverse](TheCodingMonkey.Collections.Iterators/IterReverse.md) | Iterate a collection in the reverse order |
| class [IterSelect](TheCodingMonkey.Collections.Iterators/IterSelect.md) | Iterate a collection in the Select order. |
| delegate [IterSelectDelegate](TheCodingMonkey.Collections.Iterators/IterSelectDelegate.md) | Predicate to select iterated elements. |
| class [IterSort](TheCodingMonkey.Collections.Iterators/IterSort.md) | Iterate a collection in sorted order, either using the built-in ordering for the object or using a class implementing IComparer. |
| class [IterSortHashValue](TheCodingMonkey.Collections.Iterators/IterSortHashValue.md) | Iterate the keys in a hashtable, ordering them by the values corresponding to those keys. Either uses the defined ordering on the values or a passed-in IComparer implementation. |
| class [IterSubList](TheCodingMonkey.Collections.Iterators/IterSubList.md) | Enumerate a collection, skipping some items at the beginning or end while enumerating |
| class [IterType](TheCodingMonkey.Collections.Iterators/IterType.md) | Iterate a collection in the Type order |

## TheCodingMonkey.Collections.Lists namespace

| public type | description |
| --- | --- |
| class [ForwardEnumerator&lt;T&gt;](TheCodingMonkey.Collections.Lists/ForwardEnumerator-1.md) | Forward Linked List Enumerator. |
| class [LinkedList&lt;T&gt;](TheCodingMonkey.Collections.Lists/LinkedList-1.md) | Implements a doubly linked list. |
| abstract class [ListEnumerator&lt;T&gt;](TheCodingMonkey.Collections.Lists/ListEnumerator-1.md) | Base class for all LinkedList Enumerators. |
| class [Node&lt;T&gt;](TheCodingMonkey.Collections.Lists/Node-1.md) | Encapsulates a Linked List Node |
| class [ReverseEnumerator&lt;T&gt;](TheCodingMonkey.Collections.Lists/ReverseEnumerator-1.md) | Reverse Linked List Enumerator. |

## TheCodingMonkey.Collections.SkipList namespace

| public type | description |
| --- | --- |
| class [Enumerator&lt;TKey,TValue&gt;](TheCodingMonkey.Collections.SkipList/Enumerator-2.md) | SkipList Enumerator. |
| class [Node&lt;TKey,TValue&gt;](TheCodingMonkey.Collections.SkipList/Node-2.md) | SkipList Node. |
| class [SkipList&lt;TKey,TValue&gt;](TheCodingMonkey.Collections.SkipList/SkipList-2.md) | Implementation for a Skip List. |

## TheCodingMonkey.Collections.Sort namespace

| public type | description |
| --- | --- |
| class [BubbleSort&lt;T&gt;](TheCodingMonkey.Collections.Sort/BubbleSort-1.md) | Implementation of a Bubble sort (sinking sort) for IList |
| class [HeapSort&lt;T&gt;](TheCodingMonkey.Collections.Sort/HeapSort-1.md) | Implementation of a Bubble sort (sinking sort) for IList |
| interface [ISort&lt;T&gt;](TheCodingMonkey.Collections.Sort/ISort-1.md) | Generic interface for all algorithms in this collection that perform an in-place sort of an IList |
| class [OddEvenSort&lt;T&gt;](TheCodingMonkey.Collections.Sort/OddEvenSort-1.md) | Implementation of an Odd Even sort (brick sort) for IList |
| class [QuickSort&lt;T&gt;](TheCodingMonkey.Collections.Sort/QuickSort-1.md) | Implementation of a Quicksort (partition-exchange sort) for IList |

<!-- DO NOT EDIT: generated by xmldocmd for TheCodingMonkey.Collections.dll -->
