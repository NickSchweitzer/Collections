// Written by Eric Gunnerson
// Downloaded from GotDotNet.com

using System;
using System.Collections;

namespace TheCodingMonkey.Collections.Iterators
{
	/// <summary>Isolate this the iteration from the collection. Allows you to modify the underlying 
	/// collection while in the middle of a foreach loop.</summary>
    public class IterIsolate : IEnumerable
    {
        /// <summary>Create an instance of the IterIsolate Class.</summary>
        /// <param name="enumerable">A class that implements IEnumerable</param>
        public IterIsolate( IEnumerable enumerable )
        {
            m_pEnumerable = enumerable;
        }

        /// <summary>Returns an isolated iterator for the given enumerable object.</summary>
        public IEnumerator GetEnumerator()
        {
            return new IterIsolateEnumerator( m_pEnumerable.GetEnumerator() );
        }

        protected IEnumerable m_pEnumerable;

        /// <summary>Helper Class used for Enumeration</summary>
        internal class IterIsolateEnumerator : IEnumerator
        {
            protected ArrayList m_lstItems;
            protected int       m_nCurrentItem;

            /// <summary>Constructor</summary>
            /// <param name="enumerator">Enumerator to Isolate</param>
            internal IterIsolateEnumerator( IEnumerator enumerator )
            {
                // if this is the enumerator from another iterator, we don't have to enumerate it; we'll just 
                // steal the arraylist to use for ourselves. 
                IterIsolateEnumerator m_pChainedEnumerator = enumerator as IterIsolateEnumerator;

                if ( m_pChainedEnumerator != null )
                    m_lstItems = m_pChainedEnumerator.m_lstItems;				
                else
                {
                    m_lstItems = new ArrayList();
                    while ( enumerator.MoveNext() )
                        m_lstItems.Add(enumerator.Current);

                    IDisposable disposable = enumerator as IDisposable;
                    if ( disposable != null )
                        disposable.Dispose();
                }
                m_nCurrentItem = -1;
            }

            #region IEnumerator Methods

            public void Reset()
            {
                m_nCurrentItem = -1;
            }

            public bool MoveNext()
            {
                return ( ++m_nCurrentItem != m_lstItems.Count );
            }

            public object Current
            {
                get { return m_lstItems[m_nCurrentItem]; }
            }

            #endregion
        }
    }
}