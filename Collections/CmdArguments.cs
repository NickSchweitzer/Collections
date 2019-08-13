using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace TheCodingMonkey.Collections
{
    /// <summary>Class for performing verbose parsing of command line arguments. Accepts command line parameters in a variety of patterns, and puts the keys and values 
    /// in a StringDictionary for easy use later.</summary>
    public class CmdArguments : StringDictionary 
    {
        // Static regular expression used to find command line arguments
        static private readonly Regex nameValueRegex = new Regex(@"^([/-]|--){1}(?<name>\w+)([:=])?(?<value>.+)?$");

        /// <summary>Standard Constructor</summary>
        /// <param name="args">Command Line Arguments from Main</param>
        /// <remarks>Valid parameters forms:
        /// {-,/,--}param{ ,=,:}((",')value(",'))
        /// Examples: -param1 value1 --param2 /param3:"Test-:-work" /param4=happy -param5 '--=nice=--'</remarks>
        public CmdArguments( string[] args )
        {
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
                    Add( lastName, match.Groups["value"].Value.Trim( trimChars ) );
                }
            }
        }

        /// <summary>Gets or sets the element with the specified key.</summary>
        /// <param name="key">Command line argument to get or set the value for</param>
        /// <returns>The Value that was provided for this command line argument, or Empty String if there were none</returns>
        public override string this[string key]
        {
            get
            {
                key = key.ToLower();
                return base[key];
            }
            set
            {
                key = key.ToLower();
                base[key] = value;
            }
        }

        /// <summary>Determines whether the Command Line arguments contain an element with the specified key. The Key is case insensitive.</summary>
        /// <param name="key">Command line argument to check for (case insensitive search)</param>
        /// <returns>True if the given key was provied, false otherwise.</returns>
        public override bool ContainsKey(string key)
        {
            key = key.ToLower();
            return base.ContainsKey(key);
        }

        /// <summary>Adds a element with the provided key and value to the Dictionary, if they weren't part of the original command line argument string.</summary>
        /// <param name="key">The object to use as a key of the element to add. This is automatically made lower case.</param>
        /// <param name="value">The value to add for the given key.</param>
        public override void Add(string key, string value)
        {
            key = key.ToLower();
            base.Add(key, value);
        }
    }
}