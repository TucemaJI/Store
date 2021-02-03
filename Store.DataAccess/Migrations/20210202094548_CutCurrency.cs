using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Store.DataAccess.Migrations
{
    public partial class CutCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationData",
                value: new DateTime(2021, 2, 2, 9, 36, 20, 21, DateTimeKind.Utc).AddTicks(1745));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationData",
                value: new DateTime(2021, 2, 2, 9, 36, 20, 21, DateTimeKind.Utc).AddTicks(4764));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationData",
                value: new DateTime(2021, 2, 2, 9, 36, 20, 21, DateTimeKind.Utc).AddTicks(6981));
        }
    }
}
