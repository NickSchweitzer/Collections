# MergeSort&lt;T&gt; class

Implementation of an Out of Place Merge Sort (partition-exchange sort) for IList

```csharp
public class MergeSort<T> : IOutOfPlaceSort<T>
    where T : IComparable
```

| parameter | description |
| --- | --- |
| T | T must implement IComparable |

## Public Members

| name | description |
| --- | --- |
| [MergeSort](MergeSort-1/MergeSort.md)() | The default constructor. |
| [Sort](MergeSort-1/Sort.md)(…) | Performs an in-place sort of the collection. |

## Remarks

More information about Merge Sorting can be found at [Wikipedia](https://en.wikipedia.org/wiki/Merge_sort) or [this blog post](https://exceptionnotfound.net/merge-sort-csharp-the-sorting-algorithm-family-reunion/).

## See Also

* interface [IOutOfPlaceSort&lt;T&gt;](IOutOfPlaceSort-1.md)
* namespace [TheCodingMonkey.Collections.Sort](../TheCodingMonkey.Collections.md)

<!-- DO NOT EDIT: generated by xmldocmd for TheCodingMonkey.Collections.dll -->