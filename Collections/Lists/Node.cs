using System;

namespace TheCodingMonkey.Collections.Lists
{
    /// <summary>Encapsulates a Linked List Node</summary>
	public class Node<T>
	{
        internal Node( T value )
        : this( value, null )
        { }

        internal Node( T value, Node<T> next )
        {
            Value   = value;
            Next = next;
        }

        /// <summary>Previous node in the list.  Null if this is the head of the list.</summary>
        public Node<T> Previous { get; set; } = null;

        /// <summary>Next node in the list.  Null of this is the tail of the list.</summary>
        public Node<T> Next { get; set; } = null;

        /// <summary>Object contained at this node in the list.</summary>
        public T Value { get; set; } = default;
    }
}