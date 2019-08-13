using System;

namespace TheCodingMonkey.Collections.BST
{
    /// <summary>Red-Black Binary Search Tree Node.</summary>
    public class RedBlackNode<TKey, TValue> : Node<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        /// <summary>Default Constructor</summary>
        internal RedBlackNode()
        : this( default, default)
        { }

        /// <summary>Constructs a node with data, but no children.</summary>
        /// <param name="key">Key used for comparison and lookup.</param>
        /// <param name="data">Data at this node.</param>
        internal RedBlackNode( TKey key, TValue data ) 
        : this( key, data, null, null ) 
        { }

        /// <summary>Constructs a node with data, and two children.</summary>
        /// <param name="key">Key used for comparison and lookup.</param>
        /// <param name="data">Data at this node.</param>
        /// <param name="left">Left child node.</param>
        /// <param name="right">Right child node.</param>
        internal RedBlackNode( TKey key, TValue data, Node<TKey, TValue> left, Node<TKey, TValue> right )
        : base( key, data, left, right )
        {
            // Can't really determine the proper color of the node here which is 
            // not very object oriented.  We have to trust the caller to properly 
            // initialize the color based on the parent color
        }

        /// <summary>Performs a deep copy on this node, and any children if they exist.</summary>
        /// <returns>Copy of this node.</returns>
        public override object Clone()
        {
            RedBlackNode<TKey, TValue> clone = new RedBlackNode<TKey, TValue>();
            clone.Color = Color;

            // If the key is cloneable, then do a deep copy.  Otherwise do a shallow one
            if ( Key is ICloneable )
                clone.Key = (TKey)((ICloneable)Key).Clone();
            else
                clone.Key = Key;

            // If the data is cloneable, then do a deep copy.  Otherwise do a shallow one
            if ( Value is ICloneable )
                clone.Value = (TValue)((ICloneable)Value).Clone();
            else
                clone.Value = Value;

            // Clone the children if they exist
            if ( Left != null )
                clone.Left = (RedBlackNode<TKey, TValue>)Left.Clone();
        	
            if ( Right != null )
                clone.Right = (RedBlackNode<TKey, TValue>)Right.Clone();

            return clone;
        }

        /// <summary>Adds the given key and value to the tree at the current node.</summary>
        /// <param name="root">Node to add key/value pair to.</param>
        /// <param name="parent">Parent node of root.</param>
        /// <param name="key">Key used to index the value.</param>
        /// <param name="value">Value to add to the node</param>
        public static void Add( ref Node<TKey, TValue> root, ref Node<TKey, TValue> parent, TKey key, TValue value )
        {
            // See if the tree is empty
            if ( root == null )
            {
                // Insert new node here. If parent is null this is the first node so it should be black - 
                // otherwise make it red.
                root = new RedBlackNode<TKey, TValue>(key, value)
                {
                    Color = (parent == null) ? NodeColor.Black : NodeColor.Red
                };
                return;
            }

            // Compare items and determine which direction to search
            // Make sure that nResult is between -1 and 1
            int nResult = key.CompareTo( root.Key );
            uint dir    = ( nResult < 0 ) ? LEFT : RIGHT;

            if ( nResult == 0 )
                throw new ArgumentException( "Attempting to add duplicate item to the tree.  The items has a value of " + key.ToString(), "data" );
            else
            {
                // Insert into "dir" subtree
                RedBlackNode<TKey, TValue> rbRoot = (RedBlackNode<TKey, TValue>)root;
                Add( ref rbRoot.Nodes[dir], ref root, key, value );

                // Check for red-property violation with child
                rbRoot = (RedBlackNode<TKey, TValue>)root;
                RedBlackNode<TKey, TValue> rbChild = (RedBlackNode<TKey, TValue>)root[dir];
                if ( ( rbRoot.Color == NodeColor.Red ) && ( rbChild.Color == NodeColor.Red ) )
                    FixRedViolation( ref parent, ref root, dir );
            }
        }

        /// <summary>Removes a node from the tree below the root node.</summary>
        /// <param name="root">Node to search for item from.</param>
        /// <param name="key">Item to use for comparison</param>
        /// <param name="blackDecrease">True if black count has gone down.</param>
        /// <param name="cmp">Comparison Type.</param>
        public static Node<TKey, TValue> Remove( ref Node<TKey, TValue> root, TKey key, ref bool blackDecrease, CompType cmp )
        {
            // Initialize
            Node<TKey, TValue> found = null;
            blackDecrease = false;

            // See if the tree is empty
            if ( root == null )
                return null;        // Not found

            // Compare items and determine which direction to search
            RedBlackNode<TKey, TValue> rbRoot = (RedBlackNode<TKey, TValue>)root;
            int  result = rbRoot.Compare( key, cmp );
            uint dir = ( result == -1 ) ? LEFT : RIGHT;

            if ( result != 0 ) 
            {
                // Delete from "dir" subtree
                found = Remove( ref rbRoot.Nodes[dir], key, ref blackDecrease, cmp );
                if ( found == null )
                    return  found;   // not found - can't delete
            }
            else  
            {   // Found key at this node
                found = root;   // This is the node

                if ( ( root.Left == null ) && ( root.Right == null ) )
                {
                    // We have a leaf -- remove it
                    blackDecrease = ( rbRoot.Color == NodeColor.Black );
                    root = null;
                    return found;
                } 
                else 
                {
                    // If we have a left subtree, replace ourself with our predecessor,
                    // otherwise replace ourself with our successor. 
                    dir = ( root.Left != null ) ? LEFT : RIGHT;
                    cmp = ( dir == LEFT ) ? CompType.MAX_CMP : CompType.MIN_CMP;
                    Node<TKey, TValue> tempDelete = Remove( ref rbRoot.Nodes[dir], key, ref blackDecrease, cmp );
                    root.Key   = tempDelete.Key;
                    root.Value = tempDelete.Value;
                }
            }

            // Look for black-property violations
            if ( blackDecrease )
                blackDecrease = FixBlackViolation( ref root, dir );

            return found;
        }

