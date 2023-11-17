using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlyBugClub_WebApp.Migrations
{
    /// <inheritdoc />
    public partial class fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "ImgUser",
                table: "AspNetUsers",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3a425b63-b4fe-49e7-b9ef-0883c1f37705", "328db17c-fafd-40e0-8e41-835482842829", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "Faculty", "FullName", "Gender", "ImgUser", "LockoutEnabled", "LockoutEnd", "Major", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "PositionID", "SecurityStamp", "TwoFactorEnabled", "UID", "UserName" },
                values: new object[] { "50a557dd-4fac-4ea1-b64a-d1e939256ce6", 0, "123/15/8 Nguyễn Hữu Cảnh", "92b266f3-4560-43ee-b592-11507edf5093", "admin@gmail.com", false, null, "Admin", null, null, false, null, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEFwF+h98enKKikSyYMghuFzE/AptgkgY3rHcYt3jgZmhLwMyyQfm1bhAb+pEiZix1A==", null, "1234567890", false, null, "c52def5d-c4fb-457f-a0ff-057b9bb0ae7c", false, null, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3a425b63-b4fe-49e7-b9ef-0883c1f37705", "50a557dd-4fac-4ea1-b64a-d1e939256ce6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3a425b63-b4fe-49e7-b9ef-0883c1f37705", "50a557dd-4fac-4ea1-b64a-d1e939256ce6" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a425b63-b4fe-49e7-b9ef-0883c1f37705");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "50a557dd-4fac-4ea1-b64a-d1e939256ce6");

            migrationBuilder.DropColumn(
                name: "ImgUser",
                table: "AspNetUsers");

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
    }
}
