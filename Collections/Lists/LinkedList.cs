using System;
using System.Collections;
using System.Collections.Generic;

namespace TheCodingMonkey.Collections.Lists
{
	/// <summary>Implements a doubly linked list.</summary>
	public class LinkedList<T> : ICloneable, ICollection<T>, IList<T>, IEnumerable<T>
	{
        /// <summary>Retrieves the first item in the list.</summary>
        public Node<T> Head { get; set; } = null;

        /// <summary>Retrieves the last item in the list.</summary>
        public Node<T> Tail { get; set; } = null;

        /// <summary>Returns true if the list is empty.</summary>
        public bool Empty => Count == 0;

        /// <summary>Makes a deep copy of this List.</summary>
        /// <returns>New instance of the list.</returns>
        public object Clone()
        {
            LinkedList<T> clone = new LinkedList<T>();
            
            Node<T> current = Head;
            while ( current != null )
            {
                T cloneValue;
                if ( current.Value is ICloneable )
                    cloneValue = (T)((ICloneable)current.Value).Clone();
                else
                    cloneValue = current.Value;

                clone.Add( cloneValue );
                current = current.Next;
            }

            return clone;
        }

        /// <summary>Returns a foward enumerator for this list.</summary>
        /// <returns>IEnumerator for this list.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new ForwardEnumerator<T>(this);
        }

