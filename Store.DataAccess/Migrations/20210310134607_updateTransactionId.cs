using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Store.DataAccess.Migrations
{
    public partial class updateTransactionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SubtitleReturned",
                table: "PrintingEditions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "SubTitle");

            migrationBuilder.AlterColumn<string>(
                name: "SubSubtitle",
                table: "PrintingEditions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "SubSubTitle");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionId",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SubtitleReturned",
                table: "PrintingEditions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "SubTitle",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "SubSubtitle",
                table: "PrintingEditions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "SubSubTitle",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "TransactionId",
                table: "Payments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
