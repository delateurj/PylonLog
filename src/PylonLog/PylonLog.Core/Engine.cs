using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PylonLog.Core
{
    [ImplementPropertyChanged]
    public class Engine
    { 
        public Engine()
        {
            this.engineLogEntries = new ObservableCollection<EngineLogEntry>();
        }

        public Engine(string serialNumber, string engineType)
        {
            this.serialNumber = serialNumber;
            this.engineType = engineType;
        }

        public int engineID { get; set; }

        public string serialNumber { get; set; }

        public string engineType { get; set; }

        public virtual ObservableCollection<EngineLogEntry>engineLogEntries { get; set; }
    }

    [ImplementPropertyChanged]
    public class EngineLogEntry
    {
        public EngineLogEntry()
        {

        }

        public int engineID { get; set; }

        public virtual Engine Engine { get; set; }

        public int engineLogEntryID { get; set; }

        public DateTime dateTime { get; set; }

        public string entryType { get; set; }

        public string text { get; set; }
    }
}
