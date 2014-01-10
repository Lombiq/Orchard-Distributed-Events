using Lombiq.Hosting.DistributedEvents.Models;
using Orchard.Data.Migration;

namespace Lombiq.Hosting.DistributedEvents.Migrations
{
    public class DistributedEventRecordMigrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable(typeof(DistributedEventRecord).Name,
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<string>("Name", column => column.WithLength(255).NotNull())
                    .Column<string>("MachineName", column => column.WithLength(255).NotNull()) // Should be max 63 characters, but who knows (http://stackoverflow.com/a/4097324/220230)
                    .Column<string>("Context", column => column.Unlimited())
                ).AlterTable(typeof(DistributedEventRecord).Name,
                table => table
                    .CreateIndex("EventsForMachine", "Id", "MachineName")
                );


            return 1;
        }
    }
}