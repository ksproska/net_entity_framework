using Microsoft.EntityFrameworkCore.Migrations;

namespace L10_2.Migrations
{
    public partial class newNewNewNewInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentOption",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "PaymentOption",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "bank transfer");

            migrationBuilder.UpdateData(
                table: "PaymentOption",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "blick");

            migrationBuilder.UpdateData(
                table: "PaymentOption",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "cash");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PaymentOption",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Carrier");

            migrationBuilder.UpdateData(
                table: "PaymentOption",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Paczkomat");

            migrationBuilder.UpdateData(
                table: "PaymentOption",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Zabka");

            migrationBuilder.InsertData(
                table: "PaymentOption",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Kiosk" });
        }
    }
}
