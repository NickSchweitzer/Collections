using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheCodingMonkey.Collections.Tests
{
    public abstract class DictionaryTest
    { 
        protected IDictionary<int, int> m_Dictionary;
        protected IDictionary<CloneableInt, CloneableInt> referenceDictionary;

        [TestInitialize]
        public abstract void Init();

        protected void AddToEmpty( int count )
        {
            AddToEmpty( count, false );
        }

        protected void AddToEmpty( int count, bool multiply )
        {
            Assert.AreEqual( 0, m_Dictionary.Count );

            // Add count items to the tree
            for ( int i = 1; i <= count; i++ )
                m_Dictionary.Add( i, multiply ? i*1000 : i );

            Assert.AreEqual( count, m_Dictionary.Count, "Count is not accurate" );
        }

        protected void AddRandomToEmpty( int count )
        {
            Random rand = new Random();
            Assert.AreEqual( 0, m_Dictionary.Count );

            // Add 1000 Random Nubers to the list
            for ( int i = 0; i < count; i++ )
            {
                int iAdd = rand.Next();
                try
                {
                    m_Dictionary.Add( iAdd, iAdd );
                }
                catch ( ArgumentException )
                {
                    i--;    // Duplicate random number, do one more
                }
            }

            Assert.AreEqual( count, m_Dictionary.Count );
        }

        protected IDictionary<int, int> MakeClone()
        {
            var cloneable = (ICloneable)m_Dictionary;
            IDictionary<int, int> cloned = (IDictionary<int, int>)cloneable.Clone();

            // Make sure of the basics... that they aren't pointing to the same references
            Assert.IsFalse( object.ReferenceEquals( m_Dictionary, cloned ) );

            // Check that the sizes match... duh
            Assert.AreEqual( m_Dictionary.Count, cloned.Count );

            return cloned;
        }

        [TestMethod]
        public void AddItems()
        {
            AddToEmpty( 10 );

            // Verify that they all got added in the proper order
            int iTest = 1;
            foreach( KeyValuePair<int, int> entry in m_Dictionary )
            {
                Assert.AreEqual( iTest, entry.Value);
                iTest++;
            }
        }

        [TestMethod]
        public void RemoveItems()
        {
            AddToEmpty( 20 );

            // Remove all the even numbers
            for ( int i = 2; i <= 20; i += 2 )
                m_Dictionary.Remove( i );

            Assert.AreEqual( 10, m_Dictionary.Count );

            // Verify that only odd numbers exist
            int iTest = 1;
            foreach ( KeyValuePair<int, int> entry in m_Dictionary )
            {
                Assert.AreEqual( iTest, entry.Value );
                iTest += 2;
            }

            // Clear the tree
            m_Dictionary.Clear();

            // Verify that its now empty
            Assert.AreEqual( 0, m_Dictionary.Count );

            // Make sure the iterator thinks its empty
            IEnumerator<KeyValuePair<int, int>> enumerator = m_Dictionary.GetEnumerator();
            Assert.IsFalse( enumerator.MoveNext() );
        }

        [TestMethod]
        public void AddRandomItems()
        {
            AddRandomToEmpty( 1000 );

            // Now verify that the inorder enumerator is returning them in order
            int  nPrevious = 0;
            bool bDoneOnce = false;

            foreach ( KeyValuePair<int, int> entry in m_Dictionary )
            {
                if ( !bDoneOnce )
                {
                    nPrevious = entry.Value;
                    bDoneOnce = true;
                }
                else
                {
                    Assert.IsTrue( nPrevious < entry.Value );
                    nPrevious = entry.Value;
                }
            }
        }

        [TestMethod, ExpectedException( typeof(ArgumentException) ) ]
        public void AddDuplicate()
        {
            AddToEmpty( 10 );

            // Attempt to re-add the number 5
            m_Dictionary.Add( 5, 5 );
        }

        [TestMethod]
        public void RemoveNonExistant()
        {
            // First try to remove from an empty list
            Assert.AreEqual( 0, m_Dictionary.Count );
            m_Dictionary.Remove( 1 );

            // Now add some items
            for ( int i = 1; i <= 10; i++ )
                m_Dictionary.Add( i, i );

            Assert.AreEqual( 10, m_Dictionary.Count );

            // Try to remove an item that's not in the list
            m_Dictionary.Remove( 11 );
            Assert.AreEqual( 10, m_Dictionary.Count );
        }

        [TestMethod, ExpectedException( typeof(ArgumentNullException) )]
        public void AddNullItem()
        {
            Assert.AreEqual( 0, referenceDictionary.Count );

            // Make sure adding a null exception throws the right exception
            referenceDictionary.Add( null, null );
        }

        [TestMethod, ExpectedException( typeof(ArgumentNullException) )]
        public void RemoveNullItem()
        {
            Assert.AreEqual(0, referenceDictionary.Count);

            for (int i = 1; i <= 10; i++)
                referenceDictionary.Add(new CloneableInt(i), new CloneableInt(i * 1000));

            // Make sure removing null throws the right exception
            referenceDictionary.Remove( null );
        }

        [TestMethod]
        public void ContainsItems()
        {
            // Make sure Contains returns false on an empty dictionary
            Assert.IsFalse( m_Dictionary.ContainsKey( 25 ) );

            AddToEmpty( 25, true );

            // Make sure Contains returns true for all items in the dictionary
            // and returns false for a bunch of items not in the dictionary
            for ( int i = 1; i <= 25; i++ )
            {
                Assert.IsTrue(  m_Dictionary.ContainsKey( i ) );
                Assert.IsFalse( m_Dictionary.ContainsKey( i*1000 ) );

                int tryGet;
                Assert.IsTrue(m_Dictionary.TryGetValue(i, out tryGet));
                Assert.AreEqual(i * 1000, tryGet);

                int wontGet;
                Assert.IsFalse(m_Dictionary.TryGetValue(i * 1000, out wontGet));
            }
        }

        [TestMethod]
        public void EnumerateKeys()
        {
            AddToEmpty( 25, true );

            // Get enumerators for everything
            ICollection<int> lstKeys  = m_Dictionary.Keys;
            IEnumerator<int> enumKeys = lstKeys.GetEnumerator();
            IEnumerator<KeyValuePair<int, int>> enumDict = m_Dictionary.GetEnumerator();

            Assert.AreEqual( 25, lstKeys.Count );
            for (int doTwice = 0; doTwice < 2; doTwice++)
            {
                int i = 1;
                while (enumKeys.MoveNext() && enumDict.MoveNext())
                {
                    // Make sure the keys list has everything in the right order
                    KeyValuePair<int, int> entry = enumDict.Current;
                    Assert.AreEqual(entry.Key, enumKeys.Current);
                    Assert.AreEqual(i++, enumKeys.Current);
                }
                enumKeys.Reset();
                enumDict.Reset();
            }
        }

        [TestMethod]
        public void EnumerateValues()
        {
            AddToEmpty( 25, true );

            // Get enumerators for everything
            int i = 1;
            ICollection<int> listKeys = m_Dictionary.Keys;
            IEnumerator<int> enumKeys = listKeys.GetEnumerator();
            IEnumerator<KeyValuePair<int, int>> enumDict = m_Dictionary.GetEnumerator();

            Assert.AreEqual( 25, listKeys.Count );
            while ( enumKeys.MoveNext() && enumDict.MoveNext() )
            {
                // Make sure the values list has everything in the right order
                KeyValuePair<int, int> entry = enumDict.Current;
                Assert.AreEqual(i, enumKeys.Current);
                Assert.AreEqual(i, entry.Key);
                Assert.AreEqual(1000 * i++, entry.Value);
            }
        }

        [TestMethod, ExpectedException( typeof(NotSupportedException) )]
        public void IndexerTest()
        {
            AddToEmpty( 25, true );

            // Check to make sure the indexer works for all entries in the dictionary
            for ( int i = 1; i <= 25; i++ )
                Assert.AreEqual( i*1000, m_Dictionary[i] );

            // Make sure the Index throws an exception for an item not in the dictionary
            Random rand = new Random();
            int nRand   = rand.Next( 26, int.MaxValue );
            int nValue  = (int)m_Dictionary[nRand];
        }

        [TestMethod]
        public void IndexerNullTest()
        {
            Assert.AreEqual(0, referenceDictionary.Count);

            for (int i = 1; i <= 25; i++)
                referenceDictionary.Add(new CloneableInt(i), new CloneableInt(i * 1000));

            Assert.AreEqual(25, referenceDictionary.Count);

            // Make sure that null throws the proper exception
            Assert.ThrowsException<ArgumentNullException>(() => { var willFail = referenceDictionary[null]; });
            Assert.ThrowsException<ArgumentNullException>(() => { referenceDictionary[null] = new CloneableInt(0); });
            Assert.ThrowsException<ArgumentNullException>(() => referenceDictionary.ContainsKey(null));
        }

        [TestMethod, ExpectedException( typeof(NotSupportedException) )]
        public void IndexerEmptyTest()
        {
            // Make sure that empty dictionary throws the proper exception
            int nValue = (int)m_Dictionary[1];
        }

        [TestMethod]
        public void CopyTo()
        {
            AddToEmpty( 25, true );

            // Copy all the elements to an array
            KeyValuePair<int, int>[] array = new KeyValuePair<int, int>[m_Dictionary.Count];
            m_Dictionary.CopyTo( array, 0 );
            Assert.AreEqual( m_Dictionary.Count, array.Length );

            int i = 0;
            foreach ( KeyValuePair<int, int> entry in m_Dictionary )
            {
                // Make sure tha that the array matches what I added
                Assert.AreEqual( i+1, array[i].Key );
                Assert.AreEqual( (i+1)*1000, array[i].Value );

                // Make sure that the array matches the enumerator
                Assert.AreEqual( entry.Key, array[i].Key );
                Assert.AreEqual( entry.Value, array[i].Value );

                i++;
            }
        }

        [TestMethod]
        public void CopyToOffset()
        {
            Assert.AreEqual( 0, m_Dictionary.Count );

            // Fill up my dictionary with the upper half of what I want in the array
            for ( int i = 5; i < 10; i++ )
                m_Dictionary.Add( i, i*1000 );

            Assert.AreEqual( 5, m_Dictionary.Count );

            KeyValuePair<int, int>[] array = new KeyValuePair<int, int>[10];

            // Pre-fill the array with the lower half of what I want in there
            for ( int i = 0; i < 5; i++ )
                array[i] = new KeyValuePair<int, int>( i, i * 1000 );

            // Fill in the upper half with bad values.  These should get overwritten by CopyTo
            for ( int i = 5; i < 10; i++ )
                array[i] = new KeyValuePair<int, int>( i * 5, i * 2 );

            // Do the CopyTo the upper half of the array
            m_Dictionary.CopyTo( array, 5 );

            // Verify that the elements were overwritten
            for ( int i = 0; i < 10; i++ )
            {
                // Make sure tha that the array matches what I added
                Assert.AreEqual( i, array[i].Key );
                Assert.AreEqual( i*1000, array[i].Value );
            }
        }

        [TestMethod, ExpectedException( typeof(ArgumentNullException) )]
        public void CopyToNull()
        {
            AddToEmpty( 10 );

            m_Dictionary.CopyTo( null, 0 );
        }

        [TestMethod, ExpectedException( typeof(ArgumentException) )]
        public void CopyToSmall()
        {
            AddToEmpty( 25 );

            KeyValuePair<int, int>[] array = new KeyValuePair<int, int>[10];
            m_Dictionary.CopyTo( array, 0 );
        }

        [TestMethod, ExpectedException( typeof(ArgumentOutOfRangeException) )]
        public void CopyToNegative()
        {
            AddToEmpty( 10 );

            KeyValuePair<int, int>[] array = new KeyValuePair<int, int>[m_Dictionary.Count];
            m_Dictionary.CopyTo( array, -1 );
        }

        [TestMethod, ExpectedException( typeof(ArgumentException) )]
        public void CopyToBadOffset()
        {
            AddToEmpty( 10 );

            KeyValuePair<int, int>[] array = new KeyValuePair<int, int>[m_Dictionary.Count];
            m_Dictionary.CopyTo( array, 1 );
        }

        [TestMethod, ExpectedException( typeof(ArgumentOutOfRangeException) )]
        public void CopyToOutOfRange()
        {
            AddToEmpty( 10 );

            KeyValuePair<int, int>[] array = new KeyValuePair<int, int>[m_Dictionary.Count];
            m_Dictionary.CopyTo( array, 11 );
        }

        [TestMethod]
        public void CloneValueTest()
        {
            AddToEmpty( 25, true );

            IDictionary<int, int> cloned = MakeClone();

            // Go through all the elements in the list and make sure they are equal
            // but that they point to different physical objects
            IEnumerator<KeyValuePair<int, int>> enumOrig = m_Dictionary.GetEnumerator();
            IEnumerator<KeyValuePair<int, int>> enumClone = cloned.GetEnumerator();

            while ( enumOrig.MoveNext() && enumClone.MoveNext() )
            {
                // Keys point to different objects since they are value types but are equal
                Assert.IsFalse( object.ReferenceEquals( enumOrig.Current.Key, enumClone.Current.Key ) );
                Assert.AreEqual( enumOrig.Current.Key, enumClone.Current.Key );

                // Keys point to different objects since they are value types but are equal
                Assert.IsFalse( object.ReferenceEquals( enumOrig.Current.Value, enumClone.Current.Value ) );
                Assert.AreEqual( enumOrig.Current.Value, enumClone.Current.Value );
            }
        }

        [TestMethod]
        public void CloneObjectTest()
        {
            Assert.AreEqual( 0, m_Dictionary.Count );

            for ( int i = 1; i <= 25; i++ )
                m_Dictionary.Add( new CloneableInt( i ), new CloneableInt( i * 1000 ) );

            Assert.AreEqual( 25, m_Dictionary.Count );

            IDictionary<int, int> cloned = MakeClone();

            // Go through all the elements in the list and make sure they are equal
            // but that they point to different physical objects
            IEnumerator<KeyValuePair<int, int>> enumOrig = m_Dictionary.GetEnumerator();
            IEnumerator<KeyValuePair<int, int>> enumClone = cloned.GetEnumerator();

            while ( enumOrig.MoveNext() && enumClone.MoveNext() )
            {
                // Keys point to different objects but are equal
                Assert.IsFalse( object.ReferenceEquals( enumOrig.Current.Key, enumClone.Current.Key ) );
                Assert.AreEqual( enumOrig.Current.Key, enumClone.Current.Key );

                // Values point to different objects but are equal
                Assert.IsFalse( object.ReferenceEquals( enumOrig.Current.Value, enumClone.Current.Value ) );
                Assert.AreEqual( enumOrig.Current.Value, enumClone.Current.Value );
            }
        }

        [TestMethod]
        public void CloneEmptyTest()
        {
            IDictionary<int, int> cloned = MakeClone();
            Assert.AreNotEqual(cloned, m_Dictionary);
            Assert.AreEqual(cloned.Count, m_Dictionary.Count);
        }
    }
}