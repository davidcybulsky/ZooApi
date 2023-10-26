using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooApi.Migrations
{
    /// <inheritdoc />
    public partial class AssemblyConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caretakers_Addresses_AddressId",
                table: "Caretakers");

            migrationBuilder.DropIndex(
                name: "IX_Caretakers_AddressId",
                table: "Caretakers");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Caretakers");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Caretakers_Id",
                table: "Addresses",
                column: "Id",
                principalTable: "Caretakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Caretakers_Id",
                table: "Addresses");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Caretakers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Caretakers_AddressId",
                table: "Caretakers",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Caretakers_Addresses_AddressId",
                table: "Caretakers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
