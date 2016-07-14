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
            IList<Prop> defaultProps = new List<Prop>();

            defaultProps.Add(new Prop("8.8x8.75"));
            defaultProps.Add(new Prop("8.8x9.0"));
            defaultProps.Add(new Prop("8.8x9.25"));
            defaultProps.Add(new Prop("7.4x7.5"));
            defaultProps.Add(new Prop("7.4x7.6"));
            defaultProps.Add(new Prop("9x6"));
            defaultProps.Add(new Prop("Other"));

            foreach (Prop prop in defaultProps)
            {
                context.props.Add(prop);
            }

            IList<GlowPlug> defaultPlugs = new List<GlowPlug>();

            defaultPlugs.Add(new GlowPlug("Nelson"));
            defaultPlugs.Add(new GlowPlug("Merlin Red"));
            defaultPlugs.Add(new GlowPlug("Merlin Green"));
            defaultPlugs.Add(new GlowPlug("Merlin Black"));
            defaultPlugs.Add(new GlowPlug("Globee Style"));
            defaultPlugs.Add(new GlowPlug("Other"));

            foreach (GlowPlug plug in defaultPlugs)
            {
                context.plugs.Add(plug);
            }

            base.Seed(context);
        }
    }
    public class PylonLogContext : DbContext
    {
        private static string SQLServerLocal2012 = @"Data Source=(LocalDb)\v11.0;Initial Catalog=PylonLogDB2012LocalDBr2;Integrated Security=SSPI;";
        private static string SQLServerLocal2014 = @"Data Source = (localdb)\mssqllocaldb; Initial Catalog = PylonLogDB2014LocalDB; Integrated Security = SSPI; MultipleActiveResultSets=True";

        public PylonLogContext() : base(SQLServerLocal2012)
        {
            Database.SetInitializer(new PylonLogInitializer());

            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseIfModelChanges<SchoolDBContext>());
            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseAlways<SchoolDBContext>());
            //Database.SetInitializer<SchoolDBContext>(new SchoolDBInitializer());
        }
        public DbSet<PylonLogEntry> pylonLogEntries { get; set; }

        public DbSet<DataBlock> dataBlocks { get; set; }

        public DbSet<Prop> props { get; set; }

        public DbSet<GlowPlug> plugs { get; set; }
    }



}
