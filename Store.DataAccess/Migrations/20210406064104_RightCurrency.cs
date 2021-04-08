using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Store.DataAccess.Migrations
{
    public partial class RightCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReturnedCurrency",
                table: "PrintingEditions",
                newName: "Currency");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationData",
                value: new DateTime(2021, 4, 6, 6, 41, 3, 777, DateTimeKind.Utc).AddTicks(5311));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationData",
                value: new DateTime(2021, 4, 6, 6, 41, 3, 777, DateTimeKind.Utc).AddTicks(8284));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationData",
                value: new DateTime(2021, 4, 6, 6, 41, 3, 778, DateTimeKind.Utc).AddTicks(971));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "PrintingEditions",
                newName: "ReturnedCurrency");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationData",
                value: new DateTime(2021, 3, 11, 10, 23, 9, 809, DateTimeKind.Utc).AddTicks(8834));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationData",
                value: new DateTime(2021, 3, 11, 10, 23, 9, 810, DateTimeKind.Utc).AddTicks(1933));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationData",
                value: new DateTime(2021, 3, 11, 10, 23, 9, 810, DateTimeKind.Utc).AddTicks(4510));
        }
    }
}
