using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SADA.Infrastructure.EF.Migrations
{
    public partial class SeedEgyptCities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            StreamReader r = new StreamReader(@"..\SADA.Infrastructure\DataSources\EgyptCities.json");
            string jsonString = r.ReadToEnd();
            var data = JsonConvert.DeserializeObject<List<CityData>>(jsonString);

            foreach (var obj in data)
            {
                migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "GovernorateId", "CreatedAt" },
                values: new object[] { obj.id, obj.city_name_en, obj.governorate_id, DateTime.UtcNow }
                );
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Cities]");
        }
    }
}
