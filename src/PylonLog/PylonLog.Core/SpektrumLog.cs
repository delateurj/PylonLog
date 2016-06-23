using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PylonLog.Core
{
    public class SpektrumLog
    {
        public byte[] rawData;
        
        public void populateRawDataFromFile(string theFilePath )
        {
            try
            {
                rawData = File.ReadAllBytes(theFilePath);
            }
            catch(Exception ex)
            {
                rawData = null;
            }
        }
    }
}
