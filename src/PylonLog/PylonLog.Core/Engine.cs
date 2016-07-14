using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PylonLog.Core
{
    [ImplementPropertyChanged]
    class Engine
    { 
        public Engine()
        {

        }

        public int engineID { get; set; }
    }

    [ImplementPropertyChanged]
    class EngineLogEntry
    {
        public EngineLogEntry()
        {

        }

        public int engineLogEntryI { get; set; }

        public DateTime dateTime { get; set; }

        public string entryType { get; set; }

        public string text { get; set; }
    }
}
