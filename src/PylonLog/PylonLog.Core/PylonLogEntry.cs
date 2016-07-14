using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using PropertyChanged;

namespace PylonLog.Core
{
    [ImplementPropertyChanged]
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

        public bool excludeFromStats { get; set; }

        public int telemetryDuration { get; set; }

        public int launchTimeStamp { get; set; }

        public int endTimeStamp { get; set; }

        public int peakRPMOnLine { get; set; }

        public int launchRPM { get; set; }

        public int avgRPM { get; set; }

        public int avgPeakRPM { get; set; }

        public int plugColor { get; set; }

        public string relativeNeedle { get; set; }

        public bool newPlug { get; set; }

        public string notes { get; set; }

        public int temperature { get; set; }

        public int humidity { get; set; }

        public virtual Prop prop { get; set; }

        public GlowPlug plugType { get; set; }

        public virtual ObservableCollection<DataBlock> DataBlocks { get; set; }

        public double averageOfSpecifiedValueType(string valueType, int start)
        {
            return averageOfSpecifiedValueType(valueType, start, DataBlocks.ToList().Where(x => x.dataType == valueType).Last().timeStamp);
        }
        public double averageOfSpecifiedValueType(string valueType)
        {
            return averageOfSpecifiedValueType(valueType, 0, DataBlocks.ToList().Where(x=>x.dataType==valueType).Last().timeStamp);
        }

        public double averageOfSpecifiedValueType(string valueType, int start, int end)
        {
            List<DataBlock> listOfDataBlocksToAverage = DataBlocks?.Where(x => x.dataType == valueType && x.timeStamp >= start && x.timeStamp <= end).ToList();

            if (listOfDataBlocksToAverage.Count > 0)
            {
                return listOfDataBlocksToAverage?.Average(x => x.dataValue) ?? 0;
            }
            else
            {
                return 0;
            }
        }
    }

    public class Prop
    {
        public int PropID { get; set; }

        public string name { get; set; }

        //Must have empty constructor for Entity Framework
        public Prop()
        {

        }

        public Prop(string name)
        {
            this.name = name;
        }

    }

    public class GlowPlug
    {
        public int GlowPlugID { get; set; }
        public string name { get; set; }

        //Must have empty constructor for Enity Framework.
        public GlowPlug()
        {

        }
        public GlowPlug(string name)
        {
            this.name = name;
        }


       
    }

}
