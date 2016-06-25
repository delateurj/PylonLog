using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PylonLog.Core
{
    public class DataBlock
    {
        public byte[] rawData { get; set; }

        public int timeStamp { get; set; }

        public string dataType { get; set; }

        public int dataValue { get; set; }


        public void populateTimeStampFromRawData()
        {
            timeStamp = BitConverter.ToInt32(rawData, 0);
        }


        public void populateDataTypeAndValueFromRawData()
        {
            if (rawData[4] == 127)
            {
                dataType = "RX-VOLT";

                dataValue = rawData[18] * 256 + rawData[19];
            }
            else if (rawData[4] == 126)
            {
                dataType = "RPM";

                dataValue = rawData[6] * 256 + rawData[7];

                if (dataValue == 65535 || dataValue == 0)
                {
                    dataValue = 0;
                }
                else
                {
                    dataValue = 60000000 / dataValue;
                }
            }
            else
            {
                dataType = "None";
                dataValue = 0;
            }
        }


    }


}
