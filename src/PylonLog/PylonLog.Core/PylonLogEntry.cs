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

            entryDateTime = DateTime.Now;
        }

        public int pylonLogEntryID { get; set; }

        public DateTime entryDateTime { get; set; }

        public string planeName { get; set; }
 
        public string engineID { get; set; }

        public string entryType { get; set; }

        public int telemetryDuration { get; set; }

        public string plugColor { get; set; }

        public string relativeNeedle { get; set; }

        public bool newPlug { get; set; }

        public virtual ObservableCollection<DataBlock> DataBlocks { get; private set; }
    }
}
