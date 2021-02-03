using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Store.DataAccess.Migrations
{
    public partial class SomeMagic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReturnedCurrency",
                table: "PrintingEditions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SubSubtitle",
                table: "PrintingEditions",
                nullable: false,
                defaultValue: "SubSubTitle");

            migrationBuilder.AddColumn<string>(
                name: "Subtitle",
                table: "PrintingEditions",
                nullable: false,
                defaultValue: "SubTitle");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationData",
                value: new DateTime(2021, 2, 2, 11, 27, 10, 280, DateTimeKind.Utc).AddTicks(4306));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreationData", "ReturnedCurrency" },
                values: new object[] { new DateTime(2021, 2, 2, 11, 27, 10, 280, DateTimeKind.Utc).AddTicks(7855), 1 });

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreationData", "ReturnedCurrency" },
                values: new object[] { new DateTime(2021, 2, 2, 11, 27, 10, 281, DateTimeKind.Utc).AddTicks(976), 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnedCurrency",
                table: "PrintingEditions");

            migrationBuilder.DropColumn(
                name: "SubSubtitle",
                table: "PrintingEditions");

            migrationBuilder.DropColumn(
                name: "Subtitle",
                table: "PrintingEditions");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationData",
                value: new DateTime(2021, 2, 2, 9, 45, 47, 258, DateTimeKind.Utc).AddTicks(8387));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationData",
                value: new DateTime(2021, 2, 2, 9, 45, 47, 259, DateTimeKind.Utc).AddTicks(1823));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationData",
                value: new DateTime(2021, 2, 2, 9, 45, 47, 259, DateTimeKind.Utc).AddTicks(4367));
        }
    }
}
