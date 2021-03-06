# RedBlackTree&lt;TKey,TValue&gt;.Remove method

Removes the node containing this data from the tree.

```csharp
public override bool Remove(TKey key)
```

| parameter | description |
| --- | --- |
| key | Value to remove from the tree. |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException | Thrown if data is null. |
| ArgumentException | Thrown if key is not of type IComparable. |

## Remarks

If data does not exist in the tree, then the tree remains unchanged. No exception is thrown.

## See Also

* class [RedBlackTree&lt;TKey,TValue&gt;](../RedBlackTree-2.md)
* namespace [TheCodingMonkey.Collections.BST](../../TheCodingMonkey.Collections.md)

<!-- DO NOT EDIT: generated by xmldocmd for TheCodingMonkey.Collections.dll -->
