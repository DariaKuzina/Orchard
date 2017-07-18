using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace Skywalker.Webshop
{
    public class Migrations : DataMigrationImpl
    {
        /*When a feature is enabled, Orchard will invoke a method called "Create" on all classes that derive from
          DataMigrationImpl if the feature is not yet registered in the database or its version is 0.
          If a migration for feature was previously executed, the version for that migration will be greater than 0. 
          If the version would be 5 for example, then Orchard will invoke a method called UpateFrom5.
          From within that method, you would return a new value of 6. 
          Orchard will use that value to update the current version of the migration.*/

        public int Create()
        {

            SchemaBuilder.CreateTable("ProductPartRecord", table => table

                // The following method will create an "Id" column for us and set it is the primary key for the table
                .ContentPartRecord()

                // Create a column named "UnitPrice" of type "decimal"
                .Column<decimal>("UnitPrice")

                // Create the "Sku" column and specify a maximum length of 50 characters
                .Column<string>("Sku", column => column.WithLength(50))
                );

            // Return the version that this feature will be after this method completes
            return 1;
        }

        public int UpdateFrom1()
        {

            // Create (or alter) a part called "ProductPart" and configure it to be "attachable".
            ContentDefinitionManager.AlterPartDefinition("ProductPart", part => part
                .Attachable());

            return 2;
        }
    }
}