using System;

namespace TheCodingMonkey.Collections
{
    /// <summary>Trivial cloneable object used for testing a Binary Search Tree.</summary>
    public class CloneableInt : ICloneable, IComparable<CloneableInt>, IComparable
    {
        public CloneableInt()
        : this( 0 ) {}

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
            if (c != null)
                return CompareTo(c);
            else
            {
                int i = (int)c;
                return CompareTo(new CloneableInt(i));
            }
        }

        public override int GetHashCode()
        { return Value.GetHashCode(); }

        public int Value { get; set; }

        public static implicit operator int( CloneableInt n ) 
        { return n.Value; }

        public static implicit operator CloneableInt( int n )
        { return new CloneableInt( n ); }
    }
}