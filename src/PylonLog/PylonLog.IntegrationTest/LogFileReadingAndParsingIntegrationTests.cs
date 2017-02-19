using System;
using System.Windows;
using NUnit.Framework;
using PylonLog.Core;
using System.IO;
using PylonLog.Utilities;


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
        public void TestFindTopHeader()
        {
            SpektrumLog spektrumLog = new SpektrumLog();

            Console.WriteLine(Directory.GetCurrentDirectory());

            spektrumLog.populateRawDataFromFile(integrationTestLog);

            int indexOfNextLogSession = spektrumLog.findNextSessionLog(0);

            Assert.AreEqual(0, indexOfNextLogSession);

            int indexOfLogSession = indexOfNextLogSession;

            TelemetrySession logSession = new TelemetrySession();

            logSession.mainHeader = spektrumLog.rawData.Slice(indexOfLogSession, indexOfLogSession + Constants.HEADER_BLOCK_LENGTH);

            logSession.poplulateNameFromMainHeader();

            Assert.AreEqual(logSession.planeName, "#1 Ninja #1");

            int indexOfLastSupplementalHeader = spektrumLog.findLastSupplmentalHeader(indexOfLogSession);

            Assert.AreEqual(72,indexOfLastSupplementalHeader);

            logSession.indexOfStartOfDataBlocks = indexOfLastSupplementalHeader + Constants.HEADER_BLOCK_LENGTH;

            Assert.AreEqual(108, logSession.indexOfStartOfDataBlocks);

            indexOfNextLogSession = spektrumLog.findNextSessionLog(indexOfLastSupplementalHeader + Constants.HEADER_BLOCK_LENGTH);

            Assert.AreEqual(108, indexOfNextLogSession);

            if (indexOfNextLogSession > -1)
            {
                logSession.rawData = spektrumLog.rawData.Slice(indexOfLogSession, indexOfNextLogSession);
            }
            else
            {
                logSession.rawData = spektrumLog.rawData.Slice(indexOfLogSession, spektrumLog.rawData.Length);
            }

            Assert.AreEqual(108, logSession.rawData.Length);

            logSession.createDataBlocksFromRawData();

            Assert.AreEqual(0,logSession.dataBlocks.Count);

            indexOfLogSession = indexOfNextLogSession;

            logSession = new TelemetrySession();

            logSession.mainHeader = spektrumLog.rawData.Slice(indexOfLogSession, indexOfLogSession + Constants.HEADER_BLOCK_LENGTH);

            logSession.poplulateNameFromMainHeader();

            Assert.AreEqual(logSession.planeName, "#1 Ninja #1");

            indexOfLastSupplementalHeader = spektrumLog.findLastSupplmentalHeader(indexOfLogSession);

            Assert.AreEqual(180, indexOfLastSupplementalHeader);

            logSession.indexOfStartOfDataBlocks = indexOfLastSupplementalHeader + Constants.HEADER_BLOCK_LENGTH-indexOfLogSession;

            indexOfNextLogSession = spektrumLog.findNextSessionLog(indexOfLastSupplementalHeader + Constants.HEADER_BLOCK_LENGTH);

            Assert.AreEqual(108, logSession.indexOfStartOfDataBlocks);

            Assert.AreEqual(279336, indexOfNextLogSession);

            if (indexOfNextLogSession > -1)
            {
                logSession.rawData = spektrumLog.rawData.Slice(indexOfLogSession, indexOfNextLogSession);
            }
            else
            {
                logSession.rawData = spektrumLog.rawData.Slice(indexOfLogSession, spektrumLog.rawData.Length);
            }

            Assert.AreEqual(279336-108, logSession.rawData.Length);

            logSession.createDataBlocksFromRawData();

            Assert.AreEqual(13956, logSession.dataBlocks.Count);

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

        public void testGui()
        {

        }
    }
}
