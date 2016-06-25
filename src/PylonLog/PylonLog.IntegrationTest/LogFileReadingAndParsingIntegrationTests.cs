using System;
using NUnit.Framework;
using PylonLog.Core;
using System.IO;

namespace PylonLog.IntegrationTest
{
    [TestFixture]
    public class LogFileReadingAndParsingIntegrationTests
    {
        string integrationTestLog = "C:\\Users\\djoe\\Dropbox\\Programming\\PylonLog\\TestData\\Log.TLM";

        string integrationBadFileName = "C:\\Users\\djoe\\Dropbox\\Programming\\PylonLog\\TestData\\NoLog.TLM";

        string integrationTestWriteResult = "C:\\Users\\djoe\\Dropbox\\Programming\\PylonLog\\TestData\\ConvertedText.txt";

        string integrationTestExpectedResult = "C:\\Users\\djoe\\Dropbox\\Programming\\PylonLog\\TestData\\converted.txt";


        [Test]
        public void TestReadingSampleLogFileAndPopulatingArray()
        {
            SpektrumLog spektrumLog = new SpektrumLog();

            Console.WriteLine(Directory.GetCurrentDirectory());

            spektrumLog.populateRawDataFromFile(integrationTestLog);

            Assert.AreEqual(4959676, spektrumLog.rawData.Length);   
        }


        [Test]
        public void TestReadingSampleLogFileAndPopulatingArrayBadFileName()
        {
            SpektrumLog spektrumLog = new SpektrumLog();

            spektrumLog.populateRawDataFromFile(integrationBadFileName);

            Assert.IsNull(spektrumLog.rawData);
        }


        [Test]
        public void TestParsingFile()
        {
            SpektrumLog spektrumLog = new SpektrumLog(integrationTestLog);

            spektrumLog.writeToTextFile(integrationTestWriteResult);

            FileAssert.AreEqual(integrationTestWriteResult, integrationTestExpectedResult);
        }
    }
}
