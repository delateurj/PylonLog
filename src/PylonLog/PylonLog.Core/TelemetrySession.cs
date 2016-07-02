using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PylonLog.Utilities;

namespace PylonLog.Core
{
    public class TelemetrySession
    {
        public byte[] rawData;

        public string planeName { get; set; }

        public DateTime timeOfLog { get; set; }

        public int duration { get; set; }

        public byte[] mainHeader;

        public List<byte[]> supplementalHeaders = new List<byte[]>();

        public List<DataBlock> dataBlocks = new List<DataBlock>();

        public override String ToString()
        {
            return planeName + " : " + duration;
        }

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

            for (int i = 0; i < numberOfDataBlocks; i++)
            {
                DataBlock nextDataBlock = new DataBlock();

                nextDataBlock.rawData = rawData.Slice(indexOfStartOfDataBlocks + i * Constants.DATA_BLOCK_LENGTH, indexOfStartOfDataBlocks + (i + 1) * Constants.DATA_BLOCK_LENGTH);

                nextDataBlock.populateTimeStampFromRawData();

                nextDataBlock.populateDataTypeAndValueFromRawData();

                dataBlocks.Add(nextDataBlock);
            }

            if (dataBlocks.Count > 0)
            {
                duration = dataBlocks.Last().timeStamp - dataBlocks.First().timeStamp;
            }
            else
            {
                duration = 0;
            }
        }


        public List<Double[]> getSelectedDataBlocks(string dataType)
        {

            List<DataBlock> list = new List<DataBlock>();
           
            foreach (DataBlock dataBlock in dataBlocks)
            {
                if(dataBlock.dataType == dataType)
                {
                    list.Add(dataBlock);
                }
               
            }

            List<Double[]> result = new List<Double[]>();

            Double[] timeStamps = new Double[list.Count];
            Double[] values = new Double[list.Count];

            for (int i=0; i < list.Count ; i++)
            {
                timeStamps[i] = (double)(list[i].timeStamp)/(double)100;
                values[i] = list[i].dataValue;
            }

            result.Add(timeStamps);
            result.Add(values);

            return result;
        }
    }
}