        /// <summary>Checks the node for valid Red-Black Tree properties.</summary>
        /// <exception cref="ApplicationException">Thrown if the Red-Black Tree is not valid.</exception>
        internal override void Check()
        {
            base.Check();

            // Verify that Black property is satisfied
            int leftHeight = ( Left != null ) ? ( (RedBlackNode<TKey, TValue>)Left ).Height : 0;
            int rightHeight = ( Right != null ) ? ( (RedBlackNode<TKey, TValue>)Right ).Height : 0;

            if ( leftHeight != rightHeight )
                throw new BSTException( "Node has subtrees of unequal height", Key, Value );

            // Verify that Red property is satisfied
            if ( Color == NodeColor.Red ) 
            {
                if ( ( Left != null ) && ( ( (RedBlackNode<TKey, TValue>)Left ).Color == NodeColor.Red ) )
                    throw new BSTException( "Red Node has a Left Red Subtree", Key, Value );

                if ( ( Right != null ) && ( ( (RedBlackNode<TKey, TValue>)Right ).Color == NodeColor.Red ) )
                    throw new BSTException( "Red Node has a Right Red Subtree", Key, Value );
            }
        }

        /// <summary>Color (Red or Black) of this Node.</summary>
        private NodeColor Color { get; set; }

        /// <summary>Height of this sub-tree.</summary>
        public int Height
        {
            get
            {
                int leftHeight = ( Left != null ) ? ( (RedBlackNode<TKey, TValue>)Left ).Height : 0;
                int rightHeight = ( Right != null ) ? ( (RedBlackNode<TKey, TValue>)Right ).Height : 0;

                // If this node is red, then it doesnt add to the logical height
                // of the tree (this is why RED is defined to be zero)
                return ( (int)Color + Math.Max( leftHeight, rightHeight ) );
            }
        }

        /// <summary>Return the opposite of the given color.</summary>
        private static NodeColor Opposite( NodeColor color) 
        {
            return ( color == NodeColor.Red ) ? NodeColor.Black : NodeColor.Red;
        }

        /// <summary>Returns the Color for a node, or Black for null.</summary>
        private static NodeColor GetColor( Node<TKey, TValue> node )
        {
            return ( node == null ) ? NodeColor.Black : ( (RedBlackNode<TKey, TValue>)node ).Color;
        }

        private void FlipColor()
        {
            // Determine the opposite color of this node
            NodeColor otherColor = Opposite( Color );

            // Make subtrees the same color as this node
            if ( Left != null )
                ( (RedBlackNode<TKey, TValue>)Left ).Color = Color;
            if ( Right != null )
                ( (RedBlackNode<TKey, TValue>)Right ).Color = Color;

            // Flip the color of this node
            Color = otherColor;
        }

        private static void RotateOnce( ref Node<TKey, TValue> root, uint dir )
        {
            uint otherDir = Opposite( dir );
            Node<TKey, TValue> oldRoot = root;

            // assign new root
            root = oldRoot[otherDir];

            // new-root exchanges it's "dir" mySubtree for it's parent
            oldRoot[otherDir] = root[dir];
            root[dir] = oldRoot;
        }

        private static void RotateTwice( ref Node<TKey, TValue> root, uint dir )
        {
            uint otherDir = Opposite( dir );
            Node<TKey, TValue> oldRoot = root;
            Node<TKey, TValue> oldOtherDirSubtree = oldRoot[otherDir];

            // assign new root
            root = oldRoot[otherDir][dir];

            // new-root exchanges it's "dir" mySubtree for it's grandparent
            oldRoot[otherDir] = root[dir];
            root[dir] = oldRoot;

            // new-root exchanges it's "other-dir" mySubtree for it's parent
            oldOtherDirSubtree[dir] = root[otherDir];
            root[otherDir] = oldOtherDirSubtree;
        }

