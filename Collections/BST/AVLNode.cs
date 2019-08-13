using System;

namespace TheCodingMonkey.Collections.BST
{
    /// <summary>AVL Binary Search Tree Node.</summary>
    public class AVLNode<TKey, TValue> : Node<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        /// <summary>Balance Constant: Left is Heavy.</summary>
        protected const int LEFT_HEAVY = -1;
        /// <summary>Balance Constant: Tree is balanced.</summary>
        protected const int BALANCED = 0;
        /// <summary>Balance Constant: Right is Heavy.</summary>
        protected const int RIGHT_HEAVY = 1;

        /// <summary>Default Constructor</summary>
        internal AVLNode()
        : this( default(TKey), default(TValue) )
        { }

        /// <summary>Constructs a node with data, but no children.</summary>
        /// <param name="key">Key used for comparison and lookup.</param>
        /// <param name="data">Data at this node.</param>
        internal AVLNode( TKey key, TValue data ) 
        : this( key, data, null, null ) 
        { }

        /// <summary>Constructs a node with data, and two children.</summary>
        /// <param name="key">Key used for comparison and lookup.</param>
        /// <param name="data">Data at this node.</param>
        /// <param name="left">Left child node.</param>
        /// <param name="right">Right child node.</param>
        internal AVLNode( TKey key, TValue data, Node<TKey, TValue> left, Node<TKey, TValue> right )
        : base( key, data, left, right )
        {
            Balance = 0;
        }

        /// <summary>Performs a deep copy on this node, and any children if they exist.</summary>
        /// <returns>Copy of this node.</returns>
        public override object Clone()
        {
            AVLNode<TKey, TValue> clone = new AVLNode<TKey, TValue>();
            clone.Balance = Balance;

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
                clone.Left = (Node<TKey, TValue>)Left.Clone();
        	
            if ( Right != null )
                clone.Right = (Node<TKey, TValue>)Right.Clone();

            return clone;
        }

        /// <summary>Checks the node for valid AVL Tree properties.</summary>
        /// <exception cref="AVLException">Thrown if the AVL Tree is not valid.</exception>
        internal override void Check()
        {
            base.Check();

            // Now get the height of each subtree
            int leftHeight = ( Left != null ) ? ( (AVLNode<TKey, TValue>)Left ).Height : 0;
            int rightHeight = ( Right != null ) ? ( (AVLNode<TKey, TValue>)Right ).Height : 0;

            // Verify that AVL tree property is satisfied
            int diffHeight = rightHeight - leftHeight;
            if ( LeftImbalance( diffHeight ) || RightImbalance( diffHeight ) )
                throw new AVLException( "Height difference is invalid", Key, Value, diffHeight );

            // Verify that balance-factor is correct
            if ( diffHeight != Balance )
                throw new AVLException( "Height different doesn't match balance-factor", Key, Value, diffHeight, Balance );
        }

        /// <summary>Adds the given key and value to the tree at the current node.</summary>
        /// <param name="root">Node to add key/value pair to.</param>
        /// <param name="key">Key used to index the value.</param>
        /// <param name="value">Value to add to the node</param>
        /// <param name="change">Balance change due to insertion.</param>
        public static void Add( ref Node<TKey, TValue> root, TKey key, TValue value, ref int change )
        {
            // See if the tree is empty
            if ( root == null ) 
            {
                // Insert new node here 
                root = new AVLNode<TKey, TValue>( key, value );
                change =  1;
                return;
            }
            AVLNode<TKey, TValue> avlRoot = (AVLNode<TKey, TValue>)root;

            // Compare items and determine which direction to search
            // Make sure that nResult is between -1 and 1
            int increase = 0;
            int result  = key.CompareTo( root.Key );
            if ( result < 0 )
                result = -1;
            else if ( result > 0 )
                result = 1;

            uint dir = ( result < 0 ) ? LEFT : RIGHT;
            if ( result == 0 )
                throw new ArgumentException( "Attempting to add duplicate item to the tree.  The items has a value of " + key.ToString(), "data" );
            else
            {
                // Insert into "dir" subtree
                Add( ref avlRoot.Nodes[dir], key, value, ref change );
                root = avlRoot;
                increase = result * change;  // set balance factor increment
            }

            // Rebalance the tree if needed
            avlRoot.Balance += increase;
            change = 0;
            if ( ( increase != 0 ) && ( avlRoot.Balance != 0 ) ) 
            {
                bool balChanged = ReBalance( ref avlRoot );
                change = 1 - (balChanged ? 1 : 0);
                root = avlRoot;
            }
        }

