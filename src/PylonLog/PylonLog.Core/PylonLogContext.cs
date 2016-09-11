using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EfEnumToLookup.LookupGenerator;

namespace PylonLog.Core
{
    public class PylonLogInitializer : CreateDatabaseIfNotExists<PylonLogContext>
    {
        protected override void Seed(PylonLogContext context)
        {
            context.props.Add(new Prop("8.8x8.75"));
            context.props.Add(new Prop("8.8x9.0"));
            context.props.Add(new Prop("8.8x9.25"));
            context.props.Add(new Prop("7.4x7.5"));
            context.props.Add(new Prop("7.4x7.6"));
            context.props.Add(new Prop("9x6"));
            context.props.Add(new Prop("Other"));

            context.plugs.Add(new GlowPlug("Nelson"));
            context.plugs.Add(new GlowPlug("Merlin Red"));
            context.plugs.Add(new GlowPlug("Merlin Green"));
            context.plugs.Add(new GlowPlug("Merlin Black"));
            context.plugs.Add(new GlowPlug("Globee Style"));
            context.plugs.Add(new GlowPlug("Other"));

            context.engines.Add(new Engine("Nelson I", "Q40"));
            context.engines.Add(new Engine("Jett I", "426"));

            base.Seed(context);
        }
    }
    public class PylonLogContext : DbContext
    {
        private static string SQLServerLocal2012 = @"Data Source=(LocalDb)\v11.0;Initial Catalog=PylonLogDB2012LocalDBr2;Integrated Security=SSPI;";
        private static string SQLServerLocal2014 = @"Data Source = (localdb)\mssqllocaldb; Initial Catalog = PylonLogDB2014LocalDB; Integrated Security = SSPI; MultipleActiveResultSets=True";
        private static string SQLServerLocal2012Prod2 = @"Data Source=(LocalDb)\v11.0;Initial Catalog=PylonLogDB2012LocalDBProd2;Integrated Security=SSPI;";
      
        public PylonLogContext() : base(SQLServerLocal2012Prod2)
        {
            Database.SetInitializer(new PylonLogInitializer());
        }
        public DbSet<PylonLogEntry> pylonLogEntries { get; set; }

        public DbSet<DataBlock> dataBlocks { get; set; }

        public DbSet<Prop> props { get; set; }

        public DbSet<GlowPlug> plugs { get; set; }

        public DbSet<Engine> engines { get; set; }
    }
}
