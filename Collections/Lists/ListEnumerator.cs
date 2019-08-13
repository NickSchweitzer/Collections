using System;

namespace TheCodingMonkey.Collections.Lists
{
    /// <summary>Base class for all LinkedList Enumerators.</summary>
    public abstract class ListEnumerator : IBiDirEnumerator, IDisposable
    {
        /// <summary>Linked list being enumerated.</summary>
        protected LinkedList m_LinkedList;
        /// <summary>Current node being pointed to.</summary>
        protected Node m_nodeCurrent;
        /// <summary>True if enumerator has been reset, and Move has not been called yet.</summary>
        protected bool m_bReset;

        /// <summary>Standard Constructor.</summary>
        /// <param name="list">List to Enumerate.</param>
        public ListEnumerator( LinkedList list )
        {
            m_LinkedList  = list;
            Reset();
        }

        /// <summary>Current Value of the Enumerator.</summary>
        public Node CurrentNode
        {
            get { return m_nodeCurrent; }
        }

        /// <summary>Releases any Resources used by this object.</summary>
        public void Dispose()
        {
            m_LinkedList = null;
        }

        /// <summary>Resets the Enumerator.</summary>
        public void Reset()
        {
            m_nodeCurrent = null;
            m_bReset      = true;
        }

        /// <summary>Current Value of the Enumerator.</summary>
        public object Current
        {
            get { return m_nodeCurrent.Value; }
        }

        /// <summary>Moves to the next item in the list.</summary>
        public abstract bool MoveNext();
        /// <summary>Moves to the previous element in the list</summary>
        public abstract bool MovePrev();
    }
}