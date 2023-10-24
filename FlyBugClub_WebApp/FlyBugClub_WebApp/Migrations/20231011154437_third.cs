using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlyBugClub_WebApp.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "UID",
                table: "AspNetUsers",
                type: "varchar(15)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(15)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PositionID",
                table: "AspNetUsers",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "AspNetUsers",
                type: "varchar(11)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "varchar(5)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(256)",
                oldMaxLength: 256,
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "UID",
                table: "AspNetUsers",
                type: "nchar(15)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PositionID",
                table: "AspNetUsers",
                type: "nchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "AspNetUsers",
                type: "nchar(11)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nchar(5)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

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
    }
}
