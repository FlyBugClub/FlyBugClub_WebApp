using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlyBugClub_WebApp.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b5201670-1b0c-4896-9a78-715e4cc249b2", "986b9853-6844-4c5e-a0b3-734bb71f30ef", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "Faculty", "FullName", "Gender", "LockoutEnabled", "LockoutEnd", "Major", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "PositionID", "SecurityStamp", "TwoFactorEnabled", "UID", "UserName" },
                values: new object[] { "42945622-e4eb-4ae8-8293-0793a445b772", 0, "123/15/8 Nguyễn Hữu Cảnh", "2a8a7167-6737-480b-ab54-da7698b3c861", "admin@gmail.com", false, null, "Admin", null, false, null, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEAGGNx3uB1QRzxOG/k0ZXSzKyDCekhM5OcmSYml5EdL9hhBS3NtwoKJ0HIFhvs/++w==", null, "1234567890", false, null, "3f93a871-fa9f-4411-8c00-fe5c9cfa07a2", false, null, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b5201670-1b0c-4896-9a78-715e4cc249b2", "42945622-e4eb-4ae8-8293-0793a445b772" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b5201670-1b0c-4896-9a78-715e4cc249b2", "42945622-e4eb-4ae8-8293-0793a445b772" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5201670-1b0c-4896-9a78-715e4cc249b2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42945622-e4eb-4ae8-8293-0793a445b772");
        }
    }
}
