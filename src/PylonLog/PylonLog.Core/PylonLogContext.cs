using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PylonLog.Core
{
    public class PylonLogContext : DbContext
    { 

        public DbSet<PylonLogEntry> pylonLogEntries { get; set; }

        public DbSet<DataBlock> dataBlocks { get; set; }
    }
}
