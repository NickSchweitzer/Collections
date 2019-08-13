using System;

namespace TheCodingMonkey.Collections.Lists
{
    /// <summary>Forward Linked List Enumerator.</summary>
    public class ForwardEnumerator : ListEnumerator
    {
        /// <summary>Standard Constructor.</summary>
        /// <param name="list">List to Enumerate.</param>
        public ForwardEnumerator(LinkedList list)
        : base(list)
        { }

        /// <summary>Moves to the next item in the list.</summary>
        public override bool MoveNext()
        {
            if (m_bReset && m_nodeCurrent == null)
            {
                m_nodeCurrent = m_LinkedList.Head;
                m_bReset = false;
            }
            else
                m_nodeCurrent = m_nodeCurrent.Next;

            return (m_nodeCurrent != null);
        }

        /// <summary>Moves to the previous element in the list</summary>
        public override bool MovePrev()
        {
            m_nodeCurrent = m_nodeCurrent.Prev;
            return (m_nodeCurrent != null);
        }
    }
}