using System;
using NUnit.Framework;
using PylonLog.Utilities;
using System.IO;

namespace PylonLog.UnitTest
{
    [TestFixture]
    public class BinarySearchTest
    {
        [Test, TestCaseSource("SearchCases")]
        public void TestBinarySearch(string testCaseName,byte[] arrayToSearch, byte[] patternToFind, int startingIndex, int expectedResult)
        {
            Assert.AreEqual(expectedResult, (BinarySearch.findPatternReverse(arrayToSearch, patternToFind, startingIndex)));
        }

        static object[] SearchCases =
        {
            new object[] {"Searched Array Shorter Than Pattern", new byte[] {0xAA} , new byte[] { 0xAA, 0xA1 }, null ,- 1 },

            new object[] { "No Match",new byte[] {0xAA,0xA0,0xA1} , new byte[] { 0xBB, 0xA2 }, null ,-1 },

            new object[] { "Pattern Empty", new byte[] {0xAA,0xA0,0xA1} , new byte[] {}, null ,-1 },

            new object[] { "Searched Array Empty", new byte[] {} , new byte[] { 0xAA, 0xA2 }, null ,-1},

            new object[] { "Partial Match",new byte[] {0xAA,0xA0,0xA1} , new byte[] { 0xAA, 0xA2 }, null ,-1 },

            new object[] {"One Char Match",  new byte[] {0xAA,0xA0,0xA1} , new byte[] { 0xA0 }, 2 ,1},

            new object[] {"One Char Match At End", new byte[] {0xAA,0xA0,0xA1} , new byte[] { 0xA1 }, 2 ,2},

            new object[] {"Searched Array and Pattern Identical",new byte[] {0xAA,0xA0,0xA1} , new byte[] { 0xA1, 0xA0, 0xAA }, 2 ,2 },

            new object[] {"Starting Index Less Than Zero", new byte[] {0xAA,0xA0,0xA1} , new byte[] { 0xAA, 0xA0, 0xA1 }, -1 ,-1},

            new object[] {"Start at end with match", new byte[] {0xAA,0xA0,0xA1} , new byte[] { 0xA1 }, 2 ,2 },

            new object[] { "Start at end with no match",new byte[] {0xAA,0xA0,0xA1} , new byte[] { 0xA2 },2 ,-1},

            new object[] { "Start at 2 with match ", new byte[] {0xAA,0xA0,0xA1} , new byte[] { 0xA1 }, 2 ,2 },

            new object[] { "Find first of two matches",new byte[] {0xAA,0xA0,0xA1,0xA2,0xA3,0xA0,0xA1,0xA2} , new byte[] { 0xA1,0xA0 }, 7 ,6 },

            new object[] { "Find second of two matches",new byte[] {0xAA,0xA0,0xA1,0xA2,0xA3,0xA0,0xA1,0xA2} , new byte[] { 0xA1, 0xA0 }, 5 ,2 },

            new object[] { "Start Beyond Match", new byte[] {0xAA,0xA0,0xA1,0xA2,0xA3,0xA0,0xA1,0xA2,0xA3,0xA4} , new byte[] { 0xA0,0xA1 }, 6 ,-1}
        };
        [Test, TestCaseSource("SearchReverseCases")]
        public void TestBinarySearchReverse(string testCaseName, byte[] arrayToSearch, byte[] patternToFind, int startingIndex, int expectedResult)
        {
            Assert.AreEqual(expectedResult, (BinarySearch.findPattern(arrayToSearch, patternToFind, startingIndex)));
        }

        static object[] SearchReverseCases =
        {
            new object[] {"Searched Array Shorter Than Pattern", new byte[] {0xAA} , new byte[] { 0xAA, 0xA1 }, null ,- 1 },

            new object[] { "No Match",new byte[] {0xAA,0xA0,0xA1} , new byte[] { 0xBB, 0xA2 }, null ,-1 },

            new object[] { "Pattern Empty", new byte[] {0xAA,0xA0,0xA1} , new byte[] {}, null ,-1 },

            new object[] { "Searched Array Empty", new byte[] {} , new byte[] { 0xAA, 0xA2 }, null ,-1},

            new object[] { "Partial Match",new byte[] {0xAA,0xA0,0xA1} , new byte[] { 0xAA, 0xA2 }, null ,-1 },

            new object[] {"One Char Match",  new byte[] {0xAA,0xA0,0xA1} , new byte[] { 0xA0 }, null ,1},

            new object[] {"One Char Match At End", new byte[] {0xAA,0xA0,0xA1} , new byte[] { 0xA1 }, null ,2},

            new object[] {"Searched Array and Pattern Identical",new byte[] {0xAA,0xA0,0xA1} , new byte[] { 0xAA, 0xA0, 0xA1 }, null ,0 },

            new object[] {"Starting Index Less Than Zero", new byte[] {0xAA,0xA0,0xA1} , new byte[] { 0xAA, 0xA0, 0xA1 }, -1 ,-1},

            new object[] {"Start at 0 with match", new byte[] {0xAA,0xA0,0xA1} , new byte[] { 0xA1 }, 0 ,2 },

            new object[] { "Start at 0 with no match",new byte[] {0xAA,0xA0,0xA1} , new byte[] { 0xA2 },0 ,-1},

            new object[] { "Start at 2 with match ", new byte[] {0xAA,0xA0,0xA1} , new byte[] { 0xA1 }, 2 ,2 },

            new object[] { "Find first of two matches",new byte[] {0xAA,0xA0,0xA1,0xA2,0xA3,0xA0,0xA1,0xA2} , new byte[] { 0xA0,0xA1 }, 0 ,1 },

            new object[] { "Find second of two matches",new byte[] {0xAA,0xA0,0xA1,0xA2,0xA3,0xA0,0xA1,0xA2} , new byte[] { 0xA0,0xA1 }, 2 ,5 },

            new object[] { "Start Beyond Match", new byte[] {0xAA,0xA0,0xA1,0xA2,0xA3,0xA0,0xA1,0xA2,0xA3,0xA4} , new byte[] { 0xA0,0xA1 }, 6 ,-1}
        };
    }

}
