using System;

namespace TheCodingMonkey.Collections.Lists
{
    /// <summary>Reverse Linked List Enumerator.</summary>
    public class ReverseEnumerator<T> : ListEnumerator<T>
    {
        /// <summary>Standard Constructor.</summary>
        /// <param name="list">List to Enumerate.</param>
        public ReverseEnumerator(LinkedList<T> list)
        : base(list)
        { }

        /// <summary>Moves to the next item in the list.</summary>
        public override bool MoveNext()
        {
            if (isReset && CurrentNode == null)
            {
                CurrentNode = linkedList.Tail;
                isReset = false;
            }
            else
                CurrentNode = CurrentNode.Previous;

            return (CurrentNode != null);
        }

        /// <summary>Moves to the previous element in the list</summary>
        public override bool MovePrevious()
        {
            CurrentNode = CurrentNode.Next;
            return (CurrentNode != null);
        }
    }
}