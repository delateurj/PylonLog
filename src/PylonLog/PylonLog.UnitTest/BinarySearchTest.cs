using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PylonLog.Utilities;

namespace PylonLog.UnitTest
{
    [TestClass]
    public class BinarySearchTest
    {
        [TestMethod]
        public void Test_Pattern_Longer_Than_Target()
        {
            byte[] targetArray = { 12 };
            byte[] patternArray = { 12, 12 };

            Assert.AreEqual(-1, ( BinarySearch.findPattern(targetArray, patternArray)));
        }

        [TestMethod]
        public void No_Match()
        {
            byte[] targetArray = { 12 };
            byte[] patternArray = { 11 };

            Assert.AreEqual(-1, (BinarySearch.findPattern(targetArray, patternArray)));
        }

        [TestMethod]
        public void Empty_Pattern()
        {
            byte[] targetArray = { 12 };
            byte[] patternArray = {  };

            Assert.AreEqual(-1, (BinarySearch.findPattern(targetArray, patternArray)));
        }

        [TestMethod]
        public void Empty_Target()
        {
            byte[] targetArray = {  };
            byte[] patternArray = { 12};

            Assert.AreEqual(-1, (BinarySearch.findPattern(targetArray, patternArray)));
        }

        [TestMethod]
        public void Test_Pattern_Partial_Match()
        {
            byte[] targetArray = { 1, 2, 3, 4 };
            byte[] patternArray = { 4, 12 };

            Assert.AreEqual(-1, (BinarySearch.findPattern(targetArray, patternArray)));
        }

        [TestMethod]
        public void Test_Pattern_One_Char_Match()
        {
            byte[] targetArray = { 12 };
            byte[] patternArray = { 12 };

            Assert.AreEqual(0, (BinarySearch.findPattern(targetArray, patternArray)));
        }

        [TestMethod]
        public void Test_Pattern_One_Char_Match_At_End()
        {
            byte[] targetArray = { 1,2,3,4,12 };
            byte[] patternArray = { 12 };

            Assert.AreEqual(4, (BinarySearch.findPattern(targetArray, patternArray)));
        }

        [TestMethod]
        public void Test_Pattern_Two_Char_Match_At_End()
        {
            byte[] targetArray = { 1, 2, 3, 4, 12 };
            byte[] patternArray = { 4, 12 };

            Assert.AreEqual(3, (BinarySearch.findPattern(targetArray, patternArray)));
        }

        [TestMethod]
        public void Test_Starting_Index_Less_Than_Zero()
        {
            byte[] targetArray = { 1, 2, 3, 4, 12 };
            byte[] patternArray = { 4, 12 };

            Assert.AreEqual(-1, (BinarySearch.findPattern(targetArray, patternArray,-1)));
        }

        [TestMethod]
        public void Test_Pattern_Two_Char_Match_At_End_Start_At_1()
        {
            byte[] targetArray = { 1, 2, 3, 4, 12 };
            byte[] patternArray = { 4, 12 };

            Assert.AreEqual(3, (BinarySearch.findPattern(targetArray, patternArray,1)));
        }

        [TestMethod]
        public void Test_Pattern_Two_Char_Match_At_End_Start_Beyond_Match()
        {
            byte[] targetArray = { 1, 2, 3, 4, 12 };
            byte[] patternArray = { 4, 12 };

            Assert.AreEqual(-1, (BinarySearch.findPattern(targetArray, patternArray, 4)));
        }

        [TestMethod]
        public void Test_Pattern_Two_Char_Match_At_End_Start_At_Match()
        {
            byte[] targetArray = { 1, 2, 3, 4, 12 };
            byte[] patternArray = { 4, 12 };

            Assert.AreEqual(3, (BinarySearch.findPattern(targetArray, patternArray, 3)));
        }
    }
}
