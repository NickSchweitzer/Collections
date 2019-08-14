using System;
using System.Collections;

namespace TheCodingMonkey.Collections.Lists
{
    /// <summary>Base class for all LinkedList Enumerators.</summary>
    public abstract class ListEnumerator<T> : IBiDirEnumerator<T>, IDisposable
    {
        /// <summary>Linked list being enumerated.</summary>
        protected LinkedList<T> linkedList;

        /// <summary>True if enumerator has been reset, and Move has not been called yet.</summary>
        protected bool isReset;

        /// <summary>Standard Constructor.</summary>
        /// <param name="list">List to Enumerate.</param>
        public ListEnumerator(LinkedList<T> list )
        {
            linkedList = list;
            Reset();
        }

        /// <summary>Current Value of the Enumerator.</summary>
        public Node<T> CurrentNode { get; protected set; }

        /// <summary>Releases any Resources used by this object.</summary>
        public void Dispose()
        {
            linkedList = null;
        }

        /// <summary>Resets the Enumerator.</summary>
        public void Reset()
        {
            CurrentNode = null;
            isReset = true;
        }

        /// <summary>Current Value of the Enumerator.</summary>
        public T Current
        {
            get { return CurrentNode.Value; }
        }

        /// <summary>Current Value of the Enumerator.</summary>
        object IEnumerator.Current
        {
            get { return CurrentNode.Value; }
        }

        /// <summary>Moves to the next item in the list.</summary>
        public abstract bool MoveNext();
        /// <summary>Moves to the previous element in the list</summary>
        public abstract bool MovePrevious();
    }
}