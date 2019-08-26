using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheCodingMonkey.Collections.Tests
{
    [TestClass, TestCategory("Command Line")]
    public class CmdArgumentsTest
    {
        private string[] BuildArgumentArray( string delim )
        {
            string[] strArray = { string.Format( "{0}Arg1", delim ),
                                  string.Format( "{0}Arg2:value2", delim ),
                                  string.Format( "{0}Arg3=value3", delim ),
                                  string.Format( "{0}Arg4", delim ),
                                  "value4"
                              };
            return strArray;
        }

        private void TestArguments( string[] strArgs, bool caseSensitive )
        {
            CmdArguments parsed = new CmdArguments(strArgs, caseSensitive);
            Assert.IsFalse(parsed.IsReadOnly);
            var parsedKeys = parsed.Keys;
            var parsedValues = parsed.Values;

            for ( int i = 1; i < 5; i++ )
            {
                string key = $"Arg{i}";
                Assert.IsTrue(parsed.ContainsKey(key));                              // Match case
                Assert.AreNotEqual(caseSensitive, parsed.ContainsKey($"arg{i}"));    // Different case

                string strValue = parsed[key];
                string strExpected = ( i == 1 ) ? string.Empty : $"value{i}";
                Assert.AreEqual( strExpected, strValue );

                Assert.IsTrue(parsedKeys.Contains(caseSensitive ? key : key.ToLower()));
                Assert.IsTrue(parsedValues.Contains(strExpected));

                string tryGet;
                Assert.IsTrue(parsed.TryGetValue(key, out tryGet));
                Assert.AreEqual(strExpected, tryGet);
                WriteArgument( key, strValue );
            }

            parsed.Clear();
            Assert.AreEqual(0, parsed.Count);
        }

        private void WriteArgument( string strKey, string strValue )
        {
            Console.WriteLine("{0} has a value of: {1}", strKey, strValue );
        }

        [TestMethod]
        public void DashesTest()
        {
            string[] args = BuildArgumentArray( "-" );
            TestArguments(args, false);
            TestArguments(args, true);
        }

        [TestMethod]
        public void DoubleDashTest()
        {
            string[] args = BuildArgumentArray( "--" );
            TestArguments(args, false);
            TestArguments(args, true);
        }

        [TestMethod]
        public void SlashTest()
        {
            string[] args = BuildArgumentArray( "/" );
            TestArguments(args, false);
            TestArguments(args, true);
        }

        [TestMethod]
        public void QuotesTest()
        {
            string strKey = "file";
            string strValue = "The file to get";

            string[] strQuotes = { string.Format("/{0}", strKey ), strValue };
            CmdArguments parsedArgs = new CmdArguments( strQuotes );

            Assert.AreEqual( strValue, parsedArgs[strKey] );
            WriteArgument( strKey, strValue );
        }
    }
}