# BitonicMergeSort&lt;T&gt; class

Implementation of a Bitonic Merge Sort (partition-exchange sort) for IList

```csharp
public class BitonicMergeSort<T> : IInPlaceSort<T>
    where T : IComparable
```

| parameter | description |
| --- | --- |
| T | T must implement IComparable |

## Public Members

| name | description |
| --- | --- |
| [BitonicMergeSort](BitonicMergeSort-1/BitonicMergeSort.md)() | The default constructor. |
| [Sort](BitonicMergeSort-1/Sort.md)(…) | Performs an in-place sort of the collection. (2 methods) |

## Remarks

The collection being sorted must have a length that is a Power of 2. More information about Bitonic Merge Sorting can be found at [Wikipedia](https://en.wikipedia.org/wiki/Bitonic_sorter) or [this blog post](https://exceptionnotfound.net/bitonic-merge-sort-csharp-the-sorting-algorithm-family-reunion/).

## See Also

* interface [IInPlaceSort&lt;T&gt;](IInPlaceSort-1.md)
* namespace [TheCodingMonkey.Collections.Sort](../TheCodingMonkey.Collections.md)

<!-- DO NOT EDIT: generated by xmldocmd for TheCodingMonkey.Collections.dll -->
