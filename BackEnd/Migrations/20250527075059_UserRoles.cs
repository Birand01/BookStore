using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class UserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "681e712f-6a12-4f7f-89fc-82b4c1140c0a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df3c0d8c-0b89-4caa-a916-74cbbcaca978");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4d16fea-5d25-4435-985a-70734698dece");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "048b63c7-ee52-4447-8ce7-a941b040f971", null, "Administrator", "ADMINISTRATOR" },
                    { "7a3dd7d1-7471-445d-b62d-fbed9051135f", null, "User", "USER" },
                    { "e3ff68ab-cffb-4636-9c55-b0b152c1c513", null, "Editor", "EDITOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "048b63c7-ee52-4447-8ce7-a941b040f971");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a3dd7d1-7471-445d-b62d-fbed9051135f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3ff68ab-cffb-4636-9c55-b0b152c1c513");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "681e712f-6a12-4f7f-89fc-82b4c1140c0a", null, "User", "USER" },
                    { "df3c0d8c-0b89-4caa-a916-74cbbcaca978", null, "Editor", "EDITOR" },
                    { "e4d16fea-5d25-4435-985a-70734698dece", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
