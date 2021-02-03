using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Store.DataAccess.Migrations
{
    public partial class SomeMagicLastest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SubtitleReturned",
                table: "PrintingEditions",
                nullable: false,
                defaultValue: "SubTitle",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationData",
                value: new DateTime(2021, 2, 2, 12, 34, 7, 75, DateTimeKind.Utc).AddTicks(7425));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationData",
                value: new DateTime(2021, 2, 2, 12, 34, 7, 76, DateTimeKind.Utc).AddTicks(448));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationData",
                value: new DateTime(2021, 2, 2, 12, 34, 7, 76, DateTimeKind.Utc).AddTicks(3003));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SubtitleReturned",
                table: "PrintingEditions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldDefaultValue: "SubTitle");

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
    }
}
