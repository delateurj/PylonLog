using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PylonLog.Utilities;

namespace PylonLog.Core
{
    public class LogSession
    {
        public byte[] rawData;

        public string planeName { get; set; }

        public int duration { get; set; }

        public byte[] mainHeader;

        public List<byte[]> supplementalHeaders = new List<byte[]>();

        public List<DataBlock> dataBlocks = new List<DataBlock>();

        public void poplulateNameFromMainHeader()
        {
            int nextCharIndex = 0 + Constants.PLANE_NAME_OFFSET;

            while (mainHeader[nextCharIndex] != 0)
            {
                planeName = planeName + Convert.ToChar(mainHeader[nextCharIndex]);

                nextCharIndex++;
            }
        }

        public void createDataBlocksFromRawData()
        {
            int indexOfStartOfDataBlocks = Constants.HEADER_BLOCK_LENGTH + Constants.HEADER_BLOCK_LENGTH * supplementalHeaders.Count;

            int numberOfDataBlocks = (rawData.Length - indexOfStartOfDataBlocks) / Constants.DATA_BLOCK_LENGTH;

            for(int i=0; i<numberOfDataBlocks;i++)
            {
                DataBlock nextDataBlock = new DataBlock();

                nextDataBlock.rawData = rawData.Slice(indexOfStartOfDataBlocks + i * Constants.DATA_BLOCK_LENGTH, indexOfStartOfDataBlocks + (i + 1) * Constants.DATA_BLOCK_LENGTH);
        
                nextDataBlock.populateTimeStampFromRawData();

                nextDataBlock.populateDataTypeAndValueFromRawData();

                dataBlocks.Add(nextDataBlock);
            }

            if(dataBlocks.Count>0)
            {
                duration = dataBlocks.Last().timeStamp - dataBlocks.First().timeStamp;
            }
            else
            {
                duration = 0;
            }
        }
    }
}
