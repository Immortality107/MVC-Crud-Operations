using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class TIN_Updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TIN",
                table: "Persons",
                newName: "TaxIdentificationNumber");

            migrationBuilder.AlterColumn<string>(
                name: "TaxIdentificationNumber",
                table: "Persons",
                type: "varchar(8)",
                maxLength: 8,
                nullable: true,
                defaultValue: "ABDD1234",
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("12ee0c3b-9a58-49f9-ba28-d494d3196740"),
                column: "TaxIdentificationNumber",
                value: "ABDD1234");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("20e135ca-1af4-41c5-8b34-8cbc76776de9"),
                column: "TaxIdentificationNumber",
                value: "ABDD1234");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("2172d164-5b51-4f5d-a8d9-9a56c388a701"),
                column: "TaxIdentificationNumber",
                value: "ABDD1234");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("28d11936-9466-4a4b-b9c5-2f0a8e0cbde9"),
                column: "TaxIdentificationNumber",
                value: "ABDD1234");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("31781573-707a-4f83-84c4-3f244d8f1117"),
                column: "TaxIdentificationNumber",
                value: "ABDD1234");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("5fcce0df-63cd-4e9d-8918-c2e1dca8f3a6"),
                column: "TaxIdentificationNumber",
                value: "ABDD1234");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("6731016c-33b2-4282-bffd-eeaef9eaa52d"),
                column: "TaxIdentificationNumber",
                value: "ABDD1234");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("798c7e3a-b8ee-4761-81dd-6d0139d07d89"),
                column: "TaxIdentificationNumber",
                value: "ABDD1234");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("a3b9833b-8a4d-43e9-8690-61e08df81a9a"),
                column: "TaxIdentificationNumber",
                value: "ABDD1234");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("ae4f5f03-72d5-4c26-b078-bd1b6de922a5"),
                column: "TaxIdentificationNumber",
                value: "ABDD1234");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("b689bf81-edde-41a5-958b-33c7dfa90beb"),
                column: "TaxIdentificationNumber",
                value: "ABDD1234");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("f17f8815-e060-48ff-be59-f0257d216025"),
                column: "TaxIdentificationNumber",
                value: "ABDD1234");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxIdentificationNumber",
                table: "Persons",
                newName: "TIN");

            migrationBuilder.AlterColumn<string>(
                name: "TIN",
                table: "Persons",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldMaxLength: 8,
                oldNullable: true,
                oldDefaultValue: "ABDD1234");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("12ee0c3b-9a58-49f9-ba28-d494d3196740"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("20e135ca-1af4-41c5-8b34-8cbc76776de9"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("2172d164-5b51-4f5d-a8d9-9a56c388a701"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("28d11936-9466-4a4b-b9c5-2f0a8e0cbde9"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("31781573-707a-4f83-84c4-3f244d8f1117"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("5fcce0df-63cd-4e9d-8918-c2e1dca8f3a6"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("6731016c-33b2-4282-bffd-eeaef9eaa52d"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("798c7e3a-b8ee-4761-81dd-6d0139d07d89"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("a3b9833b-8a4d-43e9-8690-61e08df81a9a"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("ae4f5f03-72d5-4c26-b078-bd1b6de922a5"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("b689bf81-edde-41a5-958b-33c7dfa90beb"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("f17f8815-e060-48ff-be59-f0257d216025"),
                column: "TIN",
                value: null);
        }
    }
}
