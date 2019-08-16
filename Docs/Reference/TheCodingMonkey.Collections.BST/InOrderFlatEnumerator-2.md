# InOrderFlatEnumerator&lt;TKey,TValue&gt; class

InOrder Traversal Enumerator. The BinarySearchTree is enumerated in the order of Left Node, Current Value, Right Node.

```csharp
public class InOrderFlatEnumerator<TKey, TValue> : FlatEnumerator<TKey, TValue>
    where TKey : IComparable<TKey>
```

## Public Members

| name | description |
| --- | --- |
| [InOrderFlatEnumerator](InOrderFlatEnumerator-2/InOrderFlatEnumerator.md)(…) | Standard Constructor. |

## Protected Members

| name | description |
| --- | --- |
| override [Traverse](InOrderFlatEnumerator-2/Traverse.md)(…) | Defines a traversal function for shallow copying the elements from the BinarySearchTree into the passed in ArrayList. |

## See Also

* class [FlatEnumerator&lt;TKey,TValue&gt;](FlatEnumerator-2.md)
* namespace [TheCodingMonkey.Collections.BST](../TheCodingMonkey.Collections.md)

<!-- DO NOT EDIT: generated by xmldocmd for TheCodingMonkey.Collections.dll -->