using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheCodingMonkey.Collections.Lists;

namespace TheCodingMonkey.Collections.Tests
{
	/// <summary>Test Fixture for the LinkedList class.</summary>
    [TestClass, TestCategory("Linked List")]
    public class LinkedListTests
	{
        private LinkedList m_LinkedList;

        [TestInitialize]
        public void Init()
        {
            m_LinkedList = new LinkedList();
        }

        protected void AddToEmpty( int nCount )
        {
            AddToEmpty( nCount, false );
        }

        protected void AddToEmpty( int nCount, bool bMultiply )
        {
            Assert.IsTrue( m_LinkedList.Empty );

            for ( int i = 1; i <= nCount; i++ )
                m_LinkedList.Add( bMultiply ? i*1000 : i );

            Assert.AreEqual( nCount, m_LinkedList.Count );
        }

        protected LinkedList MakeClone()
        {
            LinkedList cloned = (LinkedList)m_LinkedList.Clone();

            // Make sure of the basics... that they aren't pointing to the same references
            Assert.IsFalse( object.ReferenceEquals( m_LinkedList, cloned ) );

            // Check that the sizes match... duh
            Assert.AreEqual( m_LinkedList.Count, cloned.Count );

            return cloned;
        }

        [TestMethod]
        public void AddItems()
        {
            AddToEmpty( 10 );

            // Verify the Forward Iterator
            int iTest = 1;
            foreach ( int iList in m_LinkedList )
            {
                Assert.AreEqual( iList, iTest );
                iTest++;
            }

            // Verify the Reverse Iterator
            iTest = 10;
            IEnumerator enumerator = new ReverseEnumerator( m_LinkedList );
            while ( enumerator.MoveNext() )
            {
                int iList = (int)enumerator.Current;
                Assert.AreEqual( iTest, iList );
                iTest--;
            }
        }

        [TestMethod]
        public void InsertItemsAtIndex()
        {
            AddToEmpty( 10 );

            // Insert 10 items at the front of the list
            for ( int i = 20; i >= 11; i-- )
                m_LinkedList.Insert( 0, i );

            Assert.AreEqual( 20, m_LinkedList.Count );

            // Verify the first 10 items are the inserted items
            int iTest = 11;
            IEnumerator enumerator = m_LinkedList.GetEnumerator();
            while ( iTest <= 20 )
            {
                enumerator.MoveNext();
                int iList = (int)enumerator.Current;
                Assert.AreEqual( iTest, iList );
                iTest++;
            }

            // Verify that the last 10 items are the ones that were added originally
            iTest = 10;
            enumerator = new ReverseEnumerator( m_LinkedList );
            while ( iTest >= 1 )
            {
                enumerator.MoveNext();
                int iList = (int)enumerator.Current;
                Assert.AreEqual( iTest, iList );
                iTest--;
            }
        }

        [TestMethod]
        public void RemoveItems()
        {
            AddToEmpty( 20 );

            // Remove all the even numbers
            for ( int i = 0; i <= 20; i += 2 )
                m_LinkedList.Remove( i );

            Assert.AreEqual( 10, m_LinkedList.Count );

            // Verify that the remaining items are even
            int iTest = 1;
            foreach ( int iList in m_LinkedList )
            {
                Assert.AreEqual( iTest, iList );
                iTest +=2;
            }

            // Verify that clear works
            m_LinkedList.Clear();

            // Verify that Empty is now true
            Assert.IsTrue( m_LinkedList.Empty );

            // Make sure the iterator thinks its empty
            IEnumerator enumerator = m_LinkedList.GetEnumerator();
            Assert.IsFalse( enumerator.MoveNext() );

            // Finally try to remove from an empty list
            m_LinkedList.Remove( 1 );
        }

        [TestMethod]
        public void RemoveItemsAtIndex()
        {
            AddToEmpty( 20, true );

            // Remove all the even numbers
            for ( int i = 1; i <= 10; i++ )
                m_LinkedList.RemoveAt( i );

            Assert.AreEqual( 10, m_LinkedList.Count );

            // Verify that the remaining items are odd
            int iTest = 1000;
            foreach ( int iList in m_LinkedList )
            {
                Assert.AreEqual( iTest, iList );
                iTest += 2000;
            }
        }

