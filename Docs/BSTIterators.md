# Binary Search Tree Iterators

All of the Iterators implemented in this library "flatten" the binary search tree at the time of construction of the enuermator class. That means there is a shallow copy made of all the nodes in the order in which they will be traversed to a seperate collection.

![Binary Search Tree Sample](./Images/sample_bst.png)

For a Binary Search Tree with the nodes as defined above, the different iterators will traverse in the following orders:

* In Order: 4, 2, 5, 1, 3
* Pre-Order: 1, 2, 4, 5, 3
* Post-Order: 4, 5, 2, 3, 1
* Level Order: 1, 2, 3, 4, 5