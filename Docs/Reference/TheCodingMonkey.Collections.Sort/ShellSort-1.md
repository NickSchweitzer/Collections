# ShellSort&lt;T&gt; class

Implementation of a Shell Sort for IList

```csharp
public class ShellSort<T> : IInPlaceSort<T>
    where T : IComparable
```

| parameter | description |
| --- | --- |
| T | T must implement IComparable |

## Public Members

| name | description |
| --- | --- |
| [ShellSort](ShellSort-1/ShellSort.md)() | The default constructor. |
| [Sort](ShellSort-1/Sort.md)(…) | Performs an in-place sort of the collection. |

## Remarks

More information about Shell Sort can be found at [Wikipedia](https://en.wikipedia.org/wiki/Shellsort) or [this blog post](https://exceptionnotfound.net/shell-sort-csharp-the-sorting-algorithm-family-reunion/).

## See Also

* interface [IInPlaceSort&lt;T&gt;](IInPlaceSort-1.md)
* namespace [TheCodingMonkey.Collections.Sort](../TheCodingMonkey.Collections.md)

<!-- DO NOT EDIT: generated by xmldocmd for TheCodingMonkey.Collections.dll -->