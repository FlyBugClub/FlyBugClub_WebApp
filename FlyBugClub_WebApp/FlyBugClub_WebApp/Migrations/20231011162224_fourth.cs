using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlyBugClub_WebApp.Migrations
{
    /// <inheritdoc />
    public partial class fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "491f951e-ae93-4714-8bb2-3fbc474df8ad", "e806f855-2acb-4a99-bc8d-42a30ec4fa30" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "491f951e-ae93-4714-8bb2-3fbc474df8ad");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e806f855-2acb-4a99-bc8d-42a30ec4fa30");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(5)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "533cbaf4-cdde-43d9-b6cc-962164c64b32", "d865b956-68ba-4213-8a8e-c225dbda16a4", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "Faculty", "FullName", "Gender", "LockoutEnabled", "LockoutEnd", "Major", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "PositionID", "SecurityStamp", "TwoFactorEnabled", "UID", "UserName" },
                values: new object[] { "ecf21b2b-66d0-4cb3-88b4-96f1b8e2625a", 0, "123/15/8 Nguyễn Hữu Cảnh", "7f60444b-ea06-42b2-b598-6f88795727dc", "admin@gmail.com", false, null, "Admin", null, false, null, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEHhlbQsAi7YYWJraea2HmljBParcGdoFpYGy5o5jpBoZglC/Xa3ifv18UL/kCgMHww==", null, "1234567890", false, null, "c9cfd11e-7b5e-4d49-b3aa-e2b32f52aeaa", false, null, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "533cbaf4-cdde-43d9-b6cc-962164c64b32", "ecf21b2b-66d0-4cb3-88b4-96f1b8e2625a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "533cbaf4-cdde-43d9-b6cc-962164c64b32", "ecf21b2b-66d0-4cb3-88b4-96f1b8e2625a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "533cbaf4-cdde-43d9-b6cc-962164c64b32");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ecf21b2b-66d0-4cb3-88b4-96f1b8e2625a");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "varchar(5)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "491f951e-ae93-4714-8bb2-3fbc474df8ad", "046d1e08-38ac-4b29-bfee-8fa1212dae11", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "Faculty", "FullName", "Gender", "LockoutEnabled", "LockoutEnd", "Major", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "PositionID", "SecurityStamp", "TwoFactorEnabled", "UID", "UserName" },
                values: new object[] { "e806f855-2acb-4a99-bc8d-42a30ec4fa30", 0, "123/15/8 Nguyễn Hữu Cảnh", "6e85eeb5-8337-4475-992b-643d29b4b353", "admin@gmail.com", false, null, "Admin", null, false, null, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEIAReSoRUrgrMbnRnEwmt2UYkpw0HlXkCZyVFXfOFwSvlO9dcK2OoywqN6jy9heSLw==", null, "1234567890", false, null, "cf2be3b3-246c-4632-82ee-fc6dcb4e498d", false, null, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "491f951e-ae93-4714-8bb2-3fbc474df8ad", "e806f855-2acb-4a99-bc8d-42a30ec4fa30" });
        }
    }
}
