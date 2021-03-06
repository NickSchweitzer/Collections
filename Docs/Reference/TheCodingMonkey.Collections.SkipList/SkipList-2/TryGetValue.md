# SkipList&lt;TKey,TValue&gt;.TryGetValue method

Attempts to retrieve the value for the given key.

```csharp
public bool TryGetValue(TKey key, out TValue value)
```

| parameter | description |
| --- | --- |
| key | Key to search for in the list. |
| value | Value that is present for the given key, ir present. Otherwise the default value for the object is returned. |

## Return Value

True if the key is present in the list, false otherwise.

## See Also

* class [SkipList&lt;TKey,TValue&gt;](../SkipList-2.md)
* namespace [TheCodingMonkey.Collections.SkipList](../../TheCodingMonkey.Collections.md)

<!-- DO NOT EDIT: generated by xmldocmd for TheCodingMonkey.Collections.dll -->
