using System;

namespace TheCodingMonkey.Collections
{
    /// <summary>Trivial cloneable object used for testing a Binary Search Tree.</summary>
    public class CloneableInt : ICloneable, IComparable<CloneableInt>, IComparable
    {
        public CloneableInt( int n )
        { Value = n; }

        public object Clone()
        { return new CloneableInt( Value ); }

        public override bool Equals( object obj )
        { return ( CompareTo( obj ) == 0 ); }

        public int CompareTo(CloneableInt obj)
        {
            return Value.CompareTo(obj.Value);
        }

        public int CompareTo( object obj )
        {
            if ( obj == null )
                throw new ArgumentNullException( "obj", "obj cannot be null" );

            CloneableInt c = obj as CloneableInt;
            return CompareTo(c);
        }

        public override int GetHashCode()
        { return Value.GetHashCode(); }

        public int Value { get; set; }
    }
}