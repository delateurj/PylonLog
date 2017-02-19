using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PylonLog.Utilities;
using System.Collections.ObjectModel;

namespace PylonLog.Core
{
    public class SpektrumLog
    {
        public byte[] rawData;

        public ObservableCollection<TelemetrySession> logSessions = new ObservableCollection<TelemetrySession>();

        public SpektrumLog(string theFilePath)
        {
            populateRawDataFromFile(theFilePath);
            createSessionLogs();
        }

        public SpektrumLog()
        {

        }

        public void populateRawDataFromFile(string theFilePath)
        {
            try
            {
                rawData = File.ReadAllBytes(theFilePath);
            }
            catch (Exception ex)
            {
                rawData = null;
            }
        }



        public void createSessionLogs()
        {
            int indexOfNextLogSession = findNextSessionLog(0); 

            while (indexOfNextLogSession > -1)
            {
                int indexOfLogSession = indexOfNextLogSession;

                TelemetrySession logSession = new TelemetrySession();

                logSession.mainHeader = rawData.Slice(indexOfLogSession, indexOfLogSession + Constants.HEADER_BLOCK_LENGTH);

                logSession.poplulateNameFromMainHeader();

                int indexOfLastSupplementalHeader = findLastSupplmentalHeader(indexOfLogSession);

                logSession.indexOfStartOfDataBlocks = indexOfLastSupplementalHeader + Constants.HEADER_BLOCK_LENGTH - indexOfLogSession;

                indexOfNextLogSession = findNextSessionLog(indexOfLastSupplementalHeader + Constants.HEADER_BLOCK_LENGTH);

                if (indexOfNextLogSession > -1)
                {
                    logSession.rawData = rawData.Slice(indexOfLogSession, indexOfNextLogSession);
                }
                else
                {
                    logSession.rawData = rawData.Slice(indexOfLogSession, rawData.Length);
                }

                logSession.createDataBlocksFromRawData();

                logSessions.Add(logSession);
            }
        }

        public int findNextSessionLog(int startIndex)
        {
            int result = -1;

            int lastHeaderOfNextLogIndex = -1;

            lastHeaderOfNextLogIndex = findLastSupplmentalHeader(startIndex);

            if (lastHeaderOfNextLogIndex == -1)
            {
                result = -1;
            }
            else
            {
                result = findIndexOfSessionStartFromLastHeaderIndex(lastHeaderOfNextLogIndex);
            }

            return result;
        }

        public int findLastSupplmentalHeader(int startIndex)
        {
            int result = -1;

            result = BinarySearch.findPattern(rawData, Constants.LAST_SUPPLEMENTAL_HEADER_PATTERN, startIndex);

            return result;
        }

        public int findIndexOfSessionStartFromLastHeaderIndex(int indexOfLastSupplmentalHeader)
        {
            int result = -1;

            Boolean notYetFound = true;

            int indexOfEndOfPreviousHeaderStart = BinarySearch.findPatternReverse(rawData, Constants.NEW_SESSION_LOG_PATTERN, indexOfLastSupplmentalHeader);

            while (indexOfEndOfPreviousHeaderStart != -1 && notYetFound)
            {
                if (rawData[indexOfEndOfPreviousHeaderStart + 2] == 0x00)
                {
                    result = indexOfEndOfPreviousHeaderStart - 3;

                    notYetFound = false;
                }
                else
                {
                    indexOfEndOfPreviousHeaderStart = 
                        BinarySearch.findPatternReverse(
                            rawData, 
                            Constants.NEW_SESSION_LOG_PATTERN, 
                            indexOfEndOfPreviousHeaderStart-3);
                }
            }

            return result;
        }


        public void writeToTextFile(string theFilePath)
        {
            using (StreamWriter writer = new StreamWriter(theFilePath))
            {
                foreach (TelemetrySession logSession in logSessions)
                {
                    writer.WriteLine(logSession.planeName);

                    writer.WriteLine(logSession.duration / 6000 + ":" + (logSession.duration % 6000) / 100 + "." + logSession.duration % 6000 % 100);

                    writer.WriteLine(logSession.dataBlocks.Count);

                    foreach (DataBlock dataBlock in logSession.dataBlocks)
                    {
                        writer.WriteLine(dataBlock.timeStamp - logSession.dataBlocks[0].timeStamp + "," + dataBlock.dataType + "," + dataBlock.dataValue);
                    }
                }
            }
        }
    }
}
