using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace PylonLog.Core
{
    public class PylonLogEntry
    {
        public PylonLogEntry()
        {
            this.DataBlocks = new ObservableCollection<DataBlock>();
        }

        public int pylonLogEntryID { get; set; }

        public string planeName { get; set; }
 
        public int engineID { get; set; }

        public string entryType { get; set; }

        public int telemetryDuration { get; set; }

        public virtual ObservableCollection<DataBlock> DataBlocks { get; private set; }
    }
}