        /// <summary>Removes a node from the tree below the root node.</summary>
        /// <param name="root">Node to search for item from.</param>
        /// <param name="key">Item to use for comparison</param>
        /// <param name="change">Balance change due to removal.</param>
        /// <param name="cmp">Comparison Type.</param>
        public static Node<TKey, TValue> Remove( ref Node<TKey, TValue> root, TKey key, ref int change, CompType cmp )
        {
            // See if the tree is empty
            if ( root == null )
            {
                change = 0;
                return null;
            }

            Node<TKey, TValue> nodeReturn = null;
            AVLNode<TKey, TValue> avlRoot = (AVLNode<TKey, TValue>)root;

            // Compare items and determine which direction to search
            int  decrease = 0;
            int  result  = avlRoot.Compare( key, cmp );
            uint dir = ( result == -1 ) ? LEFT : RIGHT;

            if ( result != 0 ) 
            {
                // Look into the appropriate sub tree
                nodeReturn = Remove( ref avlRoot.Nodes[dir], key, ref change, cmp );
                if ( nodeReturn == null)
                    return nodeReturn;

                decrease = result * change;
            } 
            else  
            {
                nodeReturn = root;
                // Delete this node re-arrange the tree
                if ( ( root.Left == null ) && ( root.Right == null ) )
                {
                    // This is a leaf - simply remove it
                    root   = null;
                    change = 1;
                    return nodeReturn;
                }
                else if ( ( root.Left == null ) || ( root.Right == null ) )
                {
                    // Only a single child, that child is now the root
                    Node<TKey, TValue> toDelete = root;
                    uint child   = ( root.Left != null ) ? LEFT : RIGHT;
                    root   = root[child];
                    change = 1;
                    toDelete.Left  = null;
                    toDelete.Right = null;
                    return nodeReturn;
                } 
                else 
                {
                    Node<TKey, TValue> tempDelete = Remove( ref avlRoot.Nodes[RIGHT], key, ref decrease, CompType.MIN_CMP );
                    root.Key   = tempDelete.Key;
                    root.Value = tempDelete.Value;
                }
            }

            // Rebalance the tree if necessary
            avlRoot.Balance -= decrease;
            if ( decrease != 0 ) 
            {
                if ( avlRoot.Balance != 0 )
                {
                    change = ReBalance( ref avlRoot ) ? 1 : 0;  // rebalance and see if height changed
                    root   = avlRoot;
                }
                else
                    change = 1;   // balanced because subtree decreased
            } 
            else 
                change = 0;

            return nodeReturn;
        }

        /// <summary>Height of this sub-tree.</summary>
        public int Height
        {
            get
            {
                int leftHeight = ( Left != null ) ? ( (AVLNode<TKey, TValue>)Left ).Height : 0;
                int rightHeight = ( Right != null ) ? ( (AVLNode<TKey, TValue>)Right ).Height : 0;
                return ( 1 + Math.Max( leftHeight, rightHeight ) );
            }
        }

        /// <summary>Balance factor for this sub-tree.</summary>
        public int Balance { get; set; }

        private static bool LeftImbalance( int bal )
        {
            return ( bal < LEFT_HEAVY );
        }

        private static bool RightImbalance( int bal )
        {
            return ( bal > RIGHT_HEAVY );
        }

        private static bool RotateOnce( ref AVLNode<TKey, TValue> root, uint dir )
        {
            uint otherDir = Opposite( dir );
            AVLNode<TKey, TValue> oldRoot = root;

            // Is the height going to change?
            bool heightChange = !( ( (AVLNode<TKey, TValue>)root[otherDir] ).Balance == 0 );

            // Assign a new root node
            root = (AVLNode<TKey, TValue>)oldRoot[otherDir];

            // The new root exchanges its subtree for its parents
            oldRoot[otherDir] = root[dir];
            root[dir] = oldRoot;

            // Update the balance factors
            oldRoot.Balance = -( (dir == LEFT) ? --(root.Balance) : ++(root.Balance) );
            return heightChange;
        }

        private static bool RotateTwice( ref AVLNode<TKey, TValue> root, uint dir )
        {
            uint otherDir = Opposite( dir );
            AVLNode<TKey, TValue> oldRoot = root;
            AVLNode<TKey, TValue> oldOtherDirSubtree = (AVLNode<TKey, TValue>)root[otherDir];

            // assign new root
            AVLNode<TKey, TValue> temp = (AVLNode<TKey, TValue>)oldRoot[otherDir];
            root = (AVLNode<TKey, TValue>)temp[dir];

            // new-root exchanges it's "dir" mySubtree for it's grandparent
            oldRoot[otherDir] = root[dir];
            root[dir] = oldRoot;

            // new-root exchanges it's "other-dir" mySubtree for it's parent
            oldOtherDirSubtree[dir] = root[otherDir];
            root[otherDir] = oldOtherDirSubtree;

            // update balances
            ( (AVLNode<TKey, TValue>)root.Left ).Balance = -Math.Max( root.Balance, 0 );
            ( (AVLNode<TKey, TValue>)root.Right ).Balance = -Math.Min( root.Balance, 0 );
            root.Balance = 0;

            // A double rotation always shortens the overall height of the tree
            return true;
        }

        private static bool ReBalance( ref AVLNode<TKey, TValue> root )
        {
            bool heightChange = false;
            if ( LeftImbalance( root.Balance ) )
            {
                // Rotate Right
                if ( ( (AVLNode<TKey, TValue>)root.Left ).Balance == RIGHT_HEAVY )
                    heightChange = RotateTwice( ref root, RIGHT );      // Right-Left Rotate
                else
                    heightChange = RotateOnce( ref root, RIGHT );       // Right-Right Rotate
            }
            else if ( RightImbalance( root.Balance ) )
            {
                // Rotate Left
                if ( ( (AVLNode<TKey, TValue>)root.Right ).Balance == LEFT_HEAVY )
                    heightChange = RotateTwice( ref root, LEFT );       // Left-Right Rotate
                else
                    heightChange = RotateOnce( ref root, LEFT );        // Left-Left Rotate
            }

           return  heightChange;
        }
    }
}