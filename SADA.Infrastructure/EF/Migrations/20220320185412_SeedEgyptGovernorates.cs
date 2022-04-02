global using Newtonsoft.Json;
global using SADA.Core.Settings;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SADA.Infrastructure.EF.Migrations
{
    public partial class SeedEgyptGovernorates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            StreamReader r = new StreamReader(@"..\SADA.Infrastructure\DataSources\EgyptGovernorates.json");
            string jsonString = r.ReadToEnd();
            var data = JsonConvert.DeserializeObject<List<GovernorateData>>(jsonString);

            foreach (var obj in data)
            {
                migrationBuilder.InsertData(
                table: "Governorates",
                columns: new[] {  "Id", "Name", "ShippingFees", "CreatedAt" },
                values: new object[] { obj.id, obj.governorate_name_en, 50.0, DateTime.UtcNow}
                );
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Governorates]");
        }
    }
}