        private static void FixRedViolation( ref Node<TKey, TValue> parent, ref Node<TKey, TValue> child, uint dir )
        {
            // See if this is the top of the tree... if so then its black
            if ( parent == null )
                ( (RedBlackNode<TKey, TValue>)child ).Color = NodeColor.Black;
            else 
            {
                // Figure out which subtrees everything belongs to
                uint thisDir  = object.ReferenceEquals( parent.Left, child ) ? LEFT : RIGHT;
                uint otherDir = Opposite( thisDir );

                // Get my sibling and his color
                RedBlackNode<TKey, TValue> sibling = (RedBlackNode<TKey, TValue>)parent[otherDir];
                NodeColor sibColor = ( sibling != null ) ? sibling.Color : NodeColor.Black;
                if ( sibColor == NodeColor.Black )
                {
                    // Need to perform a rotation
                    if ( thisDir == dir ) 
                        RotateOnce(  ref parent, otherDir );
                    else 
                        RotateTwice( ref parent, otherDir );
                }

                // Now color flip the parent
                ( (RedBlackNode<TKey, TValue>)parent ).FlipColor();
            }
        }

        private static bool FixBlackViolation( ref Node<TKey, TValue> root, uint dir )
        {
            bool propagated = false;   // Initialize the return value

            // "dir" is the direction that just became deficient, the other
            // direction is the side that wasnt deleted from.
            uint otherDir = Opposite( dir );

            // Get the deficient subtree (if its exists) and its sibling
            RedBlackNode<TKey, TValue> child = (RedBlackNode<TKey, TValue>)root[dir];
            RedBlackNode<TKey, TValue> sibling = (RedBlackNode<TKey, TValue>)root[otherDir];

            if ( GetColor( child ) == NodeColor.Red ) 
            {
                // Increase the black height of this entire subtree by
                // simply making the child black. In theory - this routine
                // will never be called if the child is red.
                child.Color = NodeColor.Black;
            }
            else if ( GetColor( sibling ) == NodeColor.Black ) 
            {
                // Need to know if the parent is red or not. If it is, then
                // any rotated result needs to have a red-root (and root will
                // need to change to black), otherwise any rotated result will
                // have a black root.
                bool blackParent = ( GetColor( root ) == NodeColor.Black );
                if ( !blackParent )
                    ( (RedBlackNode<TKey, TValue>)root ).Color = NodeColor.Black;

                // Now look at the color of the sibling's children
                if ( GetColor( sibling[dir] ) == NodeColor.Red ) 
                {
                    // Need to change the color of this subtree if the parent is black
                    if ( blackParent )
                        ( (RedBlackNode<TKey, TValue>)sibling[dir] ).Color = NodeColor.Black;

                    // Now rotate twice to make this subtree the new root
                    RotateTwice( ref root, dir );
                } 
                else if ( GetColor( sibling[otherDir] ) == NodeColor.Red ) 
                {
                    // Need to change the color of this subtree
                    ( (RedBlackNode<TKey, TValue>)sibling[otherDir] ).Color = NodeColor.Black;

                    // If the root is red then exchange its color with the sibling
                    if ( !blackParent )
                        sibling.Color = NodeColor.Red;
                    
                    // Now rotate once to make the sibling the new root
                    RotateOnce( ref root, dir );
                } 
                else
                {
                    // Make the sibling red
                    sibling.Color = NodeColor.Red;

                    // If the root was black we have fixed the black-property
                    // violation at this level by shortening the black height of
                    // the "dir" subtree, hence the problem has been propagated
                    // up to the next higher level.
                    propagated = blackParent;
                }
            } 
            else 
            {
                // First, perform a single rotation and flip the color of the
                // sibling and its "dir" subtree.
                sibling.Color = NodeColor.Black;
                ( (RedBlackNode<TKey, TValue>)sibling[dir] ).Color = NodeColor.Red;
                RotateOnce( ref root, dir );

                // Now check for a red-property violation with the sibling's
                // subtree (which after the rotation, is now in a different
                // position). Note that the sibling's former subtree is the
                // "nephew" or "niece" of the child.
                RedBlackNode<TKey, TValue> rbRoot = (RedBlackNode<TKey, TValue>)root;
                RedBlackNode<TKey, TValue> tempNode = (RedBlackNode<TKey, TValue>)root[dir];
                Node<TKey, TValue> niece = root[dir][otherDir];

                if ( ( GetColor( niece[dir] )      == NodeColor.Red ) &&
                     ( GetColor( niece[otherDir] ) == NodeColor.Red ) )
                {
                    // change the color of the niece
                    ( (RedBlackNode<TKey, TValue>)niece ).Color = NodeColor.Black;

                    // No child, need an additional rotation
                    // (this time a double)
                    if ( child == null )
                        RotateTwice( ref rbRoot.Nodes[dir], dir );
                } 
                else if ( GetColor( niece[dir] ) == NodeColor.Red )
                    FixRedViolation( ref rbRoot.Nodes[dir], ref tempNode.Nodes[otherDir], dir );
                else if ( GetColor( niece[otherDir] ) == NodeColor.Red )
                    FixRedViolation( ref rbRoot.Nodes[dir], ref tempNode.Nodes[otherDir], otherDir );
            }

            return  propagated;
        }

        /// <summary>Node Color Enumeration</summary>
        public enum NodeColor
        {
            /// <summary>Red</summary>
            Red   = 0,
            /// <summary>Black</summary>
            Black = 1
        }
    }
}