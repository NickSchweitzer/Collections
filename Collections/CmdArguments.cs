using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TheCodingMonkey.Collections
{
    /// <summary>Class for performing verbose parsing of command line arguments. Accepts command line parameters in a variety of patterns, and puts the keys and values 
    /// in a StringDictionary for easy use later.</summary>
    public class CmdArguments : IDictionary<string, string> 
    {
        private readonly IDictionary<string, string> innerDictionary = new Dictionary<string, string>();

        // Static regular expression used to find command line arguments
        static private readonly Regex nameValueRegex = new Regex(@"^([/-]|--){1}(?<name>\w+)([:=])?(?<value>.+)?$");

        /// <summary>Standard Constructor</summary>
        /// <param name="args">Command Line Arguments from Main</param>
        /// <param name="caseSensitive">Set to true if argument keys should be treated in a case sensitive manner.</param>
        /// <remarks>Valid parameters forms:
        /// {-,/,--}param{ ,=,:}((",')value(",'))
        /// Examples: -param1 value1 --param2 /param3:"Test-:-work" /param4=happy -param5 '--=nice=--'</remarks>
        public CmdArguments(string[] args, bool caseSensitive = false)
        {
            CaseSensitive = caseSensitive;
            string lastName = null;             // Last name which was found
            char [] trimChars = {'"','\''};     // Set of characters to trim from a command line argument

            foreach( string arg in args )
            {
                // Look for a argument delimiter
                Match match = nameValueRegex.Match( arg );
                if ( !match.Success )
                {
                    // Found a value (for the last parameter found (space separator))
                    if ( lastName != null )
                        this[lastName] = arg.Trim( trimChars );
                }
                else
                {			
                    // Matched a name, optionally with inline value
                    lastName = match.Groups["name"].Value;
                    if (!caseSensitive)
                        lastName = lastName.ToLower();

                    Add( lastName, match.Groups["value"].Value.Trim( trimChars ) );
                }
            }
        }

        /// <summary>Determines if command line argument keys are treated in a case senesitive manner. By default, the parser is case insensitive.</summary>
        public bool CaseSensitive { get; private set; } = false;

        /// <summary>Retrieves an ICollection of command line argument switches that were used</summary>
        public ICollection<string> Keys => innerDictionary.Keys;

        /// <summary>Retrieves an ICollection of the command line parameters passed to the switches. This is generally useless without knowing
        /// the corresponding keys.</summary>
        public ICollection<string> Values => innerDictionary.Values;

        /// <summary>Retrieves the number of command line switches passed to the command line</summary>
        public int Count => innerDictionary.Count;

        /// <summary>Gets a value indicating whether the Dictionary is read-only</summary>
        public bool IsReadOnly => innerDictionary.IsReadOnly;

        /// <summary>Gets or sets the element with the specified key.</summary>
        /// <param name="key">Command line argument to get or set the value for</param>
        /// <returns>The Value that was provided for this command line argument, or Empty String if there were none</returns>
        public string this[string key]
        {
            get
            {
                if (!CaseSensitive)
                    key = key.ToLower();

                return innerDictionary[key];
            }
            set
            {
                if (!CaseSensitive)
                    key = key.ToLower();

                innerDictionary[key] = value;
            }
        }

        /// <summary>Determines whether the Command Line arguments contain an element with the specified key. The Key is case insensitive.</summary>
        /// <param name="key">Command line argument to check for (case insensitive search)</param>
        /// <returns>True if the given key was provied, false otherwise.</returns>
        public bool ContainsKey(string key)
        {
            if (!CaseSensitive)
                key = key.ToLower();

            return innerDictionary.ContainsKey(key);
        }

        /// <summary>Adds a element with the provided key and value to the Dictionary, if they weren't part of the original command line argument string.</summary>
        /// <param name="key">The object to use as a key of the element to add. This is automatically made lower case.</param>
        /// <param name="value">The value to add for the given key.</param>
        public void Add(string key, string value)
        {
            if (!CaseSensitive)
                key = key.ToLower();

            innerDictionary.Add(key, value);
        }

        /// <summary>Removes the value with the specified command line switch from the Dictionary</summary>
        /// <param name="key">Command line switch to remove</param>
        /// <returns>True if the element was found, False otherwise.</returns>
        public bool Remove(string key)
        {
            if (!CaseSensitive)
                key = key.ToLower();

            return innerDictionary.Remove(key);
        }

        /// <summary>Gets the value associated with the specified command line switch.</summary>
        /// <param name="key">Command line switch to search for</param>
        /// <param name="value">Parameter for the given switch if the switch was found, otherwise the default value.</param>
        /// <returns>True if the command line switch is in the dictionary, False otherwise.</returns>
        public bool TryGetValue(string key, out string value)
        {
            return innerDictionary.TryGetValue(key, out value);
        }

        /// <summary>Adds a element with the provided key and value to the Dictionary, if they weren't part of the original command line argument string.</summary>
        /// <param name="item">The switch and corresponding value to add. This Key is automatically made lower case if <see cref="CaseSensitive">CaseSensitive</see> is False.</param>
        public void Add(KeyValuePair<string, string> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>Removes all items from the Collection</summary>
        public void Clear()
        {
            innerDictionary.Clear();
        }

        /// <summary>Returns true if the Dictionary contains the command line switch specified in item</summary>
        /// <param name="item">Item to look for (only the Key is used)</param>
        /// <returns>True if the Key is in the list, False otherwise.</returns>
        public bool Contains(KeyValuePair<string, string> item)
        {
            return ContainsKey(item.Key);
        }

        /// <summary>Copies the elements of the Dictionary to an array, starting at the specified array index.</summary>
        /// <param name="array">The one-dimensional array that is the destination of the elements copied from the Dictionary. The array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins</param>
        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            ((IDictionary<string, string>)innerDictionary).CopyTo(array, arrayIndex);
        }

        /// <summary>Removes the value with the specified command line switch from the Dictionary</summary>
        /// <param name="item">Command line switch to remove (only the Key is used in the item)</param>
        /// <returns>True if the element was found, False otherwise.</returns>
        public bool Remove(KeyValuePair<string, string> item)
        {
            return Remove(item.Key);
        }

        /// <summary>Returns an IDictionaryEnumerator for the IDictionary.</summary>
        /// <returns>An IDictionaryEnumerator for the IDictionary.</returns>
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return innerDictionary.GetEnumerator();
        }

        /// <summary>Returns an IDictionaryEnumerator for the IDictionary.</summary>
        /// <returns>An IDictionaryEnumerator for the IDictionary.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return innerDictionary.GetEnumerator();
        }
    }
}