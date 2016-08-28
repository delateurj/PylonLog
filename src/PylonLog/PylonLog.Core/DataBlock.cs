using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PylonLog.Core
{
    public class DataBlock
    {
        [NotMapped]
        public byte[] rawData { get; set; }

        public long DataBlockID { get; set; }

        public int timeStamp { get; set; }

        public string dataType { get; set; }

        public int dataValue { get; set; }

        public int PylonLogEntryID { get; set; }

        public virtual PylonLogEntry pylonLogEntry { get; set; }


        public DataBlock shallowClone()
        {
            return (DataBlock)this.MemberwiseClone();
        }
        public void populateTimeStampFromRawData()
        {
            timeStamp = BitConverter.ToInt32(rawData, 0);
        }

        public void populateDataTypeAndValueFromRawData()
        {
            if (rawData[4] == 127 || rawData[4] == 255)
            {
                dataType = "RX-VOLT";

                dataValue = rawData[18] * 256 + rawData[19];
            }
           else if (rawData[4] == 126 || rawData[4] == 254)  //ToDo: Figure out why rpm data is not always data type 126 (e.g. on Miss Dara)
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
