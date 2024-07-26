using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;
using Nbl.Plugin.Widgets.ServiceableStoreLocations.Domain;
using Nop.Data.Extensions;
using Nop.Data.Migrations;

namespace Nbl.Plugin.Widgets.ServiceableStoreLocations.Migrations
{
    [NopMigration("2024/07/26 11:59:17:6455422", "Widget.ServiceableStoreLocations base schema", MigrationProcessType.Installation)]
    public class SchemaMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.TableFor<StorePincodeMapping>();
        }

    }
}
