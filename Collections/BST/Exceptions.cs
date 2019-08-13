using System;

namespace TheCodingMonkey.Collections.BST
{
    /// <summary>Exception thrown if a Binary Search Tree fails validation.</summary>
    public class BSTException: Exception
    {
        /// <summary>BSTException Constructor.</summary>
        /// <param name="message">Exception Message</param>
        /// <param name="currKey">Key of the Current Node.</param>
        /// <param name="currValue">Value of the Current Node.</param>
        /// <param name="otherKey">Key of another Node.</param>
        /// <param name="otherValue">Value of another Node.</param>
        public BSTException( string message, object currKey, object currValue, object otherKey, object otherValue )
        : base(message)
        {
            CurrentKey = currKey;
            CurrentValue = currValue;
            OtherKey = otherKey;
            OtherValue = otherValue;
        }

        /// <summary>BSTException Constructor.</summary>
        /// <param name="message">Exception Message</param>
        /// <param name="currKey">Key of the Current Node.</param>
        /// <param name="currValue">Value of the Current Node.</param>
        public BSTException(string message, object currKey, object currValue)
        : this(message, currKey, currValue, null, null)
        { }

        /// <summary>Key for the Current Invalid Node.</summary>
        public object CurrentKey { get; set; }
        
        /// <summary>Value for the Current Invalid Node.</summary>
        public object CurrentValue { get; set; }

        /// <summary>Key for a Node Adjacent to the Current Node.</summary>
        public object OtherKey { get; set; }

        /// <summary>Value for a Node Adjacent to the Current Node.</summary>
        public object OtherValue { get; set; }
    }

    /// <summary>Exception thrown if an AVL Search Tree fails validation.</summary>
    public class AVLException : BSTException
    {
        /// <summary>AVLException Constructor.</summary>
        /// <param name="message">Exception Message</param>
        /// <param name="currKey">Key of the Current Node.</param>
        /// <param name="currValue">Value of the Current Node.</param>
        /// <param name="height">Height of the exception node.</param>
        public AVLException( string message, object currKey, object currValue, int height )
        : this(message, currKey, currValue, height, 0)
        { }

        /// <summary>AVLException Constructor.</summary>
        /// <param name="message">Exception Message</param>
        /// <param name="currKey">Key of the Current Node.</param>
        /// <param name="currValue">Value of the Current Node.</param>
        /// <param name="height">Height of the exception node.</param>
        /// <param name="balance">Balance factor of the exception node.</param>
        public AVLException( string message, object currKey, object currValue, int height, int balance )
        : base(message, currKey, currValue, null, null)
        {
            Height  = height;
            Balance = balance;
        }

        /// <summary>Height of the node causing the exception.</summary>
        public int Height { get; }

        /// <summary>Invalid balance factor causing the exception.</summary>
        public int Balance { get; }
    }
}