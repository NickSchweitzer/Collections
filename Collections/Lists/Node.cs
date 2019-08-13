using System;

namespace TheCodingMonkey.Collections.Lists
{
    /// <summary>Encapsulates a Linked List Node</summary>
	public class Node
	{
        /// <summary>Default Constructor</summary>
        internal Node()
        : this( null )
		{ }

        internal Node( object value )
        : this( value, null )
        { }

        internal Node( object value, Node next )
        {
            Value   = value;
            Next = next;
        }

        /// <summary>Previous node in the list.  Null if this is the head of the list.</summary>
        public Node Prev { get; set; } = null;

        /// <summary>Next node in the list.  Null of this is the tail of the list.</summary>
        public Node Next { get; set; } = null;

        /// <summary>Object contained at this node in the list.</summary>
        public object Value { get; set; } = null;
    }
}