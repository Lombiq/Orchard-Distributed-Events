using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lombiq.Hosting.DistributedSignals.Models;
using Orchard.Data.Migration;

namespace Lombiq.Hosting.DistributedSignals.Migrations
{
    public class DistributedSignalRecordMigrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable(typeof(DistributedSignalRecord).Name,
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<string>("Name", column => column.WithLength(255).NotNull())
                    .Column<string>("MachineName", column => column.WithLength(255).NotNull()) // Should be max 63 characters, but who knows (http://stackoverflow.com/a/4097324/220230)
                    .Column<string>("Context", column => column.Unlimited())
                ).AlterTable(typeof(DistributedSignalRecord).Name,
                table => table
                    .CreateIndex("Signal", "Name")
                );


            return 1;
        }
    }
}