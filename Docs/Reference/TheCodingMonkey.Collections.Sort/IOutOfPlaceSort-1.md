# IOutOfPlaceSort&lt;T&gt; interface

Generic interface for all algorithms in this collection that perform an out of place sort of an IList

```csharp
public interface IOutOfPlaceSort<T>
    where T : IComparable
```

| parameter | description |
| --- | --- |
| T | T must implement IComparable |

## Members

| name | description |
| --- | --- |
| [Sort](IOutOfPlaceSort-1/Sort.md)(…) | Performs an in-place sort of the collection. |

## Remarks

The passed in Collection is unmodified by the sorting algorithm.

## See Also

* namespace [TheCodingMonkey.Collections.Sort](../TheCodingMonkey.Collections.md)

<!-- DO NOT EDIT: generated by xmldocmd for TheCodingMonkey.Collections.dll -->
