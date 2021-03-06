# QuickSort&lt;T&gt; class

Implementation of a Quicksort (partition-exchange sort) for IList

```csharp
public class QuickSort<T> : IInPlaceSort<T>
    where T : IComparable
```

| parameter | description |
| --- | --- |
| T | T must implement IComparable |

## Public Members

| name | description |
| --- | --- |
| [QuickSort](QuickSort-1/QuickSort.md)() | The default constructor. |
| [Sort](QuickSort-1/Sort.md)(…) | Performs an in-place sort of the collection. |

## Remarks

More information about QuickSort can be found at [Wikipedia](https://en.wikipedia.org/wiki/Quicksort) or [this blog post](https://exceptionnotfound.net/quick-sort-csharp-the-sorting-algorithm-family-reunion/).

## See Also

* interface [IInPlaceSort&lt;T&gt;](IInPlaceSort-1.md)
* namespace [TheCodingMonkey.Collections.Sort](../TheCodingMonkey.Collections.md)

<!-- DO NOT EDIT: generated by xmldocmd for TheCodingMonkey.Collections.dll -->