        [TestMethod, ExpectedException( typeof(System.ArgumentOutOfRangeException) )]
        public void RemoveAtEmpty()
        {
            AddToEmpty( 20 );
            m_LinkedList.Clear();
            Assert.IsTrue( m_LinkedList.Empty );
            m_LinkedList.RemoveAt( 1 );
        }

        [TestMethod, ExpectedException( typeof( System.ArgumentOutOfRangeException) )]
        public void RemoveAtOutOfRange()
        {
            AddToEmpty( 10 );
            m_LinkedList.RemoveAt( 11 );
        }

        [TestMethod, ExpectedException( typeof( System.ArgumentOutOfRangeException) )]
        public void RemoveAtNegative()
        {
            AddToEmpty( 10 );
            m_LinkedList.RemoveAt( -1 );
        }

        [TestMethod]
        public void Contains()
        {
            AddToEmpty( 25 );

            for ( int i = 1; i <= 25; i++ )
                Assert.IsTrue( m_LinkedList.Contains( i ) );

            Random rand = new Random();
            int nValue  = rand.Next( 26, int.MaxValue );
            Assert.IsFalse( m_LinkedList.Contains( nValue ) );
        }

        [TestMethod]
        public void IndexOf()
        {
            AddToEmpty( 25, true );

            for ( int i = 0; i < 25; i++ )
                Assert.AreEqual( i, m_LinkedList.IndexOf( (i+1)*1000 ) );

            Random rand = new Random();
            int nValue  = rand.Next( 26, int.MaxValue );
            Assert.AreEqual( -1, m_LinkedList.IndexOf( nValue ) );
        }

        [TestMethod, ExpectedException( typeof(System.ArgumentOutOfRangeException) )]
        public void IndexerTest()
        {
            AddToEmpty( 25, true );

            // Check to make sure the indexer works for all entries in the dictionary
            for ( int i = 0; i < 25; i++ )
                Assert.AreEqual( (i+1)*1000, m_LinkedList[i] );

            int nValue = (int)m_LinkedList[50];
        }

        [TestMethod, ExpectedException( typeof(System.ArgumentOutOfRangeException) )]
        public void IndexerNegativeTest()
        {
            AddToEmpty( 25 );

            // Make sure that null throws the proper exception
            int nValue = (int)m_LinkedList[-1];
        }

        [TestMethod, ExpectedException( typeof(System.ArgumentOutOfRangeException) )]
        public void IndexerEmptyTest()
        {
            // Make sure that empty dictionary throws the proper exception
            int nValue = (int)m_LinkedList[1];
        }

        [TestMethod]
        public void CopyTo()
        {
            AddToEmpty( 25 );

            // Copy all the elements to an array
            int[] array = new int[m_LinkedList.Count];
            m_LinkedList.CopyTo( array, 0 );
            Assert.AreEqual( m_LinkedList.Count, array.Length );

            int i = 0;
            foreach( int n in m_LinkedList )
            {
                // Make sure tha that the array matches what I added
                // And make sure that the array matches the enumerator
                Assert.AreEqual( i+1, array[i] );
                Assert.AreEqual( n, i+1 );
                i++;
            }
        }

        [TestMethod]
        public void CopyToOffset()
        {
            Assert.IsTrue( m_LinkedList.Empty );

            // Fill up my dictionary with the upper half of what I want in the array
            for ( int i = 5; i < 10; i++ )
                m_LinkedList.Add( i );

            Assert.AreEqual( 5, m_LinkedList.Count );

            int[] array = new int[10];

            // Pre-fill the array with the lower half of what I want in there
            for ( int i = 0; i < 5; i++ )
                array[i] = i;

            // Fill in the upper half with bad values.  These should get overwritten by CopyTo
            for ( int i = 5; i < 10; i++ )
                array[i] = i*100;

            // Do the CopyTo the upper half of the array
            m_LinkedList.CopyTo( array, 5 );

            // Verify that the elements were overwritten
            for ( int i = 0; i < 10; i++ )
            {
                // Make sure tha that the array matches what I added
                Assert.AreEqual( i, array[i] );
            }
        }

        [TestMethod, ExpectedException( typeof(System.ArgumentNullException) )]
        public void CopyToNull()
        {
            AddToEmpty( 10 );

            m_LinkedList.CopyTo( null, 0 );
        }

        [TestMethod, ExpectedException( typeof(System.ArgumentException) )]
        public void CopyToSmall()
        {
            AddToEmpty( 25 );

            int[] array = new int[10];
            m_LinkedList.CopyTo( array, 0 );
        }

