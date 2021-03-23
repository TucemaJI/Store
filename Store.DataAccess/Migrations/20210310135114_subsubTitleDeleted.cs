using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Store.DataAccess.Migrations
{
    public partial class subsubTitleDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubSubtitle",
                table: "PrintingEditions");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationData",
                value: new DateTime(2021, 3, 10, 13, 51, 13, 107, DateTimeKind.Utc).AddTicks(1962));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationData",
                value: new DateTime(2021, 3, 10, 13, 51, 13, 107, DateTimeKind.Utc).AddTicks(4973));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationData",
                value: new DateTime(2021, 3, 10, 13, 51, 13, 107, DateTimeKind.Utc).AddTicks(7488));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubSubtitle",
                table: "PrintingEditions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationData",
                value: new DateTime(2021, 3, 10, 13, 46, 6, 409, DateTimeKind.Utc).AddTicks(4543));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationData",
                value: new DateTime(2021, 3, 10, 13, 46, 6, 409, DateTimeKind.Utc).AddTicks(7490));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationData",
                value: new DateTime(2021, 3, 10, 13, 46, 6, 409, DateTimeKind.Utc).AddTicks(9979));
        }
    }
}
