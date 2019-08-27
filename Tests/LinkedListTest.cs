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
        private LinkedList<int> testList;

        [TestInitialize]
        public void Init()
        {
            testList = new LinkedList<int>();
        }

        [TestMethod]
        public void BasedPropertiesTest()
        {
            AddToEmpty(10);
            Assert.IsFalse(testList.IsReadOnly);
        }

        protected void AddToEmpty( int nCount )
        {
            AddToEmpty( nCount, false );
        }

        protected void AddToEmpty( int nCount, bool bMultiply )
        {
            Assert.IsTrue( testList.Empty );

            for ( int i = 1; i <= nCount; i++ )
                testList.Add( bMultiply ? i*1000 : i );

            Assert.AreEqual( nCount, testList.Count );
        }

        protected LinkedList<int> MakeClone()
        {
            LinkedList<int> cloned = (LinkedList<int>)testList.Clone();

            // Make sure of the basics... that they aren't pointing to the same references
            Assert.IsFalse( object.ReferenceEquals( testList, cloned ) );

            // Check that the sizes match... duh
            Assert.AreEqual( testList.Count, cloned.Count );

            return cloned;
        }

        [TestMethod]
        public void AddItems()
        {
            AddToEmpty( 10 );

            // Verify the Forward Iterator
            int iTest = 1;
            foreach ( int iList in testList )
            {
                Assert.AreEqual( iList, iTest );
                iTest++;
            }

            // Verify the Reverse Iterator
            iTest = 10;
            IEnumerator enumerator = new ReverseEnumerator<int>( testList );
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
                testList.Insert( 0, i );

            Assert.AreEqual( 20, testList.Count );

            // Verify the first 10 items are the inserted items
            int iTest = 11;
            IEnumerator enumerator = testList.GetEnumerator();
            while ( iTest <= 20 )
            {
                enumerator.MoveNext();
                int iList = (int)enumerator.Current;
                Assert.AreEqual( iTest, iList );
                iTest++;
            }

            // Verify that the last 10 items are the ones that were added originally
            iTest = 10;
            enumerator = new ReverseEnumerator<int>( testList );
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
                testList.Remove( i );

            Assert.AreEqual( 10, testList.Count );

            // Verify that the remaining items are even
            int iTest = 1;
            foreach ( int iList in testList )
            {
                Assert.AreEqual( iTest, iList );
                iTest +=2;
            }

            // Verify that clear works
            testList.Clear();

            // Verify that Empty is now true
            Assert.IsTrue( testList.Empty );

            // Make sure the iterator thinks its empty
            IEnumerator enumerator = testList.GetEnumerator();
            Assert.IsFalse( enumerator.MoveNext() );

            // Finally try to remove from an empty list
            testList.Remove( 1 );
        }

        [TestMethod]
        public void RemoveItemsAtIndex()
        {
            AddToEmpty( 20, true );

            // Remove all the even numbers
            for ( int i = 1; i <= 10; i++ )
                testList.RemoveAt( i );

            Assert.AreEqual( 10, testList.Count );

            // Verify that the remaining items are odd
            int iTest = 1000;
            foreach ( int iList in testList )
            {
                Assert.AreEqual( iTest, iList );
                iTest += 2000;
            }
        }

        [TestMethod, ExpectedException( typeof(System.ArgumentOutOfRangeException) )]
        public void RemoveAtEmpty()
        {
            AddToEmpty( 20 );
            testList.Clear();
            Assert.IsTrue( testList.Empty );
            testList.RemoveAt( 1 );
        }

        [TestMethod, ExpectedException( typeof( System.ArgumentOutOfRangeException) )]
        public void RemoveAtOutOfRange()
        {
            AddToEmpty( 10 );
            testList.RemoveAt( 11 );
        }

        [TestMethod, ExpectedException( typeof( System.ArgumentOutOfRangeException) )]
        public void RemoveAtNegative()
        {
            AddToEmpty( 10 );
            testList.RemoveAt( -1 );
        }

        [TestMethod]
        public void Contains()
        {
            AddToEmpty( 25 );

            for ( int i = 1; i <= 25; i++ )
                Assert.IsTrue( testList.Contains( i ) );

            Random rand = new Random();
            int nValue  = rand.Next( 26, int.MaxValue );
            Assert.IsFalse( testList.Contains( nValue ) );
        }

        [TestMethod]
        public void IndexOf()
        {
            AddToEmpty( 25, true );

            for ( int i = 0; i < 25; i++ )
                Assert.AreEqual( i, testList.IndexOf( (i+1)*1000 ) );

            Random rand = new Random();
            int nValue  = rand.Next( 26, int.MaxValue );
            Assert.AreEqual( -1, testList.IndexOf( nValue ) );
        }

        [TestMethod, ExpectedException( typeof(System.ArgumentOutOfRangeException) )]
        public void IndexerTest()
        {
            AddToEmpty( 25, true );

            // Check to make sure the indexer works for all entries in the dictionary
            for ( int i = 0; i < 25; i++ )
                Assert.AreEqual( (i+1)*1000, testList[i] );

            int nValue = testList[50];
        }

        [TestMethod, ExpectedException( typeof(System.ArgumentOutOfRangeException) )]
        public void IndexerNegativeTest()
        {
            AddToEmpty( 25 );

            // Make sure that null throws the proper exception
            int nValue = (int)testList[-1];
        }

        [TestMethod, ExpectedException( typeof(System.ArgumentOutOfRangeException) )]
        public void IndexerEmptyTest()
        {
            // Make sure that empty dictionary throws the proper exception
            int nValue = testList[1];
        }

        [TestMethod]
        public void CopyTo()
        {
            AddToEmpty( 25 );

            // Copy all the elements to an array
            int[] array = new int[testList.Count];
            testList.CopyTo( array, 0 );
            Assert.AreEqual( testList.Count, array.Length );

            int i = 0;
            foreach( int n in testList )
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
            Assert.IsTrue( testList.Empty );

            // Fill up my dictionary with the upper half of what I want in the array
            for ( int i = 5; i < 10; i++ )
                testList.Add( i );

            Assert.AreEqual( 5, testList.Count );

            int[] array = new int[10];

            // Pre-fill the array with the lower half of what I want in there
            for ( int i = 0; i < 5; i++ )
                array[i] = i;

            // Fill in the upper half with bad values.  These should get overwritten by CopyTo
            for ( int i = 5; i < 10; i++ )
                array[i] = i*100;

            // Do the CopyTo the upper half of the array
            testList.CopyTo( array, 5 );

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

            testList.CopyTo( null, 0 );
        }

        [TestMethod, ExpectedException( typeof(System.ArgumentException) )]
        public void CopyToSmall()
        {
            AddToEmpty( 25 );

            int[] array = new int[10];
            testList.CopyTo( array, 0 );
        }

        [TestMethod, ExpectedException( typeof(System.ArgumentOutOfRangeException) )]
        public void CopyToNegative()
        {
            AddToEmpty( 10 );

            int[] array = new int[testList.Count];
            testList.CopyTo( array, -1 );
        }

        [TestMethod, ExpectedException( typeof(System.ArgumentException) )]
        public void CopyToBadOffset()
        {
            AddToEmpty( 10 );

            int[] array = new int[testList.Count];
            testList.CopyTo( array, 1 );
        }

        [TestMethod, ExpectedException( typeof(System.ArgumentOutOfRangeException) )]
        public void CopyToOutOfRange()
        {
            AddToEmpty( 10 );

            int[] array = new int[testList.Count];
            testList.CopyTo( array, 11 );
        }

        [TestMethod]
        public void InsertItemsAtNode()
        {
            AddToEmpty( 10 );

            // Insert 10 items at the front of the list
            for ( int i = 20; i >= 11; i-- )
                testList.Insert( testList.Head, i );

            Assert.AreEqual( 20, testList.Count );

            // Verify the first 10 items are the inserted items
            int iTest = 11;
            IEnumerator enumerator = testList.GetEnumerator();
            while ( iTest <= 20 )
            {
                enumerator.MoveNext();
                int iList = (int)enumerator.Current;
                Assert.AreEqual( iTest, iList );
                iTest++;
            }

            // Verify that the last 10 items are the ones that were added originally
            iTest = 10;
            enumerator = new ReverseEnumerator<int>( testList );
            while ( iTest >= 1 )
            {
                enumerator.MoveNext();
                int iList = (int)enumerator.Current;
                Assert.AreEqual( iTest, iList );
                iTest--;
            }
        }

        [TestMethod]
        public void InsertItemsBeforeNode()
        {
            Assert.IsTrue(testList.Empty);
            testList.Add(10);
            Assert.AreEqual(1, testList.Count);

            // Insert 10 items at the front of the list
            for (int i = 9; i > 0; i--)
                testList.Insert(testList.Head, i, true);

            Assert.AreEqual(10, testList.Count);

            // Verify the list is 1 - 10 in order
            int iTest = 1;
            var enumerator = testList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Assert.AreEqual(iTest, enumerator.Current);
                iTest++;
            }
        }

        [TestMethod, ExpectedException( typeof(System.ArgumentNullException) )]
        public void RemoveItemsAtNode()
        {
            AddToEmpty( 20, true );

            // Remove the first 10 items from the list
            for ( int i = 0; i < 10; i++ )
                testList.RemoveAt( testList.Head );

            Assert.AreEqual( 10, testList.Count );

            // Verify that the only remaining items are the last 10
            int iTest = 11000;
            foreach ( int iList in testList )
            {
                Assert.AreEqual( iTest, iList );
                iTest += 1000;
            }

            // Clear the list
            testList.Clear();

            // Finally try to remove from an empty list
            testList.RemoveAt( null );
        }

        [TestMethod]
        public void CloneValueTest()
        {
            AddToEmpty( 25, true );

            LinkedList<int> cloned = MakeClone();

            IEnumerator enumOrig  = testList.GetEnumerator();
            IEnumerator enumClone = cloned.GetEnumerator();
            while ( enumOrig.MoveNext() && enumClone.MoveNext() )
            {
                // Values point to different objects (ValueTypes) and are equal
                Assert.IsFalse( object.ReferenceEquals( enumOrig.Current, enumClone.Current ) );
                Assert.AreEqual( enumOrig.Current, enumClone.Current );
            }
        }

        [TestMethod]
        public void CloneObjectTest()
        {
            LinkedList<CloneableInt> referenceList = new LinkedList<CloneableInt>();
            Assert.AreEqual( 0, referenceList.Count );

            for ( int i = 1; i <= 25; i++ )
                referenceList.Add( new CloneableInt( i * 1000 ) );

            Assert.AreEqual( 25, referenceList.Count );

            LinkedList<CloneableInt> cloned = (LinkedList<CloneableInt>)referenceList.Clone();

            // Make sure of the basics... that they aren't pointing to the same references
            Assert.IsFalse(object.ReferenceEquals(referenceList, cloned));

            // Check that the sizes match... duh
            Assert.AreEqual(referenceList.Count, cloned.Count);

            IEnumerator enumOrig  = referenceList.GetEnumerator();
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
            LinkedList<int> cloned = MakeClone();
            Assert.AreNotEqual(cloned, testList);
            Assert.AreEqual(cloned.Count, testList.Count);
        }
    }
}