        [TestMethod, ExpectedException( typeof(System.ArgumentOutOfRangeException) )]
        public void CopyToNegative()
        {
            AddToEmpty( 10 );

            int[] array = new int[m_LinkedList.Count];
            m_LinkedList.CopyTo( array, -1 );
        }

        [TestMethod, ExpectedException( typeof(System.ArgumentException) )]
        public void CopyToBadOffset()
        {
            AddToEmpty( 10 );

            int[] array = new int[m_LinkedList.Count];
            m_LinkedList.CopyTo( array, 1 );
        }

        [TestMethod, ExpectedException( typeof(System.ArgumentOutOfRangeException) )]
        public void CopyToOutOfRange()
        {
            AddToEmpty( 10 );

            int[] array = new int[m_LinkedList.Count];
            m_LinkedList.CopyTo( array, 11 );
        }

        [TestMethod]
        public void InsertItemsAtEnumerator()
        {
            AddToEmpty( 10 );

            // Insert 10 items at the front of the list
            IEnumerator head = m_LinkedList.GetEnumerator();
            for ( int i = 20; i >= 11; i-- )
            {
                head.Reset();
                head.MoveNext();
                m_LinkedList.Insert( head, i );
            }
            Assert.AreEqual( 20, m_LinkedList.Count );

            // Verify the first 10 items are the inserted items
            int iTest = 11;
            IEnumerator enumerator = m_LinkedList.GetEnumerator();
            while ( iTest <= 20 )
            {
                enumerator.MoveNext();
                int iList = (int)enumerator.Current;
                Assert.AreEqual( iTest, iList );
                iTest++;
            }

            // Verify that the last 10 items are the ones that were added originally
            iTest = 10;
            enumerator = new ReverseEnumerator( m_LinkedList );
            while ( iTest >= 1 )
            {
                enumerator.MoveNext();
                int iList = (int)enumerator.Current;
                Assert.AreEqual( iTest, iList );
                iTest--;
            }
        }

        [TestMethod, ExpectedException( typeof(System.ArgumentNullException) )]
        public void RemoveItemsAtEnumerator()
        {
            AddToEmpty( 20, true );

            // Remove the first 10 items from the list
            IEnumerator enumerator = m_LinkedList.GetEnumerator();
            for ( int i = 0; i < 10; i++ )
            {
                enumerator.Reset();
                enumerator.MoveNext();
                m_LinkedList.RemoveAt( enumerator );
            }

            Assert.AreEqual( 10, m_LinkedList.Count );

            // Verify that the only remaining items are the last 10
            int iTest = 11000;
            foreach ( int iList in m_LinkedList )
            {
                Assert.AreEqual( iTest, iList );
                iTest += 1000;
            }

            // Clear the list
            m_LinkedList.Clear();

            // Finally try to remove from an empty list
            m_LinkedList.RemoveAt( null );
        }

        [TestMethod]
        public void CloneValueTest()
        {
            AddToEmpty( 25, true );

            LinkedList cloned = MakeClone();

            IEnumerator enumOrig  = m_LinkedList.GetEnumerator();
            IEnumerator enumClone = cloned.GetEnumerator();
            while ( enumOrig.MoveNext() && enumClone.MoveNext() )
            {
                // Values point to same objects and are equal
                Assert.IsTrue( object.ReferenceEquals( enumOrig.Current, enumClone.Current ) );
                Assert.AreEqual( enumOrig.Current, enumClone.Current );
            }
        }

        [TestMethod]
        public void CloneObjectTest()
        {
            Assert.AreEqual( 0, m_LinkedList.Count );

            for ( int i = 1; i <= 25; i++ )
                m_LinkedList.Add( new CloneableInt( i * 1000 ) );

            Assert.AreEqual( 25, m_LinkedList.Count );

            LinkedList cloned = MakeClone();

            IEnumerator enumOrig  = m_LinkedList.GetEnumerator();
            IEnumerator enumClone = cloned.GetEnumerator();
            while ( enumOrig.MoveNext() && enumClone.MoveNext() )
            {
                // Values point to different objects but are equal
                Assert.IsFalse( object.ReferenceEquals( enumOrig.Current, enumClone.Current ) );
                Assert.AreEqual( enumOrig.Current, enumClone.Current );
            }
        }

        [TestMethod]
        public void CloneEmptyTest()
        {
            LinkedList cloned = MakeClone();
        }
	}
}