using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAccess.Migrations
{
    public partial class SomeMagicLast : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubtitleReturned",
                table: "PrintingEditions",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationData",
                value: new DateTime(2021, 2, 2, 12, 21, 32, 905, DateTimeKind.Utc).AddTicks(9509));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreationData", "SubtitleReturned" },
                values: new object[] { new DateTime(2021, 2, 2, 12, 21, 32, 906, DateTimeKind.Utc).AddTicks(2680), "Subtitle2" });

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreationData", "SubtitleReturned" },
                values: new object[] { new DateTime(2021, 2, 2, 12, 21, 32, 906, DateTimeKind.Utc).AddTicks(5495), "Subtitle2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubtitleReturned",
                table: "PrintingEditions");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationData",
                value: new DateTime(2021, 2, 2, 11, 33, 40, 749, DateTimeKind.Utc).AddTicks(6063));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationData",
                value: new DateTime(2021, 2, 2, 11, 33, 40, 749, DateTimeKind.Utc).AddTicks(9332));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationData",
                value: new DateTime(2021, 2, 2, 11, 33, 40, 750, DateTimeKind.Utc).AddTicks(2111));
        }
    }
}
