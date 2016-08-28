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
            int indexOfNextLogSession = BinarySearch.findPattern(rawData, Utilities.Constants.NEW_SESSION_LOG_PATTERN);

            int indexOfNextSupplementalHeader;

            while (indexOfNextLogSession > -1)
            {
                int indexOfLogSession = indexOfNextLogSession;

                TelemetrySession logSession = new TelemetrySession();

                logSession.mainHeader = rawData.Slice(indexOfLogSession, indexOfLogSession + Constants.HEADER_BLOCK_LENGTH);

                logSession.poplulateNameFromMainHeader();

                indexOfNextSupplementalHeader = parseSupplementalHeaders(indexOfLogSession, logSession);

                indexOfNextLogSession = BinarySearch.findPattern(rawData, Constants.NEW_SESSION_LOG_PATTERN, indexOfNextSupplementalHeader + Constants.HEADER_BLOCK_LENGTH);

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

                if(logSession.planeName == "Miss Dara" && logSession.dataBlocks.Count == 10125 )
                {
                    int breakVariable = 0;
                }
            }
        }


        public int parseSupplementalHeaders(int indexOfLogSession, TelemetrySession logSession)
        {
            int indexOfNextSupplementalHeader = BinarySearch.findPattern(rawData, Constants.NEW_SESSION_LOG_PATTERN, indexOfLogSession + Constants.HEADER_BLOCK_LENGTH);

            logSession.supplementalHeaders.Add(rawData.Slice(indexOfNextSupplementalHeader, indexOfNextSupplementalHeader + Constants.HEADER_BLOCK_LENGTH));

            while (rawData[indexOfNextSupplementalHeader + 4] != 0x17 || rawData[indexOfNextSupplementalHeader + 5] != 0x17)
            {
                indexOfNextSupplementalHeader = BinarySearch.findPattern(rawData, Constants.NEW_SESSION_LOG_PATTERN, indexOfNextSupplementalHeader + Constants.HEADER_BLOCK_LENGTH);

                logSession.supplementalHeaders.Add(rawData.Slice(indexOfNextSupplementalHeader, indexOfNextSupplementalHeader + Constants.HEADER_BLOCK_LENGTH));
            }

            return indexOfNextSupplementalHeader;
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
