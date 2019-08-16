# Node&lt;TKey,TValue&gt;.CompType&lt;TKey,TValue&gt; enumeration

Comparison Type Enumeration

```csharp
public enum CompType<TKey, TValue>
    where TKey : IComparable<TKey>
```

## Values

| name | value | description |
| --- | --- | --- |
| MIN_CMP | `-1` | Compare left node for null. |
| EQ_CMP | `0` | Compare key values for equality. |
| MAX_CMP | `1` | Compare right node for null. |

## See Also

* class [Node&lt;TKey,TValue&gt;](Node-2.md)
* namespace [TheCodingMonkey.Collections.BST](../TheCodingMonkey.Collections.md)

<!-- DO NOT EDIT: generated by xmldocmd for TheCodingMonkey.Collections.dll -->