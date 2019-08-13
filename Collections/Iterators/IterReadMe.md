Iterators

This project contains Iterators make the foreach statement more 
useful. The base iterator is IterIsolate, which isolates the enumerator from
the collection, so the collection can be changed in the foreach.

The iterators are as follows:

IterIsolate			Isolate the enumerator from the collection
IterRandom			Return the items in random order
IterReverse			Return the items in reverse order
IterSelect			Return only those items selected by a user-defined function
IterSort			Return the items in sorted order
IterSubList			Return a sublist of the items
IterType			Return only those items of a specific type
IterSortHashValue	Return key values sorted by the corresponding
					values in the hashtable.

Iterators work by fully enumerating the passed-in collection, storing
it in an ArrayList, and then enumerating over that.
					
Iterators are used in the following manner:

foreach (string s in new IterSort(collection))
{
	...
}

Iterators can be chained together:

foreach (string s in new IterReverse(new IterSort(new IterSubList(items, 1, 1))))
{
	...
}

When iterators are chained, only one temporary ArrayList object is
created. 

Caveats:
*******

There are a few caveats to using these iterators. There is more
overhead if you use something like IterReverse, since the 
iterator will create a ArrayList to hold the values. 

The second caveat is that these iterators only work on objects that
support IEnumerable, which means the values returned from the 
enumerator will be boxed if they're value types. This is only an 
issue if the enumerator uses the strongly-typed enumerator idiom 
(ie Current isn't of type object, but of some other type) *and* that
type is a value type.