        /// <summary>Returns a foward enumerator for this list.</summary>
        /// <returns>IEnumerator for this list.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ForwardEnumerator<T>(this);
        }

        /// <summary>Returns the number of items in the list.</summary>
        public int Count { get; private set; } = 0;

        /// <summary>Copies the List elements to a one-dimensional Array instance at the specified index.</summary>
        /// <param name="array">The one dimensional Array to copy the elements to.</param>
        /// <param name="index">The zero-based index in array at which copying begins.</param>
        /// <exception cref="ArgumentNullException">Thrown if array is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if index is less than zero or if index
        /// is greater than the size of array.</exception>
        /// <exception cref="ArgumentException">Thrown if the number of items in the list is greater than the
        /// available space in array.</exception>
        public void CopyTo(T[] array, int index )
        {
            if ( array == null )
                throw new ArgumentNullException( "array", "array cannot be null" );

            if ( ( index < 0 ) || ( index >= array.Length ) )
                throw new ArgumentOutOfRangeException( "index", index, "index must be greater than zero and less than the size of array" );

            if ( ( array.Length - index ) < Count )
                throw new ArgumentException( "array is not large enough to store the list", "array" );

            IEnumerator<T> enumerator = GetEnumerator();
            while ( enumerator.MoveNext() )
                array.SetValue( enumerator.Current, index++ );
        }

        /// <summary>Gets a value indicating whether the List is read-only.</summary>
        public bool IsReadOnly => false;

        /// <summary>Gets or sets the element at the specified index.</summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if index is less than zero or greater than
        /// the size of the list.</exception>
        public T this[int index]
        {
            get { return GetNodeAtIndex( index ).Value;  }
            set { GetNodeAtIndex( index ).Value = value; }
        }

        /// <summary>Adds a new value onto the end of the list.</summary>
        /// <param name="value">New value to add.</param>
        public void Add( T value )
        {
            if ( Head == null )
            {
                // Empty List
                Head = new Node<T>( value );
                Tail = Head;
            }
            else
            {
                Node<T> nodeNew = new Node<T>( value );
                Tail.Next = nodeNew;
                nodeNew.Previous = Tail;
                Tail = nodeNew;
            }
            // Either way increment the count
            Count++;
        }

        /// <summary>Adds a new value at the specified position.</summary>
        /// <param name="index">The zero-based index at which value should be inserted.</param>
        /// <param name="value">New value to insert.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if index is less than zero or greater than
        /// the size of the list.</exception>
        public void Insert( int index, T value )
        {
            Insert( GetNodeAtIndex( index ), value );
        }

        /// <summary>Removes the first occurance of the given value from the list.</summary>
        /// <param name="value">Value to remove.</param>
        public bool Remove( T value )
        {
            return Remove( value, false );
        }

        /// <summary>Removes the node at the specified position from the list.</summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if index is less than zero or greater than
        /// the size of the list.</exception>
        public void RemoveAt( int index )
        {
            RemoveAt( GetNodeAtIndex( index ) );
        }

        /// <summary>Determines the index of a specific item in the List.</summary>
        /// <param name="value">The object to locate in the list.</param>
        /// <returns>The zero-based index of the object in the list, or -1 if its not in the list.</returns>
        public int IndexOf( T value )
        {
            IEnumerator enumerator = new ForwardEnumerator<T>( this );
            int index = -1;

            while ( enumerator.MoveNext() )
            {
                index++;
                if ( object.Equals( enumerator.Current, value ) )
                    return index;
            }

            return -1;  // Didn't find it
        }

        /// <summary>Determines whether the List contains a specific value.</summary>
        /// <param name="value">The object to locate in the list.</param>
        /// <returns>True if value is in the list, false otherwise.</returns>
        public bool Contains( T value )
        {
            return ( IndexOf( value ) >= 0 );
        }

        /// <summary>Removes all items from the list.</summary>
        public void Clear()
        {
            // Gotta love garbage collection
            Head = null;
            Tail = null;
            Count   = 0;
        }

        /// <summary>Removes the first occurance of the given value from the list.</summary>
        /// <param name="value">Value to remove.</param>
        /// <param name="reverse">True to start at the end of the list.</param>
        public bool Remove( T value, bool reverse )
        {
            ListEnumerator<T> enumerator;
            if ( reverse )
                enumerator = new ReverseEnumerator<T>( this );
            else
                enumerator = new ForwardEnumerator<T>( this );

            while ( enumerator.MoveNext() )
            {
                Node<T> node = (Node<T>)enumerator.CurrentNode;
                if ( object.Equals( node.Value, value ) )
                {
                    RemoveAt( enumerator.CurrentNode );
                    return true;
                }
            }

            return false;
        }

        /// <summary>Adds a new value before or after the passed in position.</summary>
        /// <param name="node">Insertion point.</param>
        /// <param name="value">New value to add.</param>
        /// <param name="insertBefore">True to insert before node, False to insert after. Default is after (False)</param>
        /// <exception cref="ArgumentNullException">Thrown if node is null.</exception>
        public void Insert( Node<T> node, T value, bool insertBefore = false)
        {
            if ( node == null )
                throw new ArgumentNullException( "node", "node cannot be null" );

            Node<T> newNode = new Node<T>( value );

            if (insertBefore)
            {
                if (node.Previous != null)
                    node = node.Previous;
                else
                {
                    // Special Case for inserting a new Head
                    newNode.Next = Head;
                    Head = newNode;
                    Count++;
                    return;
                }
            }

            // Add the appropriate references for the newNode
            newNode.Previous = node.Previous;
            newNode.Next = node;

            // Make sure that the nodes around newNode have correct references.
            // If insertPoint.Prev is null, then this is the head of the list
            if ( node.Previous != null )
                node.Previous.Next = newNode;
            else
                Head = newNode;

            node.Previous = newNode;

            // Increment the count
            Count++;
        }

        /// <summary>Removes the specified node from the list.</summary>
        /// <param name="node">Node to remove</param>
        /// <exception cref="ArgumentNullException">Thrown if node is null.</exception>
        public void RemoveAt( Node<T> node )
        {
            if ( node == null )
                throw new ArgumentNullException( "node", "node cannot be null" );

            // Rebuild the previous node... if its null then I'm removing the head of the list
            if ( node.Previous != null )
                node.Previous.Next = node.Next;
            else
                Head = node.Next;

            // Rebuild the next node... if its null then I'm removing the tail of the list
            if ( node.Next != null )
                node.Next.Previous = node.Previous;
            else
                Tail = node.Previous;

            Count--;
        }

        private Node<T> GetNodeAtIndex( int index )
        {
            if ( ( index < 0 ) || ( index >= Count ) )
                throw new ArgumentOutOfRangeException( "index", index, "Index must be greater than zero and less than the size of the list" );

            Node<T> current = Head;
            while ( index-- > 0 )
                current = current.Next;

            return current;
        }
    }
}