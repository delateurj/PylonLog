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

        //Must have empty constructor for Entity Framework
        public Engine()
        {

        }

        public Engine(string serialNumber, string engineType)
        {
            this.serialNumber = serialNumber;
            this.engineType = engineType;
        }

        public int engineID { get; set; }

        public string serialNumber { get; set; }

        public string engineType { get; set; }

        public int headHeight { get; set; }

        public int deckClearance { get; set; }

        public string timing { get; set; }

        public string notes { get; set; }
    }
}
