using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SADA.Infrastructure.EF.Migrations
{
    public partial class SeedPaymentMethods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            StreamReader r = new StreamReader(@"..\SADA.Infrastructure\DataSources\PaymentMethods.json");
            string jsonString = r.ReadToEnd();
            var data = JsonConvert.DeserializeObject<List<PaymentMethodsData>>(jsonString);

            foreach (var obj in data)
            {
                migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Name", "CreatedAt" },
                values: new object[] { obj.id, obj.name, DateTime.UtcNow }
                );
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [PaymentMethods]");
        }
    }
}
