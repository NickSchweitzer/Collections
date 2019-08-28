using System;

namespace TheCodingMonkey.Collections.Lists
{
    /// <summary>Forward Linked List Enumerator.</summary>
    public class ForwardEnumerator<T> : ListEnumerator<T>
    {
        /// <summary>Standard Constructor.</summary>
        /// <param name="list">List to Enumerate.</param>
        public ForwardEnumerator(LinkedList<T> list)
        : base(list)
        { }

        /// <summary>Moves to the next item in the list.</summary>
        public override bool MoveNext()
        {
            if (isReset && CurrentNode == null)
            {
                CurrentNode = linkedList.Head;
                isReset = false;
            }
            else
                CurrentNode = CurrentNode.Next;

            return (CurrentNode != null);
        }

        /// <summary>Moves to the previous element in the list</summary>
        public override bool MovePrevious()
        {
            if (!isReset && CurrentNode == null)    // At the end
                CurrentNode = linkedList.Tail;
            else
                CurrentNode = CurrentNode.Previous;

            return (CurrentNode != null);
        }
    }
}